using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using MvcApplication.Services;
using Unity.WebApi;

namespace MvcApplication
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly IUnityContainer UnityContainerInstance = new UnityContainer();

        /// <summary>
        /// The singleton instance of the <see cref="IUnityContainer"/>.
        /// </summary>
        public IUnityContainer UnityContainer
        {
            get { return UnityContainerInstance; }
        }

        protected void Application_Start()
        {
            UnityContainer.RegisterType<Repository, Repository>();

            AreaRegistration.RegisterAllAreas();

            // WebApi Dependency Injection
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(UnityContainer);
			// WebApi Configuration
			GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}