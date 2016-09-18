using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LearningSignalR.Db.Models.Identity
{
    public class UserClaim : IdentityUserClaim<long>
    {
        public new long Id { get; set; }

        internal static void AddModelDefinition(DbModelBuilder modelBuilder, string tableName = "UserClaims", string schemaName = "dbo")
        {
            var model = modelBuilder.Entity<UserClaim>();
            model.ToTable(tableName, schemaName);

            model.IdentityColumn(x => x.Id, "Id", 0);
            model.Column(x => x.UserId, "UserId", 1);
            model.StringColumn(x => x.ClaimType, "ClaimType", 2);
            model.StringColumn(x => x.ClaimValue, "ClaimValue", 3, true, DbConstants.BigStringLength);

            model.Property(x => x.UserId).HasUniqueIndex("IX_UserClaims_UserId_ClaimType", order: 0);
            model.Property(x => x.ClaimType).HasUniqueIndex("IX_UserClaims_UserId_ClaimType", order: 1);
        }
    }
}