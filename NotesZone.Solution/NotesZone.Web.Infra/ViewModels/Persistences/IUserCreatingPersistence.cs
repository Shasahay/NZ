using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels.Persistences
{
    using NotesZone.Domain.User;
    public interface IUserCreatingPersistence
    {
        bool PersistenceUser(User user);
        bool PersistenceUserProfile(UserProfile userProfile);
    }
}
