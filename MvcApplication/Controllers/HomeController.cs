using System.Web.Mvc;

namespace MvcApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Units(string id)
		{
			ViewBag.Id = id;
			return View();
		}
		
		public ActionResult About()
        {
            return View();
        }
    }
}
