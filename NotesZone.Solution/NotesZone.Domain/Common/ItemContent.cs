using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.Common
{
    public class ItemContent : EntityBase
    {
        public ItemContent()
        {
            this.Items = new List<Item>();
            IsActive = true;
            IsDeleted = false;
        }
        public string Title { get; set; }
        public string SortDescription { get; set; }
        public string Content { get; set; }
        public string SmallImage { get; set; }
        public  string MediumImage { get; set; }
        public  string BigImage { get; set;}
        public string FileName { get; set; }
        public  string Author { get; set; }
        //public  long NumOfView { get; set; }
        public decimal Price { get; set; }
        public Guid  ItemCategoryKey {get; set;}
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAnnouncement { get; set; }
        public virtual IList<Item> Items { get; set; }
        public string DocumentName { get; set; }
        public virtual IList<ItemRecord> ItemRecord { get; set; }  // to make foreign key and one to many relationship
    }
}
