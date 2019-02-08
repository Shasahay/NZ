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
    public class HomePageViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;
        private readonly int _numOfPage;
        private readonly int _currentPage;
        public HomePageViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression)
            : this(viewNameExpression,
                   DependencyResolver.Current.GetService<ICategoryRepository>(),
                   DependencyResolver.Current.GetService<IItemRepository>())
        {
        }

        public HomePageViewModelActionResult(
            Expression<Func<TController, ActionResult>> viewNameExpression,
            ICategoryRepository categoryRepository,
            IItemRepository itemRepository)
            : base(viewNameExpression)
        {
            // this._categoryRepository = categoryRepository;
            // this._itemRepository = itemRepository;
            this._categoryRepository = new CategoryRepository();
            this._itemRepository = new ItemRepository();
            this._numOfPage = 3; //int.Parse(ConfigurationManager.GetAppConfigBy("NumOfPage"));
        }

        public override void ExecuteResult( ControllerContext context)
        {
          
            try
            {
                var cats = this._categoryRepository.GetCategories();
                var mainViewModel = new HomePageViewModel();
                var headerViewModel = new HeaderViewModel();
                var footerViewModel = new FooterViewModel();
                var mainPageViewModel = new MainPageViewModel();

                headerViewModel.SiteTitle = "NotesZone: Your search ends here";
                if (cats != null)
                {
                    headerViewModel.Categories = cats;
                    headerViewModel.CurrentCategoryId = cats.Where(x => x.Name.Equals("Home", StringComparison.CurrentCultureIgnoreCase)).Select(i => i.Id).First();
                    footerViewModel.Categories = cats;
                }

                mainPageViewModel.LeftColumn = this.BindingDataForMainPageLeftColumnViewModel();
                mainPageViewModel.RightColumn = this.BindingDataForMainPageRightColumnViewModel();
                mainViewModel.Header = headerViewModel;
                
                mainViewModel.Footer = footerViewModel;
                mainViewModel.DashBoard = new DashboardViewModel();
                mainViewModel.MainPage = mainPageViewModel;
                this.GetViewResult(mainViewModel).ExecuteResult(context);
            }
            catch (Exception ex)
            {
            }

        }

        private MainPageRightColumnViewModel BindingDataForMainPageRightColumnViewModel()
        {
            var mainPageRightCol = new MainPageRightColumnViewModel();
            mainPageRightCol.LatestNews = this._itemRepository.GetMostDownload(this._numOfPage);
            mainPageRightCol.MostViews = this._itemRepository.GetMostViews( numOfPage: this._numOfPage);
            return mainPageRightCol;
        }

        private MainPageLeftColumnViewModel BindingDataForMainPageLeftColumnViewModel()
        {
            var mainPageLeftCol = new MainPageLeftColumnViewModel();
            int numOfRecords;
            var items = this._itemRepository.GetItems(this._currentPage, this._numOfPage, out numOfRecords);
            mainPageLeftCol.RemainItems = items;

            #region Commenting since we are not using paging on home page if require format the string correctly it will work
            //mainPageLeftCol.PagingData = new PagingViewModel(
            //                                        this._currentPage,
            //                                        this._numOfPage,
            //                                        numOfRecords,
            //                                        string.Format(
            //                                            "{0}{1}{2}",
            //                                            "/Home/Category/", "/{page}"),
            //                                         null); 
            #endregion

            if (items != null)
            {
                var firstItem = items.First();

                //if (firstItem == null)
                // throw new NoNullAllowedException("First Item".ToNotNullErrorMessage());

                // if (firstItem.ItemContent == null)
                //throw new NoNullAllowedException("First ItemContent".ToNotNullErrorMessage());

                mainPageLeftCol.FirstItem = firstItem;

                if (items.Count() > 1)
                {
                    mainPageLeftCol.RemainItems = items.Where(x => x.ItemContent != null && x.Id != mainPageLeftCol.FirstItem.Id).ToList();
                }
            }

            return mainPageLeftCol;
        }
    }
}
