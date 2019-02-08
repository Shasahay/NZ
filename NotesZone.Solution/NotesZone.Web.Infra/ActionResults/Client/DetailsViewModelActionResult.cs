using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ActionResults.Client
{
    using NotesZone.DataAccess.Repository.Common;
    using NotesZone.Web.Infra.ViewModels;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    public class DetailsViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;
        private readonly int _itemId;
        private readonly int _numOfPage;
        private int _categoryId;
        public DetailsViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                int itemId)
            : this(viewNameExpression, itemId,
                   DependencyResolver.Current.GetService<ICategoryRepository>(),
                   DependencyResolver.Current.GetService<IItemRepository>())
        {
        }


        public DetailsViewModelActionResult(
            Expression<Func<TController, ActionResult>> viewNameExpression,
            int itemId,
            ICategoryRepository categoryRepository,
            IItemRepository itemRepository)
            : base(viewNameExpression)
        {
            // this._categoryRepository = categoryRepository;
            // this._itemRepository = itemRepository;
            this._categoryRepository = new CategoryRepository();
            this._itemRepository = new ItemRepository();
            this._numOfPage = 1;// this.ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
            this._itemId = itemId;
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);
            var cats = this._categoryRepository.GetCategories();
            var mainViewModel = new HomePageViewModel();
            var headerViewModel = new HeaderViewModel();
            var footerViewModel = new FooterViewModel();
            var mainPageViewModel = new MainPageViewModel();

            headerViewModel.SiteTitle = "Notes Zone : Your search ends here!!";
            if (cats != null)
            {
                headerViewModel.Categories = cats;//.ToList();
                footerViewModel.Categories = cats;//.ToList();
            }

            mainPageViewModel.LeftColumn = this.BindingDataForDetailsLeftColumnViewModel(this._itemId);
            mainPageViewModel.RightColumn = this.BindingDataForMainPageRightColumnViewModel();

            headerViewModel.CurrentCategoryId = this._categoryId;
            mainViewModel.Header = headerViewModel;
            mainViewModel.DashBoard = new DashboardViewModel();
            mainViewModel.Footer = footerViewModel;
            mainViewModel.MainPage = mainPageViewModel;

            this.GetViewResult(mainViewModel).ExecuteResult(context);
        }

        //public override void EnsureAllInjectInstanceNotNull()
        //{
        //    Guard.ArgumentNotNull(this._categoryRepository, "CategoryRepository");
        //    Guard.ArgumentNotNull(this._itemRepository, "ItemRepository");
        //    Guard.ArgumentMustMoreThanZero(this._numOfPage, "NumOfPage");
        //}

        #endregion

        #region private functions

        private MainPageRightColumnViewModel BindingDataForMainPageRightColumnViewModel()
        {
            var mainPageRightCol = new MainPageRightColumnViewModel();

            mainPageRightCol.LatestNews = this._itemRepository.GetMostDownload(numOfPage: this._numOfPage);
            mainPageRightCol.MostViews = this._itemRepository.GetMostViews(this._numOfPage);

            return mainPageRightCol;
        }

        private DetailsLeftColumnViewModel BindingDataForDetailsLeftColumnViewModel(int itemId)
        {
            var viewModel = new DetailsLeftColumnViewModel();

            var item = this._itemRepository.GetById(itemId);

            bool isNumOfViewUpdated = _itemRepository.SaveNumberofView(itemId);
            this._categoryId = item.Category.Id;

            //if (item == null)
            //throw new NoNullAllowedException(string.Format("Item id={0}", itemId).ToNotNullErrorMessage());

            viewModel.CurrentItem = item;

            return viewModel;
        }

        #endregion
    }
}
