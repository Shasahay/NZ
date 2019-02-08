using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.Factory
{
    using NotesZone.Domain.Common;
    using NotesZone.Domain.Infrastructure;
    using NotesZone.Domain.User;
    public class UserSubscriptionFactory
    {
        public static List<UserSubscription> CreateUserSubscription(User user, List<ItemContent> itemContents, Subscription subscription, DateTime expiryDate = default(DateTime))
        {
            List<UserSubscription> userSubscriptions =  new List<UserSubscription>();
            //expiryDate you can write your logic to set the expiry date of different kind of subscription by dafault its null
            if (expiryDate == default(DateTime))
            {
                expiryDate = new DateTime(1987, 01, 05); // need to set later
            }
            
            var subscriptionId = NotesZone.Domain.Infrastructure.ApplicationVariable.dictonarySubscriptionType[subscription.SubscriptionName];

            foreach(var itemContent in itemContents)
            {
                userSubscriptions.Add(CreateUserSubscription(user.Email, itemContent.Id, subscriptionId, expiryDate, user.Email));
            }
           
            return userSubscriptions;
        }

        public static UserSubscription CreateUserSubscription(User user, ItemContent itemContent, Subscription subscription, DateTime expiryDate = default(DateTime))
        {
            List<UserSubscription> userSubscriptions = new List<UserSubscription>();
            //expiryDate you can write your logic to set the expiry date of different kind of subscription by dafault its null
            if (expiryDate == default(DateTime))
               expiryDate = new DateTime(1987, 01, 05); // need to set later
            
            var subscriptionId = NotesZone.Domain.Infrastructure.ApplicationVariable.dictonarySubscriptionType[subscription.SubscriptionName];
        
            return CreateUserSubscription(user.Email, itemContent.Id, subscriptionId, expiryDate, user.Email);
            }

        public static UserSubscription CreateUserSubscription( string email, int itemContentId, int subscriptionId, DateTime expieryDate, string createdBy)
        {
            return new UserSubscription
            {
                Email = email,
                ItemContentId = itemContentId,
                SubscriptionId = subscriptionId,
                ExpiryDate = expieryDate,
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now,

            };
        }

    }
}
