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

        [Route]
        [HttpGet]
		public IHttpActionResult GetWithQuery(string companyid, string buildingid, string unitid = "", string tenantid = "")
        {
            var leases = _repository.Leases.AsQueryable().Where(x => x.CompanyId.Equals(companyid) && x.BuildingId.Equals(buildingid));
            if (!string.IsNullOrEmpty(unitid))
                leases = leases.Where(x => x.UnitId.Equals(unitid));
            if (!string.IsNullOrEmpty(tenantid))
            {
                leases = leases.Where(x => x.TenantIds.Contains(tenantid));
            }
		    return Ok(leases.ToList());
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
            lease.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
		    lease.EndDate = lease.StartDate.AddYears(1).AddDays(-1);

            var buildings = _repository.Buildings.AsQueryable().Where(x => x.CompanyId.Equals(companyid));
		    if (!string.IsNullOrEmpty(lease.BuildingId))
		        buildings = buildings.Where(x => x.Id.Equals(lease.BuildingId));

		    var building = buildings.FirstOrDefault();
            if (building == null)
                throw new Exception("You must create a Building for this Company before you can create a Lease");

		    lease.BuildingId = building.Id;
		    lease.BuildingName = building.Name;

		    var units = _repository.Units.AsQueryable().Where(x => x.BuildingId.Equals(building.Id));
		    if (!string.IsNullOrEmpty(lease.UnitId))
		        units = units.Where(x => x.Id.Equals(lease.UnitId));

		    var unit = units.FirstOrDefault();
            if (unit == null)
                throw new Exception("You must create a Unit for this Building before you can create a Lease");

		    lease.UnitId = unit.Id;
		    lease.UnitNumber = unit.Number;

		    foreach (var tenantId in lease.TenantIds)
		    {
		        var tenant = _repository.Tenants.AsQueryable().FirstOrDefault(t => t.Id.Equals(tenantId));
                if (tenant != null)
                    tenant.LeaseIds.Add(lease.Id);
		        _repository.Tenants.Save(tenant);
		    }

			_repository.Leases.Insert(lease);
			return Created(lease.Url, lease);
		}

		[Route("{id}")]
		public IHttpActionResult Put(string id, [FromBody]Lease lease)
	    {
            var building = _repository.Buildings.AsQueryable().FirstOrDefault(x => x.Id.Equals(lease.BuildingId));
            if (building == null)
                throw new Exception("You must create a Building for this Company before you can create a Lease");
		    lease.BuildingName = building.Name;
		    var unit = _repository.Units.AsQueryable().FirstOrDefault(x => x.Id.Equals(lease.UnitId));
            if (unit == null)
                throw new Exception("You must create a Unit for this Building before you can create a Lease");
		    lease.UnitNumber = unit.Number;

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