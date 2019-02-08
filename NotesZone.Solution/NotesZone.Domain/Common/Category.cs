using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.Common
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public virtual IList<Item> Items { get; set; }
        public Category()
        {
            //this.Items = new List<Item>();
        }
    }
}
