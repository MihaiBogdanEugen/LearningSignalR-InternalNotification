using System.Collections.Generic;
using System.Data.Entity;
using LearningSignalR.Db.Models.Identity;

namespace LearningSignalR.Db.Models
{
    public class Company : BaseIdNameModel<long>
    {
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
        public virtual ICollection<Message> Messages { get; set; } = new HashSet<Message>();

        internal static void AddModelDefinition(DbModelBuilder modelBuilder, string tableName = "Companies", string schemaName = "dbo")
        {
            var model = BaseIdNameModel<long>.GetModelDefinition<Company>(modelBuilder, tableName, schemaName);

            model.HasMany(x => x.Users).WithRequired(x => x.Company);
            model.HasMany(x => x.Messages).WithRequired(x => x.Company);
        }
    }
}
