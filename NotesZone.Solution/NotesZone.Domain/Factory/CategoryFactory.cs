using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.Factory
{
    using NotesZone.Domain.Common;
    public class CategoryFactory
    {
        public static Category CreateCategory(int id)
        {
            return CreateCategory(id, null, null, DateTime.Now);
        }

        public static Category CreateCategory(int id, string name, string createdBy, DateTime createdDate)
        {
            return new Category
            {
                Id = id,
                Name = name,
                CreatedBy = createdBy,
                CreatedDate = createdDate
            };
        }
    }
}
