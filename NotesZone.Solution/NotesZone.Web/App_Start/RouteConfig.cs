using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NotesZone.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "null",
                url: "{controller}/{action}/{id}/{page}",
                defaults: new { controller = "Home", action = "Index", id = "", page = "" }
                );
            routes.MapRoute(
                name: "null1",
                url: "{controller}/{action}/{id}/{page}/{Category}/{SubCategory1}",
                defaults: new { controller = "Home", action = "Index", id = "", page = "", searchcategor = "", searchsubcategory = "" }
                );
                routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional}
            );

            

        }
    }
}
