using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels
{
    using NotesZone.Domain.Common;
    public class FooterViewModel
    {
        public List<Category> Categories { get; set; }
        public FooterViewModel()
        {
            this.Categories = new List<Category>();
        }

    }
}
