using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.Common
{
    public class ItemCategory : EntityBase
    {
        public virtual string ItemCategoryName { get; set; } // such as RGPV tech
        //public virtual string ItemSubCategory1 { get; set; } // such as GEC tech
        public virtual string ItemSubCategory1 { get; set; } // such as semester
        public virtual string ItemSubCategory2 { get; set; } // such as physics
        //public virtual string Author { get; set; }
        public virtual Guid ItemCategoryKey { get; set; }
    }
}
