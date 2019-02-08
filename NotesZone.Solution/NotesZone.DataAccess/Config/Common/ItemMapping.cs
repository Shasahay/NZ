using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Config.Common
{
    using NotesZone.Domain.Common;
    public class ItemMapping : EntityBaseMapping<Item>
    {   
        public ItemMapping()
        {
            this.Property(x => x.ItemContentId);

            this.HasRequired(x => x.Category).WithMany(y => y.Items).HasForeignKey( key=> key.ItemCategoryId) ;  // Enable in case catagory insertation also requir with Item 
            //this.HasRequired(u => u.User).WithMany(i => i.Items);
            this.HasRequired(x => x.ItemContent).WithMany(x => x.Items).HasForeignKey(key => key.ItemContentId);  // one to many mapping
            this.ToTable("Item");
        }
    }
}
