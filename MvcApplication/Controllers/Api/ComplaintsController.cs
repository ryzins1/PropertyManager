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
	[RoutePrefix("api/companies/{companyid}/leases/{leaseid}/complaints")]
    public class ComplaintsController : ApiController
    {
	    private readonly Repository _repository;

	    public ComplaintsController(Repository repository)
	    {
	        _repository = repository;
	    }

	    [Route(Name = "complaints")]
		public IHttpActionResult Get(string companyid, string leaseid)
	    {
	        var complaints = _repository.Complaints.AsQueryable().Where(p => p.LeaseId.Equals(leaseid)).ToList();
			return Ok(complaints);
	    }

		[Route("{id}", Name = "complaint")]
		public IHttpActionResult Get(string companyid, string leaseid, string id)
		{
		    var complaint = _repository.Complaints.AsQueryable().FirstOrDefault(x => x.LeaseId.Equals(leaseid) && x.Id.Equals(id));
		    if (complaint == null)
		        return NotFound();
		    return Ok(complaint);
		}

		[Route]
		public IHttpActionResult Post(string companyid, string leaseid, [FromBody]Complaint complaint)
		{
			var urlHelper = new UrlHelper(Request);

			complaint.Id = ObjectId.GenerateNewId().ToString();
            complaint.Url = urlHelper.Link("complaint", new { companyid, leaseid,  id = complaint.Id });
		    complaint.ComplaintDate = DateTime.Now;

			_repository.Complaints.Insert(complaint);
			return Created(complaint.Url, complaint);
		}

		[Route("{id}")]
		public IHttpActionResult Put(string id, string leaseid, [FromBody]Complaint complaint)
	    {
			_repository.Complaints.Save(complaint);
		    return Ok(complaint);
	    }

		[Route("{id}")]
		public IHttpActionResult Delete(string id)
		{
			_repository.Complaints.Remove(Query.EQ("_id", id));
		    return Ok("");
	    }
    }
}