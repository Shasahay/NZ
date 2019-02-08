using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.Factory
{
    using NotesZone.Domain.User;
    public class UserProfileFactory
    {
        public static UserProfile CreateUserProfile( string firstName, string lastName, string email)
        {
            //return CreateUserProfile(firstName, lastName, email, null, null, null, null, null, null,2); // by default setting register user Role = 2; working code
            return CreateUserProfile(firstName, lastName, email, null, null, null, null, null, null, (int)Role.RegisterUser); // by default setting register user Role = RegisterUser need to test this code
        }

        public static UserProfile CreateUserProfile(string firstName, string lastName, string email, string displayName, string university, string institute, string educationalBackground, string professionalBackgroung, string remark, int role)
        {
               var varDisplayName = displayName == null ? string.Concat(firstName, " ", lastName) : displayName;
               return new UserProfile
               {
                   FirstName = firstName,
                   LastName = lastName,
                   DisplayName = varDisplayName,
                   Email = email,
                   University = university,
                   Institute = institute,
                   EducationalBackground = educationalBackground,
                   ProfessionalBackground = professionalBackgroung,
                   Remark = remark,
                   Role = role,
                   CreatedDate = DateTime.Now,
                   CreatedBy = varDisplayName
               };
        }
    }
}
