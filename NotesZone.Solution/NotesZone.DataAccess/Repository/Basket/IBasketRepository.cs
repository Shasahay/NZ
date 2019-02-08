using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Repository.Basket
{
    using NotesZone.Domain.Common;
    public interface IBasketRepository
    {
        void AddItem(ItemContent itemContent, int quantity);
        void RemowItem(ItemContent itemContent);
        decimal ComputeTotalValue();
        void Clear();
    }
}
