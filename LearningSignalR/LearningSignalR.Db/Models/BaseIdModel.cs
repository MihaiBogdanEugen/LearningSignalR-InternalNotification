using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace LearningSignalR.Db.Models
{
    public class BaseIdModel<T> where T : struct
    {
        public T Id { get; set; }

        internal static EntityTypeConfiguration<TEntity> GetModelDefinition<TEntity>(DbModelBuilder modelBuilder, string tableName, string schemaName = "dbo")
            where TEntity : BaseIdModel<T>
        {
            var model = modelBuilder.Entity<TEntity>();
            model.ToTable(tableName, schemaName);

            model.IdentityColumn(x => x.Id, "Id", 0);

            return model;
        }
    }
}
