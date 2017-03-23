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
                   name: "SearchUsersByID",
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
                    constraints: new { name = @"[A-Za-zА-Яа-я]\w*" }
                );

            routes.MapRoute(
               name: "CreateUser",
               url: "create-{controller}",
               defaults: new { controller = "User", action = "Create" }
               );
            routes.MapRoute(
                    name: "Users",
                    url: "{controller}s",
                    defaults: new { controller = "User", action = "Index" }
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "CreateAward",
                url: "create-award",
                defaults: new { controller = "Award", action = "CreateAward" }
                );
        }
    }
}
