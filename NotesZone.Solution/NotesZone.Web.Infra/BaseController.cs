using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NotesZone.Web.Infra
{
    using NotesZone.Domain.User;
using System.Security.Authentication;
using System.Web.Mvc;
    public abstract class BaseController : Controller
    {
        protected string GetUserName()
        {
            if (User == null || User.Identity == null)
                throw new AuthenticationException("You should log in to the system");

            return User.Identity.Name;
        }

        protected User GetUser()
        {
            return (User)(Session["User"]);
        }

        protected string GetUserEmail()
        {
            var user =  (User)(Session["User"]);
            return user.Email;
        }


        protected void ErrorMessage(string errorMsg)
        {
            this.ViewBag.ErrorMessage = errorMsg;
        }

        protected void SucceedMessage(string succeedMsg)
        {
            this.ViewBag.SucceedMessage = succeedMsg;
        }
    }
}
