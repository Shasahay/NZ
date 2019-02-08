using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess
{
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
   public class NotesZoneDBInitializer : DbMigrationsConfiguration<NotesZoneDBContext>
   {
       public NotesZoneDBInitializer()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        public static void Initialize(string databaseName, string sqlServerInstance)
        {
            var databaseAutoMigrationSettings = ConfigurationManager.AppSettings["IsDatabaseAutoMigrationEnabled"];
            if (databaseAutoMigrationSettings == null || !Convert.ToBoolean(databaseAutoMigrationSettings)) return;

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NotesZoneDBContext, NotesZoneDBInitializer>("NotesZoneDB"));
            using (var ctx = new NotesZoneDBContext())
            {
                ctx.Database.Initialize(true);
            }
        }      
    }
}
