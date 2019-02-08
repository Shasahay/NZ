using NotesZone.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Repository.Email
{
    using System.Net;
    using System.Net.Mail;
    public class EmailRepository : IEmailRepository
    {
        private readonly EmailSettings emailSettings;

        public EmailRepository()
        {
            emailSettings = new EmailSettings();
        }
        public bool SendEmail()
        {
            throw new NotImplementedException();
        }

        
        public bool SendEmail(string toEmail, string message)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                = new NetworkCredential(emailSettings.Username,
                emailSettings.Password);
                MailMessage mailMessage = new MailMessage(
               toEmail, // From
               emailSettings.MailToAddress, // To
               "Contact Us :- Email", // Subject
               message.ToString()); // Body
                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailMessage);
                return true;

            }
        }

        public bool SendEmail(string firstName, string lastName, string fromEmail, string message)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                = new NetworkCredential(emailSettings.Username,
                emailSettings.Password);
                 if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                    = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                 StringBuilder body = new StringBuilder()
                 .AppendLine("A new Query has been submitted")
                 .AppendLine("---")
                 .AppendLine("First Name")
                            .AppendLine(firstName)
                            .AppendLine("---")
                            .AppendLine("Last Name")
                            .AppendLine(lastName)
                            .AppendLine("---")
                            .AppendLine(message)
                            .AppendLine("---");
                MailMessage mailMessage = new MailMessage(
                fromEmail, // From
                emailSettings.MailToAddress, // To
                "Contact Query submitted!", // Subject
                body.ToString()); // Body
                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailMessage);
                return true;
            }
        }

    }
}
