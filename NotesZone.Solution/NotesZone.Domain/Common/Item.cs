using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.Common
{
    using NotesZone.Domain.User;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Item : EntityBase
    {
        public Item()
        {
            // this.Category = new Category();  // Enable in case catagory insertation also require with Item
            this.ItemContent = new ItemContent();
            //this.User = new User();
        }
        public virtual int ItemContentId { get; set; }
        public virtual int ItemCategoryId { get; set; }
        public virtual Category Category { get; set; }
        [ForeignKey("ItemCategoryId")]
        public virtual ItemContent ItemContent { get; set; }
        //public virtual User User{get; set;}
    }
}
