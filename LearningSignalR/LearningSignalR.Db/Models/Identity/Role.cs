using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LearningSignalR.Db.Models.Identity
{
    public class Role : IdentityRole<long, UserRole>
    {
        internal static void AddModelDefinition(DbModelBuilder modelBuilder, string tableName = "Roles", string schemaName = "dbo")
        {
            var model = modelBuilder.Entity<Role>();
            model.ToTable(tableName, schemaName);

            model.IdentityColumn(x => x.Id, "Id", 0);
            model.StringColumn(x => x.Name, "Name", 1);

            model.Property(x => x.Name).HasUniqueIndex("IX_Roles_Name");
        }
    }
}