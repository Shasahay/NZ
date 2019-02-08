using NotesZone.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.Basket
{
    public class BasketLine
    {
        public ItemContent ItemContent { get; set; }
        public int Quantity { get; set; }
    }
}
