using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public class ContactUsViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Message { get; set; }
    }
}
