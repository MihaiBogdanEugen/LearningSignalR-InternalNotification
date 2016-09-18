using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace LearningSignalR.Db
{
    public abstract class BaseModel
    {
        protected const int IndexOfLastColumn = 5;

        protected BaseModel()
        {

        }

        public long Id { get; set; }

        protected static EntityTypeConfiguration<T> AddModelDefinition<T>(DbModelBuilder modelBuilder, string tableName, string schemaName = "public") where T : BaseModel
        {
            var model = modelBuilder.Entity<T>();

            model.ToTable(tableName, schemaName);
            model.HasKey(x => x.Id);
            model.IdentityColumn(x => x.Id, "id", 0);
            return model;
        }
    }
}