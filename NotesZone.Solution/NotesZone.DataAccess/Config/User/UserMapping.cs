using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Config.User
{
    using NotesZone.Domain.User;
    public class UserMapping : EntityBaseMapping<User>
    {
        public UserMapping()
        {
           // this.Property(x => x.FirstName).IsRequired();
            //this.Property(x => x.LastName).IsRequired();
            //this.Property(x => x.DisplayName);
            this.Property(x => x.Password).IsRequired();
            this.Property(x => x.Email).IsRequired();

            this.ToTable("User");
        }
    }
}
