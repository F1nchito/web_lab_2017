using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Epam.UsersAwards.MVCattr.App_Start;

namespace Epam.UsersAwards.MVCattr
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
