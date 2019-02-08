using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Repository.User
{
    using NotesZone.Domain.User;
    using NotesZone.Domain.Common;
    using NotesZone.Domain.Infrastructure;
    public interface IUserSubscriptionRepository
    {
        List<Subscription> GetSubscriptionbyUser(int userId);
        List<UserSubscription> GetSubscribeItembyUser(int userId);
        List<UserSubscription> GetSubscribeItembyUser(string email);
        List<UserSubscription> GetSubscribeItembyUser(string email, string subscriptionType);
        List<UserSubscription> GetSubscriptionBySubscriptionType(List<UserSubscription> userSubscriptions, ApplicationVariable.SubscriptionType subscriptionType);
        bool SaveUserSubsctiprion(User user, Item item, Subscription subscription);
    }
}
