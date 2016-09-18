using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LearningSignalR.Db.Models.Identity
{
    public class UserLogin : IdentityUserLogin<long>
    {
        public long Id { get; set; }

        internal static void AddModelDefinition(DbModelBuilder modelBuilder, string tableName = "UserLogins", string schemaName = "dbo")
        {
            var model = modelBuilder.Entity<UserLogin>();
            model.ToTable(tableName, schemaName);

            model.IdentityColumn(x => x.Id, "Id", 0);
            model.Column(x => x.UserId, "UserId", 1);
            model.StringColumn(x => x.LoginProvider, "LoginProvider", 2);
            model.StringColumn(x => x.ProviderKey, "ProviderKey", 3, true, DbConstants.BigStringLength);
            
            model.Property(x => x.UserId).HasUniqueIndex("IX_UserLogins_UserId_LoginProvider", order: 0);
            model.Property(x => x.LoginProvider).HasUniqueIndex("IX_UserLogins_UserId_LoginProvider", order: 1);
        }
    }
}