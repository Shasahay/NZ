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
    using System.Data.Entity;

    
    public class UserSubscriptionRepository : IUserSubscriptionRepository
    {
        
        public bool SaveUserSubcription(UserSubscription userSubscription)
        {
            bool isSave = false;
            using(var dbContext = new NotesZoneDBContext())
            {
                try
                {
                    var user = dbContext.Users.Where(x => x.Email.Equals(userSubscription.Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                    userSubscription.UserId = user.Id;
                    dbContext.Set<UserSubscription>().Add(userSubscription);
                    dbContext.Entry(userSubscription).State = EntityState.Added;
                    dbContext.SaveChanges();
                    isSave = true;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
            }
            return isSave;
        }

        public List<Subscription> GetSubscriptionbyUser(int userId)
        {
            throw new NotImplementedException();
        }

        public List<UserSubscription> GetSubscribeItembyUser(int userId)
        {
            throw new NotImplementedException();
        }

        public List<UserSubscription> GetSubscribeItembyUser(string email)
        {
            using(var dbContext = new NotesZoneDBContext())
            {
                return dbContext.UserSubscriptions.Where(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
        }

        public List<UserSubscription> GetSubscribeItembyUser(string email, string subscriptionType)
        {
            var userSubscriptions = GetSubscribeItembyUser(email);
            var lstUserSubscription = new List<UserSubscription>();
            using (var dbContext = new NotesZoneDBContext())
            {
                // If Subscriptiontype taking from DB 
                //var subscriptionId = dbContext.Subscriptions.Where( x=> x.SubscriptionName.Equals( subscriptionType, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                // present following :- setting\ hardcoding Subscriptiontype from code 
                var subscriptionId = ApplicationVariable.dictonarySubscriptionType[subscriptionType];
                var itemCollection = dbContext.UserSubscriptions.Where(us => us.SubscriptionId == subscriptionId && us.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase)).Join
                    (dbContext.ItemContents.Where(i => i.IsActive && !i.IsDeleted),
                        userSubs => userSubs.ItemContentId,
                        itemContent => itemContent.Id, (userSubs, itemContent) => new { userSubs, itemContent }
                    ).ToList();

                foreach (var subscription in itemCollection)
                {

                    var lstItemContent = new List<ItemContent>() {  new ItemContent
                        {
                            Id = subscription.itemContent.Id,
                            Title = subscription.itemContent.Title,
                            Author = subscription.itemContent.Author,
                            SortDescription = subscription.itemContent.SortDescription,
                            Content = subscription.itemContent.Content,
                            SmallImage = subscription.itemContent.SmallImage,
                            FileName = subscription.itemContent.FileName
                        }};

                    lstUserSubscription.Add(new UserSubscription
                    {
                        ItemContents = lstItemContent,
                        ExpiryDate = subscription.userSubs.ExpiryDate
                    }
                                             );
                }
            }
            return lstUserSubscription;

        }

        public List<UserSubscription> GetSubscriptionBySubscriptionType(List<UserSubscription> userSubscriptions, ApplicationVariable.SubscriptionType subscriptionType)
        {
            throw new NotImplementedException();
        }
    }
}
