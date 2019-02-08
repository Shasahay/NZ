using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels
{
    using NotesZone.Domain.Common;

    public abstract class ApplicationFilterBase
    {

    }
    public class AuthorFilterViewModel : ApplicationFilterBase
    {
        public List<string> Authors { get; set; }
        public List<ItemContent> ItemContents { get; set; }
        
    }

}
