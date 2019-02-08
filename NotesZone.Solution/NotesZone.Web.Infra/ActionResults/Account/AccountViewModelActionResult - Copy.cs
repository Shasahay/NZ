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
        private readonly string  _email;
        private readonly string  _name;
        private readonly string  _password;
        private readonly string _tokenID;
        private readonly int _numOfPage;
        public AccountViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression, string name, string email, string toeknID, string password)
            : this(viewNameExpression, DependencyResolver.Current.GetService<IUserRepository>(),DependencyResolver.Current.GetService<IUserSubscriptionRepository>(), 
                   DependencyResolver.Current.GetService<ICategoryRepository>(), name, email, toeknID, password)
        {

        }

        public AccountViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression, IUserRepository _userRepository,IUserSubscriptionRepository _userSubscriptionRepository, ICategoryRepository categoryRepository, string name, string email, string toeknID, string password)
            : base(viewNameExpression)
        {
            this._userRepository = new UserRepository();
            this._userSubscriptionRepository = new UserSubscriptionRepository();
            this._categoryRepository = new CategoryRepository();
            this._numOfPage = 3;
            this._email = email;
            this._name = name;
            this._password = password;
            this._tokenID = toeknID;
            this._numOfPage = 3;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var strEmail = "test@testing.com"; //"Test1@testing.com";  // for developement purpose
            //if (_userRepository.ValidateUser(email, password))
           // {
                var headerViewModel = new HeaderViewModel();
                var footerViewModel = new FooterViewModel();
                try
                {
                    var cats = this._categoryRepository.GetCategories();
                    headerViewModel.SiteTitle = "NotesZone: Your Account";
                    // headerViewModel.CurrentCategoryId = 1; // In case assign different catagory in tab selection (tested but not fully)
                    if (cats != null)
                    {
                        headerViewModel.Categories = cats;
                        footerViewModel.Categories = cats;

                    }
                    var subscriptions = this._userSubscriptionRepository.GetSubscribeItembyUser(strEmail);
                    var subscriptionViewModel = new SubscriptionViewModel();
                    if (subscriptions != null)
                    {
                        subscriptionViewModel.UserSubscriptionViewModel.TrialSubscriptionViewModel = this.BindDataForTrialSubscriptionViewModel(subscriptions, ApplicationVariable.SubscriptionType.Trial);//_userSubscriptionRepository.GetSubscribeItembyUser(strEmail, "Trial");
                        subscriptionViewModel.UserSubscriptionViewModel.PaidSubscriptionViewModel = this.BindDataForPaidSubscriptionViewModel(subscriptions, ApplicationVariable.SubscriptionType.Paid);
                    }
                    subscriptionViewModel.DashBoard = new DashboardViewModel();
                    subscriptionViewModel.Header = headerViewModel;
                    subscriptionViewModel.Footer = footerViewModel;
                    this.GetViewResult(subscriptionViewModel).ExecuteResult(context);
                }
                catch (Exception ex)
                {

                }
           // }
        }

        private TrialSubscriptionViewModel BindDataForTrialSubscriptionViewModel(List<UserSubscription> userSubscriptions, ApplicationVariable.SubscriptionType subsType)
        {
            var trialSubscriptionViewModel = new TrialSubscriptionViewModel();
            trialSubscriptionViewModel.subscriptions = this._userSubscriptionRepository.GetSubscriptionBySubscriptionType(userSubscriptions, subsType);
            return trialSubscriptionViewModel;
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
