using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NotesZone.Web.Binders
{
    using NotesZone.Domain.Basket;
    using System.Web.Mvc;
    public class BasketModelBinder : IModelBinder
    {
        private const string sessionKey = "Basket";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // get the Basket from the session
            Basket basket = (Basket)controllerContext.HttpContext.Session[sessionKey];
            // create the Basket if there wasn't one in the session data
            if (basket == null)
            {
                basket = new Basket();
                controllerContext.HttpContext.Session[sessionKey] = basket;
            }
            // return the basket
            return basket;
        }
    }
}