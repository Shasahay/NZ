using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NotesZone.Web
{
    using NotesZone.DataAccess;
    using NotesZone.Domain.Basket;
    using NotesZone.Domain.User;
    using NotesZone.Web.Binders;
    using NotesZone.Web.ViewModels;
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            NotesZoneDBInitializer.Initialize("", "");
            ModelBinders.Binders.Add(typeof(Basket), new BasketModelBinder());
            ModelBinders.Binders.Add(typeof(User), new UserModelBinder()); 
        }
    }
}
