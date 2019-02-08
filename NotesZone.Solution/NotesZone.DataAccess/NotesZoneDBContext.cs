using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess
{
    using NotesZone.DataAccess.Config.Common;
    using NotesZone.DataAccess.Config.User;
    using NotesZone.Domain.Common;
    using NotesZone.Domain.User;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    public class NotesZoneDBContext : DbContext
    {
        public NotesZoneDBContext() : base("NotesZoneDB")
        {
            // Get the ObjectContext related to this DbContext
            var objectContext = (this as IObjectContextAdapter).ObjectContext;

            //objectContext.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings["DatabaseConnectionTimeout"]);
            //disabling lazy loading and proxy creation.
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;   
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemContent> ItemContents { get; set; }
        public DbSet<ItemRecord> ItemRecords { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions {get; set;}
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        //public DbSet<University> University { get; set; }
       // public DbSet<Subject> Subject { get; set; }
        public DbSet<ItemCategory> ItemCategory { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new CategoryMapping());
            modelBuilder.Configurations.Add(new ItemMapping());
            modelBuilder.Configurations.Add(new ItemContentMapping());
            modelBuilder.Configurations.Add(new SubscriptionMapping());
            modelBuilder.Configurations.Add(new UserSubscriptionMapping());
            //modelBuilder.Configurations.Add(new UniversityMapping());
            //modelBuilder.Configurations.Add(new SubjectMapping());
            modelBuilder.Configurations.Add(new ItemCategoryMapping());
            modelBuilder.Configurations.Add(new ItemRecordMapping());
            modelBuilder.Configurations.Add(new UserProfileMapping());
        }
    }
}
