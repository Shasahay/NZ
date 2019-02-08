using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NotesZone.Web.Infra.ActionResults.Client
{
    using NotesZone.DataAccess.Repository.Common;
    using NotesZone.DataAccess.Repository.User;
    using NotesZone.Domain.User;
    using NotesZone.Web.Infra.ViewModels;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    public class UploadDashboardViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;
        public UploadDashboardViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression)
            : this(viewNameExpression,
                   DependencyResolver.Current.GetService<IUserSubscriptionRepository>(),
                   DependencyResolver.Current.GetService<ICategoryRepository>(), DependencyResolver.Current.GetService<IItemRepository>())
        {
            
        }
        public UploadDashboardViewModelActionResult (
            Expression<Func<TController, ActionResult>> viewNameExpression,
            IUserSubscriptionRepository _userRepository, ICategoryRepository _categoryRepository, IItemRepository _itemRepository)
            : base(viewNameExpression)
	    {
               this._userRepository = new UserRepository();
               this._categoryRepository = new CategoryRepository();
               this._itemRepository = new ItemRepository();
	    }

        public override void ExecuteResult(ControllerContext context)
        {
            //var strEmail = "test@testing.com"; //"Test1@testing.com";  // for developement purpose
            
            var user = (User)(context.HttpContext.Session["User"]);
            var headerViewModel = new HeaderViewModel();
            var footerViewModel = new FooterViewModel();
            var filterViewModel = new FilterViewModel();
            //var itemCategoryViewModel = new ItemCategoryViewModel();
            
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
                var userProfile = this._userRepository.GetUserProfile(user.Email);
                var uploadDashBoardViewModel = new UploadDashboardViewModel();
                if (userProfile != null)
                {
                    uploadDashBoardViewModel.AuthorProfileViewModel.UserProfile = userProfile;
                    
                }
                uploadDashBoardViewModel.ItemContentViewModel.ItemContent = new NotesZone.Domain.Common.ItemContent();
                //itemCategoryViewModel.ItemCategory = _itemRepository.GetItemCategory();
                filterViewModel.CategoryFilter = this.BindDataForCategoryFilter();
                uploadDashBoardViewModel.Header = headerViewModel;
                uploadDashBoardViewModel.Filter = filterViewModel;
                uploadDashBoardViewModel.Footer = footerViewModel;

                this.GetViewResult(uploadDashBoardViewModel).ExecuteResult(context);
            }
            catch (Exception ex)
            {

            }

           // private UserProfile

        }
        private CategoryFilterViewModel BindDataForCategoryFilter()
        {
            var catFilterViewModel = new CategoryFilterViewModel();
            catFilterViewModel.Categories = _itemRepository.GetItemCategories().Select(x => x.ItemCategoryName).Distinct().ToList();
            catFilterViewModel.SubCategories1 = _itemRepository.GetItemCategories().Select(x => x.ItemSubCategory1).Distinct().ToList();
            catFilterViewModel.SubCategories2 = _itemRepository.GetItemCategories().Select(x => x.ItemSubCategory2).Distinct().ToList();
            return catFilterViewModel;
        }

    }
}
