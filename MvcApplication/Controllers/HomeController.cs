using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using MongoDB.Driver;
using MvcApplication.Models;

namespace MvcApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
