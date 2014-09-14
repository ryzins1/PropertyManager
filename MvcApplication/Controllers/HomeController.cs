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

		public ActionResult Leases(string id, string buildingid = "", string unitid = "")
		{
		    if (!string.IsNullOrEmpty(unitid))
		    {
			    var unit = _repository.Units.AsQueryable().FirstOrDefault(x => x.Id.Equals(unitid));
    			if (unit == null)
    				return View(); // TODO return an error page?
			    ViewBag.Name = "Unit " + unit.Number + " " + unit.Description;
		        
		    }
            else if (!string.IsNullOrEmpty(buildingid))
            {
			    var building = _repository.Buildings.AsQueryable().FirstOrDefault(x => x.Id.Equals(buildingid));
    			if (building == null)
    				return View(); // TODO return an error page?
			    ViewBag.Name = "Building "  + building.Name + " " + building.Address;
            }
            else
            {
			    var company = _repository.Companies.AsQueryable().FirstOrDefault(x => x.Id.Equals(id));
    			if (company == null)
    				return View(); // TODO return an error page?
			    ViewBag.Name = "Company " + company.Name + " " + company.Description;
            }

			ViewBag.Id = id;
		    ViewBag.BuildingId = buildingid;
		    ViewBag.UnitId = unitid;
			
			return View();
		}

		public ActionResult LeaseDetails(string id)
		{
			var lease = _repository.Leases.AsQueryable().FirstOrDefault(x => x.Id.Equals(id));

			if (lease == null)
				return View(); // TODO return an error page?

			ViewBag.Id = id;
			ViewBag.LeaseId = lease.Id;
			//ViewBag.Name = company.Name;

			return View();
		}
		
		public ActionResult About()
        {
            return View();
        }
    }
}
