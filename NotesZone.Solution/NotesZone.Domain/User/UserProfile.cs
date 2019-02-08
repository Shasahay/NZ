using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.User
{
    using System.ComponentModel.DataAnnotations;
    public class UserProfile : EntityBase
    {
        [Required(ErrorMessage = "Please enter a first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a last name")]
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Please enter a E-mail")]
        public string Email { get; set; }
        public int Role { get; set; }
        public string  University { get; set; }
        public string Institute { get; set; }
        public string EducationalBackground { get; set; }
        public string ProfessionalBackground { get; set; }
        public string Remark { get; set; }
        public virtual User user { get; set; }
        public Role RoleEnum
        {
            get { return (Role)this.Role; }
        }

        //public UserProfile()
        //{
        //    user = new User();
        //    this.DisplayName = string.Concat(this.FirstName, this.LastName);
        //}
       
    }

    public enum Role : int
    {
        Anonymous = 1,
        RegisterUser = 2,
        Administrator = 3
    }
}
