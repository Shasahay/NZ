using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Config.User
{
    using NotesZone.Domain.User;
    public class UserProfileMapping : EntityBaseMapping<UserProfile>
    {
        public UserProfileMapping()
        {
            this.Property(x => x.FirstName).IsRequired();
            this.Property(x => x.LastName).IsRequired();
            this.Property(x => x.DisplayName);
            this.Property(x => x.Email).IsRequired();
            this.Property(x => x.University).IsOptional();
            this.Property(x => x.Institute).IsOptional();
            this.Property(x => x.EducationalBackground).IsOptional();
            this.Property(x => x.ProfessionalBackground).IsOptional();
            this.Property(x => x.Remark).IsOptional();
        }      
            
    }
}
