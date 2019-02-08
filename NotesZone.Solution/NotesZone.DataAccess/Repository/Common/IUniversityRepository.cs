using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Repository.Common
{
    using NotesZone.Domain.Common;
    public interface IUniversityRepository
    {
         List<University> GetUniversity();
         List<University> GetInstitute(string university);
    }
}
