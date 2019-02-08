using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels
{
    using NotesZone.Domain.Common;
    public class MainPageViewModel
    {
        public LeftColumnViewModelBase LeftColumn { get; set; }
        public MainPageRightColumnViewModel RightColumn { get; set; }
    }
    public abstract class LeftColumnViewModelBase
    {

    }

    public class MainPageLeftColumnViewModel : LeftColumnViewModelBase
    {
        public Item FirstItem { get; set; }
        public List<Item> RemainItems { get; set; }
        public PagingViewModel PagingData { get; set; }

        public MainPageLeftColumnViewModel()
        {
            this.RemainItems = new List<Item>();
            this.PagingData = new PagingViewModel();
        }
    }

    public class DetailsLeftColumnViewModel : LeftColumnViewModelBase
    {
        public Item CurrentItem { get; set; }

    }

    public class CategoryLeftColumnViewModel : LeftColumnViewModelBase
    {
        public List<Item> Items { get; set; }
        public PagingViewModel PagingData { get; set; }
        public ContactUsViewModel ContactUs { get; set; }
        public CategoryLeftColumnViewModel()
        {
            this.Items = new List<Item>();
            this.PagingData = new PagingViewModel();
            this.ContactUs = new ContactUsViewModel();
        }

    }
    public class MainPageRightColumnViewModel
    {
        public List<Item> LatestNews { get; set; }
        public List<Item> MostViews { get; set; }

        public MainPageRightColumnViewModel()
        {
            this.LatestNews = new List<Item>();
            this.MostViews = new List<Item>();
        }

    }

}
