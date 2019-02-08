using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Config.Common
{
    using NotesZone.Domain.Common;
    public class SubjectMapping : EntityBaseMapping<Subject>
    {
        public SubjectMapping()
        {
            this.Property(x => x.Name);
        }
    }
}
