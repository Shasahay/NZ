using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.Factory
{
    using NotesZone.Domain.Common;
    public class ItemContentFactory
    {
        public static ItemContent CreateItemContent(string title, string shortDes, string content)
        {
            return CreateItemContent(title, shortDes, content, null, null, null);
        }

        public static ItemContent CreateItemContent(string title, string shortDes, string content, string smallImagePath, string mediumImagePath, string largeImagePath)
        {
            Guid itemCategoryKey = Guid.NewGuid();
            return new ItemContent
            {
                Title = title,
                SortDescription = shortDes,
                Content = content,
                SmallImage = smallImagePath,
                MediumImage = mediumImagePath,
                BigImage = largeImagePath,
                ItemCategoryKey = itemCategoryKey
            };
        }

        public static ItemContent CreateItemContent(string title, string shortDes, string content, string smallImagePath, string createdBy)
        {
            Guid itemCategoryKey = Guid.NewGuid();
            return new ItemContent
            {
                Title = title,
                SortDescription = shortDes,
                Content = content,
                SmallImage = smallImagePath,
                ItemCategoryKey = itemCategoryKey,
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now,
                IsActive = false,
                IsDeleted = false,
                IsAnnouncement = false
            };
        }
    }
}
