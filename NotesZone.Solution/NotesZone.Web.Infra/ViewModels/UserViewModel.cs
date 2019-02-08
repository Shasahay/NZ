using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    public class UserViewModel
    {
        [Required]
        [Display(Name = "FirstName")]
        [EmailAddress]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        [EmailAddress]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
