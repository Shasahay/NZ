using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Config.Common
{
    using NotesZone.Domain.Common;
    public class ItemCategoryMapping : EntityBaseMapping<ItemCategory>
    {
        public ItemCategoryMapping()
        {
            this.Property(x => x.ItemCategoryName).IsRequired();
            this.Property(x => x.ItemSubCategory1).IsOptional();
            this.Property(x => x.ItemSubCategory2).IsOptional();
            //this.Property(x => x.Author).IsOptional();
            this.Property(x => x.ItemCategoryKey).IsOptional();
        }
    }
}
