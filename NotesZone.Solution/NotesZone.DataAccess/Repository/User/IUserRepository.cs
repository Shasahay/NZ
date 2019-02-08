using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Repository.User
{
    using NotesZone.Domain.User;
    using NotesZone.Domain.Common;
    public interface IUserRepository
    {
        User GetUserByUserName(string userName);
        User GetUserByEmail(string userName);
        bool IsValidateUser(User user);
        UserProfile GetUserProfile(string email);
        bool SaveUser(User user);
        bool SaveUserProfile(UserProfile userProfile);
       
    }
}
