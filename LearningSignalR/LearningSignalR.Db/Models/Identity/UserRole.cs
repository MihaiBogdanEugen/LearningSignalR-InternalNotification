using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LearningSignalR.Db.Models.Identity
{
    public class UserRole : IdentityUserRole<long>
    {
        internal static void AddModelDefinition(DbModelBuilder modelBuilder, string tableName = "UserRoles", string schemaName = "dbo")
        {
            var model = modelBuilder.Entity<UserRole>();
            model.ToTable(tableName, schemaName);

            model.HasKey(x => new { x.UserId, x.RoleId });
            model.Column(x => x.UserId, "UserId", 0);
            model.Column(x => x.RoleId, "RoleId", 1);
        }
    }
}