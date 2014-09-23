using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MvcApplication.Models;
using MvcApplication.Services;

namespace MvcApplication.Controllers.Api
{
	[RoutePrefix("api/companies/{companyid}/leases/{leaseid}/maintenancerequests")]
	public class MaintenanceRequestsController : ApiController
	{
		private readonly Repository _repository;

	    public MaintenanceRequestsController (Repository repository)
	    {
	        _repository = repository;
	    }

	    [Route(Name = "maintenancerequests")]
		public IHttpActionResult Get(string companyid, string leaseid)
	    {
	        var request = _repository.MaintenanceRequests.AsQueryable().Where(x => x.LeaseId.Equals(leaseid)).OrderByDescending(x => x.MaintenanceDate).ToList();
			return Ok(request);
	    }

		[Route("{id}", Name = "maintenancerequest")]
		public IHttpActionResult Get(string companyid, string leaseid, string id)
		{
		    var request = _repository.MaintenanceRequests.AsQueryable().FirstOrDefault(x => x.LeaseId.Equals(leaseid) && x.Id.Equals(id));
		    if (request == null)
		        return NotFound();
		    return Ok(request);
		}

		[Route]
		public IHttpActionResult Post(string companyid, string leaseid, [FromBody]MaintenanceRequest maintenanceRequest)
		{
			var urlHelper = new UrlHelper(Request);

			maintenanceRequest.Id = ObjectId.GenerateNewId().ToString();
            maintenanceRequest.Url = urlHelper.Link("maintenancerequest", new { companyid, leaseid,  id = maintenanceRequest.Id });
		    maintenanceRequest.MaintenanceDate = DateTime.Now;

			_repository.MaintenanceRequests.Insert(maintenanceRequest);
			return Created(maintenanceRequest.Url, maintenanceRequest);
		}

		[Route("{id}")]
		public IHttpActionResult Put(string id, string leaseid, [FromBody]MaintenanceRequest maintenanceRequest)
	    {
			_repository.MaintenanceRequests.Save(maintenanceRequest);
		    return Ok(maintenanceRequest);
	    }

		[Route("{id}")]
		public IHttpActionResult Delete(string id)
		{
			_repository.MaintenanceRequests.Remove(Query.EQ("_id", id));
		    return Ok("");
	    }
	}
}