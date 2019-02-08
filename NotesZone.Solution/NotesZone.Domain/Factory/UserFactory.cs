using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.Factory
{
    using NotesZone.Domain.Infrastructure;
    using NotesZone.Domain.User;
    using System.IO;
    using System.Security.Cryptography;
    public class UserFactory
    {
        //string en = Encripting.Encode("");
        public static User CreateUser(string email, string password)
        {
            string hashPassword = Encripting.Encode(password.Trim());
            return new User
            {
                Email = email,
                Password = hashPassword,
                CreatedBy = email,
                CreatedDate = DateTime.Now
            };

        }

        

        


    }
}
