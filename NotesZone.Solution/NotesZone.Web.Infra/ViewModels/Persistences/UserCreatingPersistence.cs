using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels.Persistences
{
    using NotesZone.DataAccess.Repository.User;
using NotesZone.Domain.User;
    using System.Web.Mvc;
    public class UserCreatingPersistence : IUserCreatingPersistence
    {
        private readonly IUserRepository _userRepository;
        public UserCreatingPersistence() : this(DependencyResolver.Current.GetService<IUserRepository>())
        {

        }

        public UserCreatingPersistence(IUserRepository userRepository)
        {
            this._userRepository = new UserRepository();        
        }

        public bool PersistenceUser(User user)
        {
            return _userRepository.SaveUser(user);
        }

        public bool PersistenceUserProfile(UserProfile userProfile)
        {
            return _userRepository.SaveUserProfile(userProfile);
        }
    }
}
