
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.Factory
{
    using NotesZone.Domain.Common;
    public class ItemFactory
    {
        public static Item CreateItem(string createdBy, Category category, ItemContent itemContent)
        {
            return CreateItem(-1, createdBy, category, itemContent);
        }

        public static Item CreateItem(int id, string createdBy)
        {
            return CreateItem(id, createdBy, new Category(), new ItemContent());
        }
        /// <summary>
        /// Use if new category is alos created might be in future
        /// </summary>
        /// <param name="id"></param>
        /// <param name="createdBy"></param>
        /// <param name="category"></param>
        /// <param name="itemContent"></param>
        /// <returns></returns>
        public static Item CreateItem(int id, string createdBy, Category category, ItemContent itemContent)
        {
            return new Item
            {
                Id = id,
                CreatedBy = createdBy,
                Category = category,
                ItemContent = itemContent
            };
        }

        public static Item CreateItem(int id, string createdBy, ItemContent itemContent)
        {
            return new Item
            {
                Id = id,
                CreatedBy = createdBy,
                ItemContent = itemContent
            };
        }

        public static Item CreateItem(string createdBy, ItemContent itemContent, int itemCategoryId)
        {
            
            return new Item
            {
                CreatedBy = createdBy,
                ItemContent = itemContent,
                ItemCategoryId = itemCategoryId,
                CreatedDate = DateTime.Now,
            };
        }

        
    }
}
