
using NotesZone.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels
{
    using NotesZone.Domain.Common;
    using NotesZone.Domain.User;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    public class UploadDashboardViewModel
    {
        public ItemContentViewModel ItemContentViewModel { get; set; }
        public AuthorProfileViewModel AuthorProfileViewModel {get; set;}
        public HeaderViewModel Header { get; set; }
        public FilterViewModel Filter { get; set; }
        public FooterViewModel Footer { get; set; }
        //public ItemCategoryViewModel ItemCategoryViewModel {get; set; }
        public UploadDashboardViewModel()
        {
            AuthorProfileViewModel = new AuthorProfileViewModel();
            ItemContentViewModel = new ItemContentViewModel();
            //ItemCategoryViewModel = new ItemCategoryViewModel();  // not using this view model till now in place using Filter view Model serving the same purpose
        }
    }

    //public class AuthorProfileViewModel
    //{
    //    public UserProfile UserProfile { get; set; }
    //    public AuthorProfileViewModel()
    //    {
    //        UserProfile = new UserProfile();
    //    }
    //}

    public class ItemContentViewModel
    {
        public Item Item { get; set; }
        public ItemContent ItemContent { get; set; }
        [Required, FileExtensions(Extensions = "jpg, png", ErrorMessage = "Specify a jpg\\png file. (Comma-separated values)")]
        public HttpPostedFileBase SmallImage { get; set; }
        [Required, FileExtensions(Extensions = "pdf", ErrorMessage = "Specify a pdf file. (Comma-separated values)")]
        public HttpPostedFileBase DocumentFile { get; set; }
        public ItemContentViewModel()
        {
            ItemContent = new ItemContent();
        }
    }

    public class ItemCategoryViewModel
    {
        public ItemCategory ItemCategory {get; set;}
    }


}
