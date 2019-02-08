using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Repository.User
{
    using NotesZone.Domain.Common;
    using NotesZone.Domain.Infrastructure;
    using NotesZone.Domain.User;

    
    public class UserSubscriptionRepository : IUserSubscriptionRepository
    {
        #region Working code
        public List<Subscription> GetSubscriptionbyUser(int userId)
        {
            var subscriptions = new List<Subscription>();
            using (var dbContext = new NotesZoneDBContext())
            {
                var subscriptionCollection = dbContext.UserSubscriptions.Where(u => u.UserId == userId).Join
                    (dbContext.Subscriptions,
                     us => us.Subscription.Id,
                     subs => subs.Id, (us, subs) => new { subs }
                    );

                foreach (var element in subscriptionCollection)
                {
                    subscriptions.Add(new Subscription { SubscriptionName = element.subs.SubscriptionName });
                }

            }
            return subscriptions;
        }
        public List<UserSubscription> GetSubscribeItembyUser(int userId)
        {
            List<UserSubscription> UserSubscriptions = new List<UserSubscription>();
            Item item = new Item();
            using (var dbContext = new NotesZoneDBContext())
            {
                var itemCollection = dbContext.UserSubscriptions.Where(u => u.UserId == userId).Join
                    (dbContext.Items,
                    us => us.Item.Id,
                    itm => item.Id,
                (us, itm) => new { us, itm }).Join
                (
                    dbContext.ItemContents,
                    it => it.itm.ItemContentId,
                    itmcon => itmcon.Id,
                    (it, itmcon) => new { it, itmcon }
                );
                foreach (var element in itemCollection)
                {
                    UserSubscriptions.Add(new UserSubscription { Item = element.it.itm, Subscription = element.it.us.Subscription });
                }
            }

            return UserSubscriptions;
        }
        public List<UserSubscription> GetSubscribeItembyUser(string email)
        {
            List<UserSubscription> UserSubscriptions = new List<UserSubscription>();
            Item item = new Item();
            using (var dbContext = new NotesZoneDBContext())
            {
                var itemCollection = dbContext.UserSubscriptions.Where(u => u.User.Email == email).Join
                    (dbContext.Items,
                    us => us.Item.Id,
                    itm => itm.Id,
                (us, itm) => new { us, itm }).Join
                (
                    dbContext.ItemContents,
                    it => it.itm.ItemContentId,
                    itmcon => itmcon.Id,
                    (it, itmcon) => new { it, itmcon }
                ).Join
                (
                    dbContext.Subscriptions,
                    sc => sc.it.us.Subscription.Id,
                    scn => scn.Id, (sc, scn) => new { sc, scn }
                );
                foreach (var element in itemCollection)
                {
                    UserSubscriptions.Add(new UserSubscription
                    {
                        Item = new Item
                        {
                            ItemContent = new ItemContent
                            {
                                Id = element.sc.itmcon.Id,
                                Title = element.sc.itmcon.Title,
                                SortDescription = element.sc.itmcon.SortDescription,
                                Price = element.sc.itmcon.Price,
                                Content = element.sc.itmcon.Content,
                                BigImage = element.sc.itmcon.BigImage,
                                SmallImage = element.sc.itmcon.SmallImage
                            }
                        },
                        Subscription = new Subscription
                        {
                            SubscriptionName = element.scn.SubscriptionName
                        },
                        ExpireDate = element.sc.it.us.ExpireDate
                    });
                }
            }

            return UserSubscriptions;
        }

        public List<UserSubscription> GetSubscribeItembyUser(string email, string subscriptionType)
        {
            List<UserSubscription> UserSubscriptions = new List<UserSubscription>();
            Item item = new Item();
            using (var dbContext = new NotesZoneDBContext())
            {
                var itemCollection = dbContext.UserSubscriptions.Where(u => u.User.Email == email).Join
                    (dbContext.Items,
                    us => us.Item.Id,
                    itm => itm.Id,
                (us, itm) => new { us, itm }).Join
                (
                    dbContext.ItemContents,
                    it => it.itm.ItemContentId,
                    itmcon => itmcon.Id,
                    (it, itmcon) => new { it, itmcon }
                ).Join
                (
                    dbContext.Subscriptions,
                    sc => sc.it.us.Subscription.Id,
                    scn => scn.Id, (sc, scn) => new { sc, scn }
                );
                foreach (var element in itemCollection)
                {
                    UserSubscriptions.Add(new UserSubscription
                    {
                        Item = new Item
                        {
                            ItemContent = new ItemContent
                            {
                                Id = element.sc.itmcon.Id,
                                Title = element.sc.itmcon.Title,
                                SortDescription = element.sc.itmcon.SortDescription,
                                Price = element.sc.itmcon.Price,
                                Content = element.sc.itmcon.Content,
                                BigImage = element.sc.itmcon.BigImage,
                                SmallImage = element.sc.itmcon.SmallImage
                            }
                        },
                        Subscription = new Subscription
                        {
                            SubscriptionName = element.scn.SubscriptionName
                        },
                        ExpireDate = element.sc.it.us.ExpireDate
                    }); ;
                }
            }

            if (subscriptionType == "Trial") // replace with switch and enum
                return UserSubscriptions.Where(t => t.Subscription.SubscriptionName.Equals("Trial", StringComparison.CurrentCultureIgnoreCase)).ToList();
            if (subscriptionType == "Upload")
                return UserSubscriptions.Where(t => t.Subscription.SubscriptionName.Equals("Upload", StringComparison.CurrentCultureIgnoreCase)).ToList();
            if (subscriptionType == "Download")
                return UserSubscriptions.Where(t => t.Subscription.SubscriptionName.Equals("Download", StringComparison.CurrentCultureIgnoreCase)).ToList();
            else
                return UserSubscriptions;
        }

        public List<UserSubscription> GetSubscriptionBySubscriptionType(List<UserSubscription> userSubscriptions, ApplicationVariable.SubscriptionType subscriptionType)
        {
            //if (subscriptionType.Equals(SubscriptionType.Trial, StringComparison.InvariantCultureIgnoreCase) // replace with switch and enum
            //    return userSubscriptions.Where(t => t.Subscription.SubscriptionName.Equals("Trial", StringComparison.CurrentCultureIgnoreCase)).ToList();
            //if (subscriptionType.Equals(SubscriptionType.Upload, StringComparison.InvariantCultureIgnoreCase))
            //    return userSubscriptions.Where(t => t.Subscription.SubscriptionName.Equals("Upload", StringComparison.CurrentCultureIgnoreCase)).ToList();
            //if (subscriptionType.Equals(SubscriptionType.Download, StringComparison.InvariantCultureIgnoreCase))
            //    return userSubscriptions.Where(t => t.Subscription.SubscriptionName.Equals("Download", StringComparison.CurrentCultureIgnoreCase)).ToList();
            //else
            //    return userSubscriptions;

            return userSubscriptions.Where(t => t.Subscription.SubscriptionName.Equals(subscriptionType.ToString(), StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        public bool SaveUserSubsctiprion(User user, Item item, Subscription subscription)
        {
            return true;
        } 
        #endregion




        public bool SaveUserSubcription(UserSubscription userSubscription)
        {
            throw new NotImplementedException();
        }
    }
}
