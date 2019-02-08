using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Config.Common
{
    using NotesZone.Domain.Common;
    public class UniversityMapping : EntityBaseMapping<University>
    {
        public UniversityMapping()
        {
            this.Property(x => x.UniversityName).IsRequired();
            this.Property(x => x.Institute).IsOptional();
        }
    }
}
