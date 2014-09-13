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
	[RoutePrefix("api/buildings")]
	public class BuildingsController : ApiController
	{
	    private readonly Repository _repository;

	    public BuildingsController(Repository repository)
	    {
	        _repository = repository;
	    }

	    [Route(Name = "buildings")]
		public IHttpActionResult Get()
		{
			//_repository.RemoveAll();
			return Ok(_repository.Buildings.FindAll().ToList());
	    }

		[Route("{id}", Name = "building")]
		public IHttpActionResult Get(string id)
		{
		    var building = _repository.Buildings.AsQueryable().FirstOrDefault(b => b.Id.Equals(id));
		    if (building == null)
		        return NotFound();
		    return Ok(building);

			//var query = Query.EQ("_id", id);
			//return Ok(_repository.Buildings.Find(query).Single());
		}

		[Route]
		public IHttpActionResult Post([FromBody]Building building)
		{
			var urlHelper = new UrlHelper(Request);

			building.Id = ObjectId.GenerateNewId().ToString();
            building.Url = urlHelper.Link("building", new { id = building.Id });
			building.UnitsUrl = urlHelper.Link("units", new {buildingid = building.Id});

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
			_repository.Buildings.Remove(Query.EQ("_id", id));
		    return Ok("");
	    }
	}
}
