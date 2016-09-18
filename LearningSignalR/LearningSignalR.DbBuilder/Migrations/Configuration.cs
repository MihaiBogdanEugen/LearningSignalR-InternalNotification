using System.Data.Entity.Migrations;
using LearningSignalR.Db;

namespace LearningSignalR.DbBuilder.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppDbContext context)
        {
            StaticData.Seed(context);
        }
    }
}
