using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Repository.Common
{
    using NotesZone.DataAccess.UnitOfWork;
    using NotesZone.Domain.Common;
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        public List<Category> GetCategories()
        {
            using (var dbContext = new NotesZoneDBContext())
            {
                return dbContext.Categories.ToList<Category>();
            }
        }

        public Category GetCategoryById(int id)
        {

            using (var dbContext = new NotesZoneDBContext())
            {
                return dbContext.Categories.Where(c => c.Id == id).FirstOrDefault();
            }
        }
    }
}
