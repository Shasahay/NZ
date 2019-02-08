using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels
{
    using NotesZone.Domain.User;
    public class UserProfileViewModel
    {
        public UserProfile UserProfile { get; set; }
        public string Password {get; set;}
        public UserProfileViewModel()
        {
            UserProfile = new UserProfile();
        }
    }
}
