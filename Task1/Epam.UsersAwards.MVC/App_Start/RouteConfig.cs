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
                   url: "user/{id}",
                   defaults: new { controller = "Users", action = "GetByID" },
                   constraints: new { id = @"\d+" }
               );
            routes.MapRoute(
                   name: "123",
                   url: "user/{id}/{action}",
                   defaults: new { controller = "Users", action = "GetByID" },
                   constraints: new { id = @"\d+" }
               );
            routes.MapRoute(
                    name: "SearchUsers",
                    url: "users/{filter}",
                    defaults: new { controller = "Users", action = "GetByFilter" },
                    constraints : new { filter = @"\w+" }
                );

            routes.MapRoute(
                    name: "UserSearchByName",
                    url: "user/{name}",
                    defaults: new { controller = "Users", action = "GetByName" },
                    constraints: new { name = @"[A-Za-zА-Яа-я]\w*" }
                );
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
