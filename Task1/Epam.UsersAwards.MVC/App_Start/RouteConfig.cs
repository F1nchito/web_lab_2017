using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Epam.UsersAwards.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "CreateUser",
               url: "create-user",
               defaults: new { controller = "Users", action = "Create" }
               );
            routes.MapRoute(
                    name: "Users",
                    url: "users",
                    defaults: new { controller = "Users", action = "Index" }
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Users", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "CreateAward",
                url: "create-award",
                defaults: new { controller = "Awards", action = "CreateAward" }
                );
        }
    }
}
