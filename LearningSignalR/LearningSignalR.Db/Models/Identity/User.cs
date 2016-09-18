using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LearningSignalR.Db.Models.Identity
{
    public class User : IdentityUser<long, UserLogin, UserRole, UserClaim>
    {
        public User()
        {
            this.EmailConfirmed = false;
            this.PhoneNumberConfirmed = false;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDisabled { get; set; } = false;
        public long CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Message> SentMessages { get; set; } = new HashSet<Message>();
        public virtual ICollection<Message> ReadMessages { get; set; } = new HashSet<Message>();

        internal static void AddModelDefinition(DbModelBuilder modelBuilder, string tableName = "Users", string schemaName = "dbo")
        {
            var model = modelBuilder.Entity<User>();
            model.ToTable(tableName, schemaName);

            model.IdentityColumn(x => x.Id, "Id", 0);
            model.StringColumn(x => x.FirstName, "FirstName", 1);
            model.StringColumn(x => x.LastName, "LastName", 2);
            model.StringColumn(x => x.UserName, "UserName", 3);
            model.StringColumn(x => x.Email, "Email", 4);
            model.Column(x => x.EmailConfirmed, "IsEmailConfirmed", 5);
            model.StringColumn(x => x.PasswordHash, "PasswordHash", 6);
            model.StringColumn(x => x.SecurityStamp, "SecurityStamp", 7);
            model.StringColumn(x => x.PhoneNumber, "PhoneNumber", 8, false);
            model.Column(x => x.PhoneNumberConfirmed, "IsPhoneNumberConfirmed", 9);
            model.Column(x => x.AccessFailedCount, "NoOfFailedLogins", 10);
            model.Column(x => x.LockoutEnabled, "IsLockoutEnabled", 11);
            model.DateTimeColumn(x => x.LockoutEndDateUtc, "LockoutEndingAt", 12);
            model.Column(x => x.IsDisabled, "IsDisabled", 13);
            model.Column(x => x.TwoFactorEnabled, "IsTwoFactorEnabled", 14);
            model.Column(x => x.CompanyId, "CompanyId", 15);

            model.HasMany(x => x.SentMessages).WithRequired(x => x.SentByUser);
            model.HasMany(x => x.ReadMessages).WithOptional(x => x.ReadByUser);

            model.Property(x => x.Email).HasUniqueIndex("IX_Users_Email");
            model.Property(x => x.UserName).HasUniqueIndex("IX_Users_UserName");
        }
    }
}