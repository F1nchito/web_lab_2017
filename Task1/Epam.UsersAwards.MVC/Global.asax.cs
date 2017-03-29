using Epam.UsersAwards.MVC.App_Start;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Epam.UsersAwards.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfig.RegisterMaps();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
