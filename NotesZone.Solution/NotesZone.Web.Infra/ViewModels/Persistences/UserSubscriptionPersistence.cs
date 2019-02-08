using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels.Persistences
{
    using NotesZone.DataAccess.Repository.User;
    using NotesZone.Domain.Common;
    using NotesZone.Domain.User;
    using System.Web.Mvc;
    public class UserSubscriptionPersistence : IUserSubscriptionPersistence
    {
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;
        public UserSubscriptionPersistence() : this(DependencyResolver.Current.GetService<IUserSubscriptionRepository>())
        {
            
        }
        public UserSubscriptionPersistence(IUserSubscriptionRepository userSubscriptionRepository)
        {
            //this._userSubscriptionRepository = userSubscriptionRepository; if sending reference from caller
            this._userSubscriptionRepository = new UserSubscriptionRepository();
        }
       
        bool IUserSubscriptionPersistence.PersistenceUserSubscription(List<UserSubscription> userSubscriptions)
        {
            
            bool IsSave = false;
            foreach(var userSubscription in userSubscriptions)
            {
                _userSubscriptionRepository.SaveUserSubcription(userSubscription);
                IsSave = true;
            }
            return IsSave;
        }

        bool IUserSubscriptionPersistence.PersistenceUserSubscription(UserSubscription userSubscription)
        {   
            return _userSubscriptionRepository.SaveUserSubcription(userSubscription);
         
        }
    }
}
