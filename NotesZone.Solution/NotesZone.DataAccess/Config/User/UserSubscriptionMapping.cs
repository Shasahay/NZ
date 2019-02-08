using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Config.User
{
    using NotesZone.Domain.User;
    public class UserSubscriptionMapping : EntityBaseMapping<UserSubscription>
    {
        public UserSubscriptionMapping()
        {
            #region Previous working Code
            //this.HasRequired(x => x.User);
            //this.HasRequired(x => x.Item);
            //this.HasRequired(x => x.Subscription);
            //this.ToTable("UserSubscriptions"); 
            #endregion

            this.Property(x => x.UserId).IsRequired();
            this.Property(x => x.Email).IsRequired();
            this.Property(x => x.ItemContentId).IsRequired();
            this.Property(x => x.SubscriptionId).IsRequired();
            this.Property(x => x.ExpiryDate).IsOptional();
            this.ToTable("UserSubscriptions"); 
        }
        
    }
}
