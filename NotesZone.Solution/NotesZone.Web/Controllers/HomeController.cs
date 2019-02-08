using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesZone.Web.Controllers
{
    using NotesZone.DataAccess.Repository.Email;
    using NotesZone.Web.Infra.ActionResults.Client;
    using NotesZone.Web.Infra.ViewModels;
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return new HomePageViewModelActionResult<HomeController>(x => x.Index());
        }

        public ActionResult Details(int id)
        {
            return new DetailsViewModelActionResult<HomeController>(x => x.Details(id), id);
        }

        public ActionResult Category(int id, int page = 1, string Category = null, string SubCategory1 = null)
        {
            var viewModel = new NotesZone.Web.Infra.ViewModels.HomePageViewModel();
            TryValidateModel(viewModel);

            if(id ==1)  // if catagory = home redirecting to Index page
                return new HomePageViewModelActionResult<HomeController>(x => x.Index());
            else
            return new CategoryViewModelActionResult<HomeController>(x => x.Category(id, page, Category, SubCategory1), id, page, Category, SubCategory1);
        }

        [HttpPost]
        public ActionResult Contact( ContactUsViewModel  contactViewModel)
        {
            IEmailRepository _emailRepository = new EmailRepository();
           if( _emailRepository.SendEmail( contactViewModel.FirstName, contactViewModel.LastName, contactViewModel.Email, contactViewModel.Message))
           {
               ViewBag.Message = "Success"; // this is not appearing on the screen; i need to work on this latar
               return new CategoryViewModelActionResult<HomeController>(x => x.Category(4, 1, null, null), 4,1, null, null);
               
           }

           else
           {
               ViewBag.Message = "Try Again";
               return View("_ContactUs");
           }
        }


        public void Check()
        {

        }
        [HttpPost]
        public ActionResult Search(string Category, string SubCategory1, int page = 1, int id = 2)
        {

            if (Category.Equals("Select Exam", StringComparison.CurrentCultureIgnoreCase))
                Category = null;
            if (SubCategory1.Equals("Select Subject", StringComparison.CurrentCultureIgnoreCase))
                SubCategory1 = null;

            return new CategoryViewModelActionResult<HomeController>(x => x.Category(id, page, Category, SubCategory1), id, page, Category, SubCategory1);
        }
        
        

    }
}