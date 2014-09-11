using System.Web.Mvc;

namespace MvcApplication.Controllers
{
    public class UnitsController : Controller
    {
        // GET: Unit
		[Route("/{id}/units")]
		public ActionResult Index(string id)
        {
	        ViewBag["Id"] = id;
            return View();
        }
    }
}