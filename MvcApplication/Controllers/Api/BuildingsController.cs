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
	[RoutePrefix("api/companies/{companyid}/buildings")]
	public class BuildingsController : ApiController
	{
	    private readonly Repository _repository;

	    public BuildingsController(Repository repository)
	    {
	        _repository = repository;
	    }

	    [Route(Name = "buildings")]
		public IHttpActionResult Get(string companyid)
	    {
	        var buildings = _repository.Buildings.AsQueryable().Where(x => x.CompanyId.Equals(companyid)).ToList();
			return Ok(buildings);
	    }

		[Route("{id}", Name = "building")]
		public IHttpActionResult Get(string companyid, string id)
		{
		    var building = _repository.Buildings.AsQueryable().FirstOrDefault(x => x.CompanyId.Equals(companyid) && x.Id.Equals(id));
		    if (building == null)
		        return NotFound();
		    return Ok(building);
		}

		[Route]
		public IHttpActionResult Post(string companyid, [FromBody]Building building)
		{
			var urlHelper = new UrlHelper(Request);

			building.Id = ObjectId.GenerateNewId().ToString();
		    building.CompanyId = companyid;
            building.Url = urlHelper.Link("building", new { id = building.Id });

			_repository.Buildings.Insert(building);
			return Created(building.Url, building);
		}

		[Route("{id}")]
		public IHttpActionResult Put(string id, [FromBody]Building building)
	    {
			_repository.Buildings.Save(building);
		    return Ok(building);
	    }

		[Route("{id}")]
		public IHttpActionResult Delete(string id)
		{
		    _repository.Units.Remove(Query.EQ("BuildingId", id));
			_repository.Buildings.Remove(Query.EQ("_id", id));
		    return Ok("");
	    }
	}
}