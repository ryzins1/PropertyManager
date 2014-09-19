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
	[RoutePrefix("api/maintenancerequests")]
	public class MaintenanceRequestsController : ApiController
	{
		private readonly Repository _repository;

	    public MaintenanceRequestsController (Repository repository)
	    {
	        _repository = repository;
	    }

	    [Route(Name = "maintenancerequests")]
		public IHttpActionResult Get()
	    {
	        var maintenanceRequests = _repository.MaintenanceRequests.AsQueryable().ToList();
			return Ok(maintenanceRequests);
	    }

        [Route]
        [HttpGet]
		public IHttpActionResult GetWithQuery(string companyid, string buildingid = "", string unitid = "", string tenantid = "")
        {
            var maintenanceRequests = _repository.MaintenanceRequests.AsQueryable().Where(x => x.CompanyId.Equals(companyid) && x.BuildingId.Equals(buildingid));
            if (!string.IsNullOrEmpty(unitid))
                maintenanceRequests = maintenanceRequests.Where(x => x.UnitId.Equals(unitid));
            if (!string.IsNullOrEmpty(tenantid))
                maintenanceRequests = maintenanceRequests.Where(x => x.TenantId.Equals(tenantid));
		    return Ok(maintenanceRequests.ToList());
		}

		[Route("{id}", Name = "maintenancerequest")]
		public IHttpActionResult Get(string id)
		{
			var maintenanceRequest = _repository.MaintenanceRequests.AsQueryable().FirstOrDefault(x => x.Id.Equals(id));
		    if (maintenanceRequest == null)
		        return NotFound();
		    return Ok(maintenanceRequest);
		}

		[Route]
		public IHttpActionResult Post([FromBody]MaintenanceRequest maintenanceRequest)
		{
			var urlHelper = new UrlHelper(Request);

			maintenanceRequest.Id = ObjectId.GenerateNewId().ToString();
            maintenanceRequest.Url = urlHelper.Link("maintenanceRequest", new { id = maintenanceRequest.Id });

		    if (string.IsNullOrEmpty(maintenanceRequest.CompanyId))
		    {
		        var company = _repository.Companies.AsQueryable().FirstOrDefault();
		        maintenanceRequest.CompanyId = company == null ? "" : company.Id;

    		    if (string.IsNullOrEmpty(maintenanceRequest.BuildingId))
    		    {
    		        var building = _repository.Buildings.AsQueryable().FirstOrDefault(x => x.CompanyId.Equals(company.Id));
    		        maintenanceRequest.BuildingId = building == null ? "" : building.Id;
    		    }
		    }

			_repository.MaintenanceRequests.Insert(maintenanceRequest);
			return Created(maintenanceRequest.Url, maintenanceRequest);
		}

		[Route("{id}")]
		public IHttpActionResult Put(string id, [FromBody]MaintenanceRequest maintenanceRequest)
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