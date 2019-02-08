using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Config
{
    using NotesZone.Domain;
    using System.Data.Entity.ModelConfiguration;
    public class EntityBaseMapping<T> : EntityTypeConfiguration<T> where T : EntityBase
    {
        public EntityBaseMapping()
        {
            Property(b => b.CreatedDate).IsRequired();
            Property(b => b.CreatedBy).IsRequired();
            Property(b => b.CreatedBy).HasColumnType("VARCHAR").HasMaxLength(50);
            Property(b => b.ModifiedDate).IsOptional();
            Property(b => b.ModifiedBy).IsOptional();
            Property(b => b.ModifiedBy).HasColumnType("VARCHAR").HasMaxLength(50);
        }
    }
}
