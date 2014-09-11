using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MvcApplication.Models;

namespace MvcApplication.Controllers.Api
{

	[RoutePrefix("api/buildings/{buildingid}/units")]
    public class UnitsController : ApiController
    {
		private readonly MongoCollection<Building> _buildingRepository;
		private readonly MongoCollection<Unit> _repository;

	    public UnitsController()
	    {
		    var connectionString = ConfigurationManager.ConnectionStrings["PropertyManager"].ConnectionString;
		    var client = new MongoClient(connectionString);
		    var server = client.GetServer();
		    var mongoDb = server.GetDatabase("PropertyManager");

		    _buildingRepository = mongoDb.GetCollection<Building>(typeof (Building).Name);
			_repository = mongoDb.GetCollection<Unit>(typeof(Unit).Name);
		}

		[Route("", Name = "units")]
		public IHttpActionResult Get(string buildingid)
		{
			//_repository.RemoveAll();
			var query = Query.EQ("BuildingId", buildingid);
			return Ok(_repository.Find(query).ToList());
	    }

		[Route("{id}", Name = "unit")]
		public IHttpActionResult Get(string buildingid, string id)
		{
			var query = Query.EQ("_id", id);
			return Ok(_repository.Find(query).Single());
		}

		[Route]
		public IHttpActionResult Post(string buildingid, [FromBody]Unit unit)
		{
			var urlHelper = new UrlHelper(Request);

			unit.BuildingId = buildingid;
			unit.Id = ObjectId.GenerateNewId().ToString();
			unit.Url = urlHelper.Link("unit", new { buildingid, id = unit.Id });
			_repository.Insert(unit);
			return Created(unit.Url, unit);
		}

		[Route("{id}")]
		public IHttpActionResult Put(string buildingid, string id, [FromBody]Unit unit)
	    {
			_repository.Save(unit);
		    return Ok(unit);
	    }

		[Route("{id}")]
		public IHttpActionResult Delete(string id)
	    {
			_repository.Remove(Query.EQ("_id", id));
		    return Ok("");
	    }
    }
}