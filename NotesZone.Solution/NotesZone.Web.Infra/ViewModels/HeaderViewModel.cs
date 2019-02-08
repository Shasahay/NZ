using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels
{
    using NotesZone.Domain.Common;
    public class HeaderViewModel
    {
        public string SiteTitle { get; set; }
        public int CurrentCategoryId { get; set; }
        public List<Category> Categories { get; set; }
        public HeaderViewModel()
        {
            this.Categories = new List<Category>(); 
        }
    }
}
