using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Config.Common
{
    using NotesZone.Domain.Common;
    public class CategoryMapping : EntityBaseMapping<Category>
    {
        public CategoryMapping()
        {
            this.Property(x => x.Name).IsRequired();

            //this.HasMany(x => x.Items).WithRequired(y => y.Category);

            this.ToTable("Category");
        }
    }
}
