using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.Basket
{
    using NotesZone.Domain.Common;
     public class Basket
     {
        private List<BasketLine> _lineCollection;
        public IList<BasketLine> Lines { get { return _lineCollection; } }
        public Basket()
        {
            _lineCollection = new List<BasketLine>();
        }
        public void AddItem( ItemContent itemContent, int quantity)
        {
            BasketLine line = _lineCollection.Where(i => i.ItemContent.Id == itemContent.Id).FirstOrDefault();
            if (line == null)
             _lineCollection.Add(new BasketLine { ItemContent = itemContent, Quantity = quantity });
            else
                line.Quantity += quantity;
        }

        public void RemowItem(ItemContent itemContent)
        {
            _lineCollection.RemoveAll(l => l.ItemContent.Id == itemContent.Id);
        }

        public decimal ComputeTotalValue()
        {
            return _lineCollection.Sum(t => t.ItemContent.Price * t.Quantity);
        }

        public void Clear()
        {
            _lineCollection.Clear();
        }
     }
}
