using System;
using System.Security.Claims;
using System.Threading.Tasks;
using LearningSignalR.Db;
using LearningSignalR.Db.Models.Identity;
using LearningSignalR.Identity.Cryptography;
using LearningSignalR.Identity.Stores;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace LearningSignalR.Identity.Managers
{
    public sealed class AppUserManager : UserManager<User, long>
    {
        public AppUserManager(IUserStore<User, long> store, AppUserManagerArgs args) : base(store)
        {
            #region User Configuration

            this.UserValidator = new UserValidator<User, long>(this)
            {
                AllowOnlyAlphanumericUserNames = args.UserAllowOnlyAlphanumericUserNames,
                RequireUniqueEmail = args.UserRequireUniqueEmail
            };

            this.UserLockoutEnabledByDefault = args.UserLockoutEnabledByDefault;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(args.UserAccountLockoutMinutes);
            this.MaxFailedAccessAttemptsBeforeLockout = args.UserMaxFailedAccessAttemptsBeforeLockout;

            #endregion User Configuration

            #region Password Configuration

            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = args.PasswordRequiredLength,
                RequireNonLetterOrDigit = args.PasswordRequireNonLetterOrDigit,
                RequireDigit = args.PasswordRequireDigit,
                RequireLowercase = args.PasswordRequireLowercase,
                RequireUppercase = args.PasswordRequireUppercase
            };

            this.PasswordHasher = new AdaptivePasswordHasher();

            #endregion Password Configuration
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var manager = new AppUserManager(new AppUserStore(context.Get<AppDbContext>()), context.Get<AppUserManagerArgs>());

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<User, long>(dataProtectionProvider.Create("Sell2MeIdentity"));
            }
            return manager;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(User user)
        {
            var identity = await this.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddUserClaims(user);
            return identity;
        }
    }
}