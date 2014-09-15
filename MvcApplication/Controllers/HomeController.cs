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

        public ActionResult Tenants()
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

		public ActionResult Leases(string id, string buildingid = "", string unitid = "", string tenantid = "")
		{
		    if (!string.IsNullOrEmpty(unitid))
		    {
			    var unit = _repository.Units.AsQueryable().FirstOrDefault(x => x.Id.Equals(unitid));
			    var building = _repository.Buildings.AsQueryable().FirstOrDefault(x => x.Id.Equals(unit.BuildingId));
    			if (unit == null)
    				return View(); // TODO return an error page?
			    ViewBag.Name = "Unit " + unit.Number + " " + unit.Description;
		        ViewBag.FromUrl = "/home/units/" + building.Id;
		    }
            else if (!string.IsNullOrEmpty(buildingid))
            {
			    var building = _repository.Buildings.AsQueryable().FirstOrDefault(x => x.Id.Equals(buildingid));
    			if (building == null)
    				return View(); // TODO return an error page?
			    ViewBag.Name = "Building "  + building.Name + " " + building.Address;
		        ViewBag.FromUrl = "/home/buildings/" + id;
            }
            else if (!string.IsNullOrEmpty(tenantid))
            {
			    var tenant = _repository.Tenants.AsQueryable().FirstOrDefault(x => x.Id.Equals(tenantid));
    			if (tenant == null)
    				return View(); // TODO return an error page?
                buildingid = tenant.BuildingId;
			    ViewBag.Name = "Tenant "  + tenant.FirstName + " " + tenant.LastName;
                ViewBag.FromUrl = "/home/tenants";
            }
            else
            {
			    var company = _repository.Companies.AsQueryable().FirstOrDefault(x => x.Id.Equals(id));
    			if (company == null)
    				return View(); // TODO return an error page?
			    ViewBag.Name = "Company " + company.Name + " " + company.Description;
                ViewBag.FromUrl = "/";
            }

			ViewBag.Id = id;
		    ViewBag.BuildingId = buildingid;
		    ViewBag.UnitId = unitid;
		    ViewBag.TenantId = tenantid;
			
			return View();
		}

		public ActionResult LeaseDetails(string id, string companyid)
		{
			var lease = _repository.Leases.AsQueryable().FirstOrDefault(x => x.Id.Equals(id));

			if (lease == null)
				return View(); // TODO return an error page?

		    ViewBag.FromUrl = Request.UrlReferrer == null ? "/" : Request.UrlReferrer.PathAndQuery;
			ViewBag.Id = id;
		    ViewBag.CompanyId = companyid;

			return View();
		}

		public ActionResult TenantDetails(string id, string companyid)
		{
			var tenant = _repository.Tenants.AsQueryable().FirstOrDefault(x => x.Id.Equals(id));

			if (tenant == null)
				return View(); // TODO return an error page?

			ViewBag.FromUrl = Request.UrlReferrer == null ? "/" : Request.UrlReferrer.PathAndQuery;
			ViewBag.Id = id;
			ViewBag.CompanyId = companyid;

			return View();
		}
		
		public ActionResult About()
        {
            return View();
        }
    }
}