using System.Linq;
using System.Web.Mvc;
using MongoDB.Driver.Linq;
using MvcApplication.Services;

namespace MvcApplication.Controllers
{
    public class HomeController : Controller
    {
		private readonly Repository _repository;

	    public HomeController(Repository repository)
	    {
	        _repository = repository;
	    }

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Units(string id)
		{
		    var building = _repository.Buildings.AsQueryable().FirstOrDefault(b => b.Id.Equals(id));

		    if (building == null)
		        return View(); // TODO return an error page?

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
