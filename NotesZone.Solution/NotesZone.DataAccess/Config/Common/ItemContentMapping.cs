using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Config.Common
{
    using NotesZone.Domain.Common;
    public class ItemContentMapping : EntityBaseMapping<ItemContent>
    {
        public ItemContentMapping()
        {
            this.Property(x => x.Title).IsRequired();
            this.Property(x => x.SortDescription).IsRequired();
            this.Property(x => x.Content).IsRequired();
            this.Property(x => x.SmallImage).IsOptional();
            this.Property(x => x.MediumImage).IsOptional();
            this.Property(x => x.BigImage).IsOptional();
            this.Property(x => x.FileName).IsOptional();
            this.Property(x => x.Price).IsOptional();
            this.Property(x => x.Author).IsOptional();
            this.Property(x => x.ItemCategoryKey).IsRequired();
            this.Property(x => x.IsActive).IsRequired();
            this.Property(x => x.IsDeleted).IsRequired();
            this.Property(x => x.DocumentName).IsOptional();
            this.HasMany(x => x.Items).WithRequired(y => y.ItemContent);
            this.ToTable("ItemContent");
        }
    }
}
