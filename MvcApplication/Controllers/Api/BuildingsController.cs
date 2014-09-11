using System.Configuration;
using System.Linq;
using System.Web.Http;
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
			//var mongoDb = MongoDatabase.Create(connectionString);

			_repository = mongoDb.GetCollection<Building>(typeof(Building).Name);
		}

		[Route]
		public IHttpActionResult Get()
		{
			return Ok(_repository.FindAll().ToList());
	    }

		[Route("{id}")]
		public IHttpActionResult Get(string id)
		{
			var query = Query.EQ("_id", id);
			return Ok(_repository.Find(query).Single());
		}

		[Route]
		public IHttpActionResult Post([FromBody]Building building)
		{
			building.Id = ObjectId.GenerateNewId().ToString();
			_repository.Insert(building);
			string uri = Url.Route(null, new { id = building.Id });
			return Created(uri, building);
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
