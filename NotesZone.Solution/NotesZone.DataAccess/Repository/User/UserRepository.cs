using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Repository.User
{
    using NotesZone.DataAccess.Helper;
    using NotesZone.DataAccess.UnitOfWork;
    using NotesZone.Domain.Common;
    using NotesZone.Domain.Infrastructure;
    using NotesZone.Domain.User;
    using System.Data.Entity;
    public class UserRepository : Repository<User>, IUserRepository    
    {
        //public bool IsValiadUser;
        public User GetUserByUserName(string userName)
        {
           using( var dbContext = new NotesZoneDBContext())
           {
              // return dbContext.Users.Where(u => u.UserName.Equals(userName, StringComparison.InvariantCulture)).FirstOrDefault();
           }
           return null;
        }
        public User GetUserByEmail(string email)
        {
           using( var dbContext = new NotesZoneDBContext())
           {
               return dbContext.Users.Where(u => u.Email.Equals(email, StringComparison.InvariantCulture)).FirstOrDefault();
           }
        }

        public bool IsValidateUser(User user)
        {
            var password = Encripting.Encode(user.Password.Trim());
            using (var dbContext = new NotesZoneDBContext())
            {
                user = dbContext.Users.Where(u => u.Email.Equals(user.Email, StringComparison.CurrentCultureIgnoreCase) && u.Password.Equals(password, StringComparison.InvariantCulture)).FirstOrDefault();
            }
            
                if (user == null) return false;
                else return true;
        }

        public UserProfile GetUserProfile(string email)
        {
            var userProfile = new UserProfile();
            using( var dbContext = new NotesZoneDBContext() )
            {
                return dbContext.UserProfile.Where(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            }
            //retu
        }

        public bool SaveUser(User user)
        {
            var isSave = false;
            using(var dbContext = new NotesZoneDBContext())
            {
                var existingUser = dbContext.Users.Where(x => x.Email.Equals(user.Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                if (existingUser == null)
                {
                    dbContext.Set<User>().Add(user);
                    dbContext.Entry(user).State = EntityState.Added;
                    dbContext.SaveChanges();
                    isSave = true;
                }

                else 
                {
                    // Edit Logic here
                }
                
            }

            return isSave;
        }

        public bool SaveUserProfile(UserProfile userProfile)
        {
            var isSave = false;
            try
            {
                using (var dbContext = new NotesZoneDBContext())
                {
                    var existingUserProfile = dbContext.UserProfile.Where(x => x.Email.Equals(userProfile.Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                    if (existingUserProfile == null)
                    {
                        dbContext.Set<UserProfile>().Add(userProfile);
                        dbContext.Entry(userProfile).State = EntityState.Added;
                        dbContext.SaveChanges();
                        isSave = true;
                    }
                    else
                    {
                        // edit logic here
                    }
                }
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
            return isSave;
        }

    }
}
