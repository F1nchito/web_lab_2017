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

            //routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                   name: "UserAddAward",
                   url: "award-user/{userID}_{awardID}",
                   defaults: new { controller = "User", action = "AddAward" }
               );
            routes.MapRoute(
                   name: "SearchByID",
                   url: "{controller}/{id}",
                   defaults: new { controller = "User", action = "GetByID" },
                   constraints: new { id = @"\d+" }
               );
            routes.MapRoute(
                   name: "Inverse",
                   url: "{controller}/{id}/{action}",
                   defaults: new { controller = "User", action = "GetByID" },
                   constraints: new { id = @"\d+" }
               );
            routes.MapRoute(
                    name: "SearchUsers",
                    url: "{controller}s/{filter}",
                    defaults: new { controller = "User", action = "GetByFilter" },
                    constraints : new { filter = @"\w+" }
                );

            routes.MapRoute(
                    name: "UserSearchByName",
                    url: "{controller}/{name}",
                    defaults: new { controller = "User", action = "GetByName" },
                    constraints: new { name = @"\w+" }
                );

            routes.MapRoute(
                    name: "awards",
                    url: "awards",
                    defaults: new { controller = "Award", action = "Index" }
                );
            routes.MapRoute(
                   name: "users",
                   url: "users",
                   defaults: new { controller = "User", action = "Index" }
               );
            routes.MapRoute(
               name: "CreateUser",
               url: "{action}-{controller}",
               defaults: new { controller = "User", action = "Create" }
               );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
