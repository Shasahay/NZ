using NotesZone.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.User
{
    using System.ComponentModel.DataAnnotations;
    public class User : EntityBase
    {
        //[Required(ErrorMessage = "Please enter a name")]
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //[Required(ErrorMessage = "Please enter Display name")]
        //public string DisplayName { get; set; }
        //[Required(ErrorMessage = "Please enter Password")]
        //public string Password { get; set; }
        //[Required(ErrorMessage = "Please enter a E-mail")]
        //public int Role { get; set; }
        [Required(ErrorMessage = "Please enter a E-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }
        public virtual List<Item> Items {get; set;}
       // public virtual IList<UserSubscription> UserSubscription { get; set; }
        
        
        public User()
        {
            this.Items = new List<Item>();
            //this.UserSubscription = new List<UserSubscription>();
            
        }

    }

    
}
