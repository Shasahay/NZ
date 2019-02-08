using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.Factory
{
    using NotesZone.Domain.Common;
    public class ItemCategoryFactory
    {
        public static ItemCategory CreateItemCategory(Guid itemCategoryKey, string createdBy, string itemCategoryName = null, string itemSubCategory1 = null, string itemSubCategory2 = null)
        {
            return new ItemCategory
            {
                ItemCategoryName = itemCategoryName,
                ItemSubCategory1 = itemSubCategory1,
                ItemSubCategory2 = itemSubCategory2,
                ItemCategoryKey = itemCategoryKey,
                CreatedDate = DateTime.Now,
                CreatedBy = createdBy
            };
        }

        public static ItemCategory CreateItemCategory()
        {
                return null;
        }

    }
}
