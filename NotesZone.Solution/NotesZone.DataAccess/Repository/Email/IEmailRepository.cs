using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Repository.Email
{
    public interface IEmailRepository
    {
         bool SendEmail();
         bool SendEmail(string toEmail, string fromEmail, string subject, string meaasage);
         bool SendEmail(string toEmail, string message);
    }
}
