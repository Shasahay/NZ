using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels
{
    public class FilterViewModel
    {
        public AuthorFilterViewModel AuthorFilter { get; set; }
        public CategoryFilterViewModel CategoryFilter { get; set; }
    }
}
