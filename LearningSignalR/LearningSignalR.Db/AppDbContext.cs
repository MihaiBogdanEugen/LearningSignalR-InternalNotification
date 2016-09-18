using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using LearningSignalR.Db.Models;
using LearningSignalR.Db.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace LearningSignalR.Db
{
    public class AppDbContext : IdentityDbContext<User, Role, long, UserLogin, UserRole, UserClaim>
    {
        //TODO: use this for running migrations only!
        public AppDbContext() : base("DefaultConnection")
        {
            this.RequireUniqueEmail = true;
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public AppDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            this.RequireUniqueEmail = true;
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Message> Messages { get; set; }

        public static AppDbContext Create(IdentityFactoryOptions<AppDbContext> options, IOwinContext context)
        {
            return new AppDbContext(context.Get<DbRepositoryArgs>().ConnectionNameOrConnectionString);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Identity Models

            User.AddModelDefinition(modelBuilder);
            Role.AddModelDefinition(modelBuilder);
            UserLogin.AddModelDefinition(modelBuilder);
            UserClaim.AddModelDefinition(modelBuilder);
            UserRole.AddModelDefinition(modelBuilder);

            #endregion Identity Models

            Company.AddModelDefinition(modelBuilder);
            Message.AddModelDefinition(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}