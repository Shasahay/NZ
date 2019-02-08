using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Repository.Order
{
    using NotesZone.Domain.Address;
    using NotesZone.Domain.Basket;
    using NotesZone.Domain.Infrastructure;
    using NotesZone.Domain.User;
    using System.Net;
    using System.Net.Mail;
    public class OrderProcessRepository : IOrderProcessRepository
    {
        private readonly EmailSettings emailSettings;

        public OrderProcessRepository()
        {
            emailSettings = new EmailSettings();
        }

        // This constructor can be deleted latar
        public OrderProcessRepository( EmailSettings setting)
        {
            emailSettings = setting;
        }
        public void ProcessOrder(Basket basket, User user)
        {
            throw new NotImplementedException();
        }

        public void ProcessOrder(Domain.Basket.Basket basket, Domain.Address.ShippingDetails shippingDetails)
        {
            throw new NotImplementedException();
        }

        public void EmailProcessOrder(Basket basket, ShippingDetails shippingDetails)
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
                .AppendLine("A new order has been submitted")
                .AppendLine("---")
                .AppendLine("Items:");
                foreach (var line in basket.Lines)
                {
                    var subtotal = line.ItemContent.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c}", line.Quantity,
                    line.ItemContent.Title,
                    subtotal);
                }

                body.AppendFormat("Total order value: {0:c}", basket.ComputeTotalValue())
                            .AppendLine("---")
                            .AppendLine("Ship to:")
                            .AppendLine(shippingDetails.Name)
                            .AppendLine(shippingDetails.Line1)
                            .AppendLine(shippingDetails.Line2 ?? "")
                            .AppendLine(shippingDetails.Line3 ?? "")
                            .AppendLine(shippingDetails.City)
                            .AppendLine(shippingDetails.State ?? "")
                            .AppendLine(shippingDetails.Country)
                            .AppendLine(shippingDetails.Zip)
                            .AppendLine("---")
                            .AppendFormat("Gift wrap: {0}",
                            shippingDetails.GiftWrap ? "Yes" : "No");
                MailMessage mailMessage = new MailMessage(
                emailSettings.MailFromAddress, // From
                emailSettings.MailToAddress, // To
                "New order submitted!", // Subject
                body.ToString()); // Body
                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailMessage);

            }
        }

    }

    
}