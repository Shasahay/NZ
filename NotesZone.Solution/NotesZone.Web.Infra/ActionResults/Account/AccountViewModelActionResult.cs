using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NotesZone.Web.Infra.ActionResults.Account
{
    using NotesZone.DataAccess.Repository.Common;
    using NotesZone.DataAccess.Repository.User;
    using NotesZone.Domain.Infrastructure;
    using NotesZone.Domain.User;
    using NotesZone.Web.Infra.ViewModels;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    public class AccountViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly User _user;
        //private readonly string  _name;
        //private readonly string  _password;
        private readonly string _tokenID;
        private readonly int _numOfPage;
        public AccountViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression, User user)
            : this(viewNameExpression, DependencyResolver.Current.GetService<IUserRepository>(),DependencyResolver.Current.GetService<IUserSubscriptionRepository>(), 
                   DependencyResolver.Current.GetService<ICategoryRepository>(), user)
        {

        }

        public AccountViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression, IUserRepository _userRepository,IUserSubscriptionRepository _userSubscriptionRepository, ICategoryRepository categoryRepository, User user)
            : base(viewNameExpression)
        {
            this._userRepository = new UserRepository();
            this._userSubscriptionRepository = new UserSubscriptionRepository();
            this._categoryRepository = new CategoryRepository();
            this._numOfPage = 3;
            this._user = user;
            //this._password = password;
           // this._tokenID = toeknID;
            this._numOfPage = 3;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            //var strEmail = "test@testing.com"; //"Test1@testing.com";  // for developement purpose
            //if (_userRepository.ValidateUser(email, password))
           // {
                var headerViewModel = new HeaderViewModel();
                var footerViewModel = new FooterViewModel();
                //var authorProfileViewModel = new AuthorProfileViewModel();
                var userProfileViewModel = new UserProfileViewModel();
                try
                {
                    var cats = this._categoryRepository.GetCategories();
                    headerViewModel.SiteTitle = "NotesZone: Your Account";
                    // headerViewModel.CurrentCategoryId = 1; // In case assign different catagory in tab selection (tested but not fully)
                    if (cats != null)
                    {
                        headerViewModel.Categories = cats;
                        headerViewModel.CurrentCategoryId = cats.Where(x => x.Name.Equals("Home", StringComparison.CurrentCultureIgnoreCase)).Select(i => i.Id).First();
                        footerViewModel.Categories = cats;

                    }

                    var userDashboardViewModel = new UserDashboardViewModel();
                    if (_user != null)
                    {
                        userProfileViewModel.UserProfile = this.BindDataForUserProfile(this._user.Email);
                    }

                    userDashboardViewModel.Header = headerViewModel;
                    userDashboardViewModel.UserProfileViewModel = userProfileViewModel;
                    userDashboardViewModel.Footer = footerViewModel;
                    this.GetViewResult(userDashboardViewModel).ExecuteResult(context);
                }
                catch (Exception ex)
                {

                }
           // }
        }

        private UserProfile BindDataForUserProfile(string email)
        {
            return this._userRepository.GetUserProfile(email);
        }

        private PaidSubscriptionViewModel BindDataForPaidSubscriptionViewModel(List<UserSubscription> userSubscriptions, ApplicationVariable.SubscriptionType subsType)
        {
            var uploadSubscriptionViewModel = new UploadSubscriptionViewModel();
            var downloadSubscriptionViewModel = new DownloadSubscriptionViewModel();
            var paidSubscriptionViewModel = new PaidSubscriptionViewModel();
            uploadSubscriptionViewModel.subscriptions = this._userSubscriptionRepository.GetSubscriptionBySubscriptionType(userSubscriptions, ApplicationVariable.SubscriptionType.Upload);
            downloadSubscriptionViewModel.subscriptions = this._userSubscriptionRepository.GetSubscriptionBySubscriptionType(userSubscriptions, ApplicationVariable.SubscriptionType.Download);
            paidSubscriptionViewModel = new PaidSubscriptionViewModel { UploadSubscriptionViewModel = uploadSubscriptionViewModel, DownloadSubscriptionViewModel = downloadSubscriptionViewModel };
            return paidSubscriptionViewModel;
        }

    }
}
