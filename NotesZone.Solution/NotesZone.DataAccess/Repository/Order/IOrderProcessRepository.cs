using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Repository.Order
{
    using NotesZone.Domain.Address;
using NotesZone.Domain.Basket;
using NotesZone.Domain.User;
    public interface IOrderProcessRepository
    {
        /// <summary>
        ///  for e_books or to takr semail address of user and send to payment services
        /// </summary>
        /// <param name="basket"></param>
        /// <param name="shippingDetails"></param>
        void ProcessOrder(Basket basket, User user);
        void ProcessOrder(Basket basket, ShippingDetails shippingDetails);

        /// <summary>
        ///  for testing on local machine
        /// </summary>
        /// <param name="basket"></param>
        /// <param name="shippingDetails"></param>
        void EmailProcessOrder(Basket basket, ShippingDetails shippingDetails);
    }
}
