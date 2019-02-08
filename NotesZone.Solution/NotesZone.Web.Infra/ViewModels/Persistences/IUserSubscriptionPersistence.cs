using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels.Persistences
{
    using NotesZone.Domain.User;
    public interface IUserSubscriptionPersistence
    {
        bool PersistenceUserSubscription(List<UserSubscription> userSubscriptions);
        bool PersistenceUserSubscription(UserSubscription userSubscriptions);
    }
}
