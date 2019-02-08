using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ActionResults.Client
{
    using NotesZone.DataAccess.Repository.Common;
    using NotesZone.Domain.Common;
    using NotesZone.Web.Infra.ViewModels;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    public class CategoryViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;
        private readonly int _categoryId;
        private readonly int _currentPage;
        private readonly int _numOfPage;
        private readonly string _searchCategory;
        private readonly string _searchSubCategory1;

        public CategoryViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                int categoryId, int currentPage, string searchCategory, string searchSubCategory1)
            : this(viewNameExpression, categoryId,currentPage, searchCategory, searchSubCategory1,
                   DependencyResolver.Current.GetService<ICategoryRepository>(),
                   DependencyResolver.Current.GetService<IItemRepository>())
        {
        }

        public CategoryViewModelActionResult(
                    Expression<Func<TController, ActionResult>> viewNameExpression,
                    int categoryId,int currentPage,string searchCategory, string searchSubCategory1,
                    ICategoryRepository categoryRepository,
                    IItemRepository itemRepository)
            : base(viewNameExpression)
        {
            this._categoryRepository = new CategoryRepository();
            this._itemRepository = new ItemRepository();
            this._numOfPage = 5;//int.Parse(ConfigurationManager.GetAppConfigBy("NumOfPage")); 
            this._currentPage = currentPage;
            this._categoryId = categoryId;
            this._searchCategory = searchCategory;
            this._searchSubCategory1 = searchSubCategory1;
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
            var filterViewModel = new FilterViewModel();

            if (cats != null && cats.Any())
            {
                headerViewModel.Categories = cats.ToList();
                headerViewModel.CurrentCategoryId = this._categoryId;
                footerViewModel.Categories = cats.ToList();
            }
            mainPageViewModel.LeftColumn = this.BindingDataForCategoryLeftColumnViewModel(this._categoryId);
            mainPageViewModel.RightColumn = this.BindingDataForMainPageRightColumnViewModel();

            var items = ((CategoryLeftColumnViewModel)mainPageViewModel.LeftColumn).Items;
            if (items != null && items.Count > 0)
            {
                headerViewModel.SiteTitle = string.Format("Notes Zone - {0} Section", items.FirstOrDefault().Category.Name);
                if (items.FirstOrDefault().Category.Name.Equals("Notes", StringComparison.CurrentCultureIgnoreCase))
                {
                    filterViewModel.AuthorFilter = this.BindDataForAuthorFilter(this._categoryId);
                    filterViewModel.CategoryFilter = this.BindDataForCategoryFilter(this._categoryId);
                }
            }
            else
                headerViewModel.SiteTitle = "Notes Zone : Your search ends here !!";

            mainViewModel.Header = headerViewModel;
            mainViewModel.Filter = filterViewModel;
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
        //    Guard.ArgumentMustMoreThanZero(this._categoryId, "CategoryId");
        //}

        #endregion

        #region private functions

        private CategoryLeftColumnViewModel BindingDataForCategoryLeftColumnViewModel(int categoryId)
        {
            var viewModel = new CategoryLeftColumnViewModel();
            int numOfRecords;
            var items = new List<Item>();
            if (string.IsNullOrEmpty(this._searchCategory) &&  string.IsNullOrEmpty(this._searchSubCategory1))
            {
                items = this._itemRepository.GetItemByCategory(this._currentPage, this._numOfPage, out numOfRecords, categoryId);
                viewModel.PagingData = new PagingViewModel(
                                                    this._currentPage,
                                                    this._numOfPage,
                                                    numOfRecords,
                                                    string.Format(
                                                        "{0}{1}{2}",
                                                        "/Home/Category/", categoryId, "/{page}"),
                                                     null);
            }
            else
            {
                items = this._itemRepository.GetItemByCategory(this._currentPage, this._numOfPage, out numOfRecords, categoryId, this._searchCategory, this._searchSubCategory1);
                viewModel.PagingData = new PagingViewModel(
                                                    this._currentPage,
                                                    this._numOfPage,
                                                    numOfRecords,
                                                    string.Format(
                                                        "{0}{1}{2}{3}{4}{5}{6}",
                                                        "/Home/Category/", categoryId, "/{page}","/", this._searchCategory,"/", this._searchSubCategory1),
                                                     null);
            }
            //if (items == null)
            //    throw new NoNullAllowedException("Items".ToNotNullErrorMessage());
            viewModel.ContactUs = new ContactUsViewModel();
            
            if (items.Any())
            {
                viewModel.Items = items.ToList();
            }

            return viewModel;
        }

        private MainPageRightColumnViewModel BindingDataForMainPageRightColumnViewModel()
        {
            var mainPageRightCol = new MainPageRightColumnViewModel();

            mainPageRightCol.LatestNews = this._itemRepository.GetMostDownload(numOfPage: this._numOfPage).ToList();
            mainPageRightCol.MostViews = this._itemRepository.GetMostViews(numOfPage: this._numOfPage).ToList();

            return mainPageRightCol;
        }

        private AuthorFilterViewModel BindDataForAuthorFilter (int categoryId)
        {
            var authorfilterViewModel = new AuthorFilterViewModel();
            var _itemCon = _itemRepository.GetItemByCategory(categoryId);
            var authors =  _itemCon.Select(a => a.ItemContent.Author).Distinct().ToList();
            authorfilterViewModel.Authors = authors;
            return authorfilterViewModel;
        }

        private CategoryFilterViewModel BindDataForCategoryFilter(int categoryId)
        {
            var catFilterViewModel = new CategoryFilterViewModel();
            //var catItemFilter = 
            catFilterViewModel.Categories = _itemRepository.GetItemCategories().Select(x => x.ItemCategoryName).Distinct().ToList();
            catFilterViewModel.SubCategories1 = _itemRepository.GetItemCategories().Select(x => x.ItemSubCategory1).Distinct().ToList();
            catFilterViewModel.SubCategories2 = _itemRepository.GetItemCategories().Select(x => x.ItemSubCategory2).Distinct().ToList();
            return catFilterViewModel;
        }

        #endregion
    }
}
