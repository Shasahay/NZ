using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels
{
    public class UserDashboardViewModel
    {
        public HeaderViewModel Header { get; set; }
        //public AuthorProfileViewModel AuthorProfileViewModel { get; set; }
        public UserProfileViewModel UserProfileViewModel { get; set; }
        public FooterViewModel Footer { get; set; }
        public UserDashboardViewModel()
        {
            UserProfileViewModel = new UserProfileViewModel();
        }
    }
}
