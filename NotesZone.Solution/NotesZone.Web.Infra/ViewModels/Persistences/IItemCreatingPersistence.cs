using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels.Persistences
{
    using NotesZone.Domain.Common;
    public interface IItemCreatingPersistence
    {
        bool PersistenceItem(Item item);
        bool PersistenceItemCategory(ItemCategory itemCategory);
    }
}
