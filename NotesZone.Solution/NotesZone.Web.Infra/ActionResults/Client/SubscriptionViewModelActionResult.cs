using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NotesZone.Web.Infra.ActionResults.Client
{
    using System.Web.Mvc;
    using NotesZone.DataAccess.Repository.User;
    using NotesZone.Web.Infra.ViewModels;
    using NotesZone.Domain.User;
    using System.Linq.Expressions;
    using NotesZone.DataAccess.Repository.Common;
    using NotesZone.Domain.Infrastructure;
    public class SubscriptionViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly int _numOfPage;
        private readonly int _currentPage;
        public SubscriptionViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression)
            : this(viewNameExpression,
                   DependencyResolver.Current.GetService<IUserSubscriptionRepository>(), 
                   DependencyResolver.Current.GetService<ICategoryRepository>())
        {
        }

        public SubscriptionViewModelActionResult(
            Expression<Func<TController, ActionResult>> viewNameExpression,
            IUserSubscriptionRepository _userSubscriptionRepository, ICategoryRepository categoryRepository)
            : base(viewNameExpression)
        {
            this._userSubscriptionRepository = new UserSubscriptionRepository();
            this._categoryRepository = new CategoryRepository();
            this._numOfPage = 3;// int.Parse(ConfigurationManager.GetAppConfigBy("NumOfPage"));
        }

        public override void ExecuteResult(ControllerContext context)
        {
           // var strEmail = "Developerfirst@development.com"; //"Test1@testing.com";  // for developement purpose
            var user = (User)(context.HttpContext.Session["User"]);
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
                    headerViewModel.CurrentCategoryId = cats.Where(x => x.Name.Equals("Home", StringComparison.CurrentCultureIgnoreCase)).Select(i => i.Id).First();
                    footerViewModel.Categories = cats;
                    
                }
                
                var subscriptionViewModel = new SubscriptionViewModel();
                if (user.Email != null)
                {
                    //subscriptionViewModel.UserSubscriptionViewModel.TrialSubscriptionViewModel = this.BindDataForTrialSubscriptionViewModel(subscriptions, ApplicationVariable.SubscriptionType.Trial);//_userSubscriptionRepository.GetSubscribeItembyUser(strEmail, "Trial");
                    subscriptionViewModel.UserSubscriptionViewModel.PaidSubscriptionViewModel = this.BindDataForPaidSubscriptionViewModel(user.Email, ApplicationVariable.SubscriptionType.Paid);
                }
                subscriptionViewModel.DashBoard = new DashboardViewModel();
                subscriptionViewModel.Header = headerViewModel;
                subscriptionViewModel.Footer = footerViewModel;
                this.GetViewResult(subscriptionViewModel).ExecuteResult(context);
            }
            catch(Exception ex)
            {

            }
        }

        /// <summary>
        /// Not using this business might use in future
        /// </summary>
        /// <param name="userSubscriptions"></param>
        /// <param name="subsType"></param>
        /// <returns></returns>
        private TrialSubscriptionViewModel BindDataForTrialSubscriptionViewModel(List<UserSubscription> userSubscriptions, ApplicationVariable.SubscriptionType subsType)
        {
            var trialSubscriptionViewModel = new TrialSubscriptionViewModel();
            trialSubscriptionViewModel.subscriptions = this._userSubscriptionRepository.GetSubscriptionBySubscriptionType(userSubscriptions, subsType);
            return trialSubscriptionViewModel;
        }

        private PaidSubscriptionViewModel BindDataForPaidSubscriptionViewModel(string email, ApplicationVariable.SubscriptionType subsType)
        {
            var uploadSubscriptionViewModel = new UploadSubscriptionViewModel();
            var downloadSubscriptionViewModel = new DownloadSubscriptionViewModel();
            var paidSubscriptionViewModel = new PaidSubscriptionViewModel();
            uploadSubscriptionViewModel.subscriptions = this._userSubscriptionRepository.GetSubscribeItembyUser(email, ApplicationVariable.SubscriptionType.Upload.ToString());
            downloadSubscriptionViewModel.subscriptions = this._userSubscriptionRepository.GetSubscribeItembyUser(email, ApplicationVariable.SubscriptionType.Download.ToString());
            //paidSubscriptionViewModel = new PaidSubscriptionViewModel { UploadSubscriptionViewModel = uploadSubscriptionViewModel, DownloadSubscriptionViewModel = downloadSubscriptionViewModel };

            paidSubscriptionViewModel.UploadSubscriptionViewModel = uploadSubscriptionViewModel;
            paidSubscriptionViewModel.DownloadSubscriptionViewModel = downloadSubscriptionViewModel;
            return paidSubscriptionViewModel;
        }

    }
}
