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

        public ActionResult Companies()
        {
            return View();
        }

		public ActionResult Buildings(string id)
		{
		    var company = _repository.Companies.AsQueryable().FirstOrDefault(x => x.Id.Equals(id));

		    if (company == null)
		        return View(); // TODO return an error page?

			ViewBag.Id = id;
			ViewBag.Name = company.Name;

			return View();
		}
		
		public ActionResult Units(string id)
		{
		    var building = _repository.Buildings.AsQueryable().FirstOrDefault(b => b.Id.Equals(id));

		    if (building == null)
		        return View(); // TODO return an error page?

		    ViewBag.CompanyId = building.CompanyId;
			ViewBag.Id = building.Id;
			ViewBag.Name = building.Name;

			return View();
		}
		
		public ActionResult About()
        {
            return View();
        }
    }
}
