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
	[RoutePrefix("api/tenants")]
	public class TenantsController : ApiController
	{
	    private readonly Repository _repository;

	    public TenantsController(Repository repository)
	    {
	        _repository = repository;
	    }

	    [Route(Name = "tenants")]
		public IHttpActionResult Get()
	    {
	        var tenants = _repository.Tenants.AsQueryable().ToList();
			return Ok(tenants);
	    }

        [Route]
        [HttpGet]
		public IHttpActionResult GetWithQuery(string companyid, string buildingid = "", string unitid = "")
        {
            var tenants = _repository.Tenants.AsQueryable().Where(x => x.CompanyId.Equals(companyid));
            if (!string.IsNullOrEmpty(companyid))
                tenants = tenants.Where(x => x.CompanyId.Equals(companyid));
            if (!string.IsNullOrEmpty(unitid))
                tenants = tenants.Where(x => x.UnitId.Equals(unitid));
		    return Ok(tenants.ToList());
		}

		[Route("{id}", Name = "tenant")]
		public IHttpActionResult Get(string id)
		{
		    var tenant = _repository.Tenants.AsQueryable().FirstOrDefault(x => x.Id.Equals(id));
		    if (tenant == null)
		        return NotFound();
		    return Ok(tenant);
		}

		[Route]
		public IHttpActionResult Post([FromBody]Tenant tenant)
		{
			var urlHelper = new UrlHelper(Request);

			tenant.Id = ObjectId.GenerateNewId().ToString();
            tenant.Url = urlHelper.Link("tenant", new { id = tenant.Id });

		    if (string.IsNullOrEmpty(tenant.CompanyId))
		    {
		        var company = _repository.Companies.AsQueryable().FirstOrDefault();
		        tenant.CompanyId = company == null ? "" : company.Id;

    		    if (string.IsNullOrEmpty(tenant.BuildingId))
    		    {
    		        var building = _repository.Buildings.AsQueryable().FirstOrDefault(x => x.CompanyId.Equals(company.Id));
    		        tenant.BuildingId = building == null ? "" : building.Id;
    		    }
		    }

			_repository.Tenants.Insert(tenant);
			return Created(tenant.Url, tenant);
		}

		[Route("{id}")]
		public IHttpActionResult Put(string id, [FromBody]Tenant tenant)
	    {
			_repository.Tenants.Save(tenant);
		    return Ok(tenant);
	    }

		[Route("{id}")]
		public IHttpActionResult Delete(string id)
		{
			_repository.Tenants.Remove(Query.EQ("_id", id));
		    return Ok("");
	    }
	}
}