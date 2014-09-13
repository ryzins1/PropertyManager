﻿using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MvcApplication.Models;
using MvcApplication.Services;

namespace MvcApplication.Controllers.Api
{
	[RoutePrefix("api/companies/{companyid}/leases")]
	public class LeasesController : ApiController
	{
	    private readonly Repository _repository;

	    public LeasesController(Repository repository)
	    {
	        _repository = repository;
	    }

	    [Route(Name = "leases")]
		public IHttpActionResult Get(string companyid)
	    {
	        var leases = _repository.Leases.AsQueryable().Where(x => x.CompanyId.Equals(companyid)).ToList();
			return Ok(leases);
	    }

		[Route("{id}", Name = "lease")]
		public IHttpActionResult Get(string companyid, string id)
		{
		    var lease = _repository.Leases.AsQueryable().FirstOrDefault(x => x.CompanyId.Equals(companyid) && x.Id.Equals(id));
		    if (lease == null)
		        return NotFound();
		    return Ok(lease);
		}

		[Route]
		public IHttpActionResult Post(string companyid, [FromBody]Lease lease)
		{
			var urlHelper = new UrlHelper(Request);

			lease.Id = ObjectId.GenerateNewId().ToString();
		    lease.CompanyId = companyid;
            lease.Url = urlHelper.Link("lease", new { id = lease.Id });

			_repository.Leases.Insert(lease);
			return Created(lease.Url, lease);
		}

		[Route("{id}")]
		public IHttpActionResult Put(string id, [FromBody]Lease lease)
	    {
			_repository.Leases.Save(lease);
		    return Ok(lease);
	    }

		[Route("{id}")]
		public IHttpActionResult Delete(string id)
		{
			_repository.Leases.Remove(Query.EQ("_id", id));
		    return Ok("");
	    }
	}
}