using NotesZone.Domain.User;
using NotesZone.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesZone.Web.Controllers
{
    using NotesZone.DataAccess.Repository.User;
using NotesZone.Domain.Factory;
using NotesZone.Web.Infra.ActionResults.Account;
using NotesZone.Web.Infra.ViewModels;
using NotesZone.Web.Infra.ViewModels.Persistences;
    public class AccountController : Controller
    {
        private IUserCreatingPersistence _userCreatingPersistence;
        private readonly IUserRepository _userRepository;
        // GET: Account
        public AccountController()
        {
            _userRepository = new UserRepository();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

       // [HttpPost]
        //public ActionResult Login(string name, string email, string tockenID, string password)
        //{
        //   // var loginViewModel = new LoginViewModel{ Email = email, Password = password};
        //    //return View();LoginViewModel
        //   //return new  AccountViewModelActionResult<AccountController>(x => x.Login(name, email,tockenID, password), name, email, tockenID, password);

        //    return RedirectToAction("Index", "Subscription");

        //}
        [HttpPost]
        public ActionResult LogIn(LoginViewModel loginViewModel, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                User user = new User { Email = loginViewModel.Email, Password = loginViewModel.Password };
                if (_userRepository.IsValidateUser(user))
                {
                    Session["User"] = user;
                    //return RedirectToAction("Index", "UploadDashboard"); // working code
                    //return RedirectToAction("Checkout", "Basket", new { returnUrl });
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ViewBag.Message = "Invalid Username or Password";
                    return View(); //return the same view with message "Invalid Username or Password"
                }
            }
            
            else
            {
                ViewBag.Message = "Invalid Username or Password";
                return View(); // Return the same view with validation errors.
            }
 
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "UploadDashboard");
            }
        }
        public ActionResult UserDashboard(User user)
        {
            return new AccountViewModelActionResult<AccountController>(x => x.UserDashboard(user), user);
        }

        public PartialViewResult LogInSumary(User user)
        {
            return PartialView(user);
        }

        public ActionResult SignOut()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }


        public ActionResult RegisterUser() 
        {
            return View("Register");
        }

        public ActionResult Register(UserViewModel userViewModel, string returnUrl)
        {
            _userCreatingPersistence = new UserCreatingPersistence();
            var varUser =  CreateandUpdateUser(userViewModel, true);
            var varUserProfile = CreateOrUpdateUserProfile(userViewModel, true);
            if (_userCreatingPersistence.PersistenceUser(varUser) && _userCreatingPersistence.PersistenceUserProfile(varUserProfile))
            {
                Session["User"] = new User { Email = varUser.Email};
                return RedirectToAction("Index", "Home");
            }
            return null;
        }


        private User CreateandUpdateUser(dynamic vm, bool isNew)
        {
            return  UserFactory.CreateUser( vm.Email, vm.Password);
                
        }

        private UserProfile CreateOrUpdateUserProfile(dynamic vm, bool isNew)
        {
            return  UserProfileFactory.CreateUserProfile(vm.FirstName, vm.LastName, vm.Email);
        }

    }
}