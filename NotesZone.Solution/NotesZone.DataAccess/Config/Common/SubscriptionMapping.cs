using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Config.Common
{
    using NotesZone.Domain.Common;
    public class SubscriptionMapping : EntityBaseMapping<Subscription>
    {
        public SubscriptionMapping()
        {
            this.Property(sn => sn.SubscriptionName);
        }
    }
}
