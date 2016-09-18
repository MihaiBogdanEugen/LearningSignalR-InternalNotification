using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace LearningSignalR.Db.Models
{
    public class BaseIdNameModel<T> : BaseIdModel<T>
        where T : struct 
    {
        public string Name { get; set; }

        internal static EntityTypeConfiguration<TEntity> GetModelDefinition<TEntity>(DbModelBuilder modelBuilder,
            string tableName, string schemaName = "dbo", bool isNameRequired = true, bool isNameUnique = true)
            where TEntity : BaseIdNameModel<T>
        {
            var model = BaseIdModel<T>.GetModelDefinition<TEntity>(modelBuilder, tableName, schemaName);

            model.StringColumn(x => x.Name, "Name", 1, isNameRequired);

            if (isNameUnique)
                model.Property(x => x.Name).HasUniqueIndex($"IX_{tableName}_Name");
            
            return model;
        }
    }
}