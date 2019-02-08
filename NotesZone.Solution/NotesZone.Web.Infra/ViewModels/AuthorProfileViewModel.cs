using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels
{
    using NotesZone.Domain.User;
    public class AuthorProfileViewModel
    {
        public UserProfile UserProfile { get; set; }
            public AuthorProfileViewModel()
            {
                UserProfile = new UserProfile();
            }
    }
}
