using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MvcApplication.Models;

namespace MvcApplication.Controllers.Api
{
	[RoutePrefix("api/buildings")]
	public class BuildingsController : ApiController
    {
	    private readonly MongoCollection<Building> _repository;

	    public BuildingsController()
	    {
		    var connectionString = ConfigurationManager.ConnectionStrings["PropertyManager"].ConnectionString;
		    var client = new MongoClient(connectionString);
		    var server = client.GetServer();
		    var mongoDb = server.GetDatabase("PropertyManager");

			_repository = mongoDb.GetCollection<Building>(typeof(Building).Name);
		}

		[Route(Name = "buildings")]
		public IHttpActionResult Get()
		{
			//_repository.RemoveAll();
			return Ok(_repository.FindAll().ToList());
	    }

		[Route("{id}", Name = "building")]
		public IHttpActionResult Get(string id)
		{
			var query = Query.EQ("_id", id);
			return Ok(_repository.Find(query).Single());
		}

		[Route]
		public IHttpActionResult Post([FromBody]Building building)
		{
			var urlHelper = new UrlHelper(Request);

			building.Id = ObjectId.GenerateNewId().ToString();
            building.Url = urlHelper.Link("building", new { id = building.Id });
			building.UnitsUrl = urlHelper.Link("units", new {buildingid = building.Id});

			_repository.Insert(building);
			return Created(building.Url, building);
		}

		[Route("{id}")]
		public IHttpActionResult Put(string id, [FromBody]Building building)
	    {
			_repository.Save(building);
		    return Ok(building);
	    }

		[Route("{id}")]
		public IHttpActionResult Delete(string id)
	    {
			_repository.Remove(Query.EQ("_id", id));
		    return Ok("");
	    }
	}
}
