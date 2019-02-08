using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels
{
    using NotesZone.Domain.Common;
    public class CategoryFilterViewModel : ApplicationFilterBase
    {
        public List<string> Categories { get; set; }
        public List<string> SubCategories1 { get; set; }
        public List<string> SubCategories2 { get; set; }
        public List<ItemContent> ItemContents { get; set; }
    }
    public class SubCategoryFilterViewModel : ApplicationFilterBase
    {
        public List<string> SubCategories { get; set; }
        public List<ItemContent> ItemContents { get; set; }
    }
}
