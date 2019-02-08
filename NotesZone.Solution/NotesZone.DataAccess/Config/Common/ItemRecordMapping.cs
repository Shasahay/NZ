using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Config.Common
{
    using NotesZone.Domain.Common;
    public class ItemRecordMapping : EntityBaseMapping<ItemRecord>
    {
        public ItemRecordMapping()
        {
            this.Property(x => x.ItemContentId).IsRequired();
            this.Property(x => x.NumOfView).IsOptional();
            this.Property(x => x.Rating).IsOptional();
            this.Property(x => x.NumOfDownload).IsOptional();
            this.HasRequired(x => x.ItemContent).WithMany(y => y.ItemRecord).HasForeignKey(key => key.ItemContentId); 
        }
    }
}
