using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NotesZone.Web.Binders
{
    using NotesZone.Domain.User;
    using System.Web.Mvc;
    public class UserModelBinder : IModelBinder
    {
        private const string sessionKey = "User";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            User user = (User)controllerContext.HttpContext.Session[sessionKey];
            if (user == null)
            {
                user = new User();
                controllerContext.HttpContext.Session[sessionKey] = user;
            }
            // return the user
            return user;
        }
    }
    
    
}