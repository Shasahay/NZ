using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.User
{
    using NotesZone.Domain.Common;
    using System.ComponentModel.DataAnnotations.Schema;
    public class UserSubscription : EntityBase
    {
        #region Previous Working Code
        //public virtual Item Item { get; set; }
        //public virtual User User { get; set; }
        //public virtual Subscription Subscription { get; set; }
        //public virtual DateTime ExpireDate { get; set; }
        //// public virtual List<ItemContent> ItemContents { get; set; }
        //public int UserId { get; set; }
        //public UserSubscription()
        //{
        //    this.Item = new Item();
        //    this.User = new User();
        //    this.Subscription = new Subscription();
        //    //   this.ItemContents = new List<ItemContent>();
        //} 
        #endregion
        public virtual int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual string Email { get; set; }
        public virtual int ItemContentId { get; set; }
        public virtual int SubscriptionId { get; set; }
        public virtual DateTime ExpiryDate { get; set; }
        [NotMapped]
        public virtual Item Item { get; set; }
        [NotMapped]
        public List<ItemContent> ItemContents { get; set; }
        public UserSubscription()
        {

        }
    }
}
