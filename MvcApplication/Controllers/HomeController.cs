using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MvcApplication.Models;

namespace MvcApplication.Controllers
{
    public class HomeController : Controller
    {
		private readonly MongoCollection<Building> _buildingRepository;

	    public HomeController()
	    {
			var connectionString = ConfigurationManager.ConnectionStrings["PropertyManager"].ConnectionString;
			var client = new MongoClient(connectionString);
			var server = client.GetServer();
			var mongoDb = server.GetDatabase("PropertyManager");

			_buildingRepository = mongoDb.GetCollection<Building>(typeof(Building).Name);
	    }

		public ActionResult Index()
        {
            return View();
        }

		public ActionResult Units(string id)
		{
			var query = Query.EQ("_id", id);
			var building = _buildingRepository.Find(query).Single();

			ViewBag.Id = id;
			ViewBag.BuildingName = building.Name;

			return View();
		}
		
		public ActionResult About()
        {
            return View();
        }
    }
}
