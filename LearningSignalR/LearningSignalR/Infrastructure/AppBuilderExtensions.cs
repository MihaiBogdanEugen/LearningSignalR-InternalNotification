using System;
using LearningSignalR.BackEnd.DbRepository.Entity;
using LearningSignalR.Db;
using LearningSignalR.Db.Models.Identity;
using LearningSignalR.Identity.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace LearningSignalR.Infrastructure
{
    public static class AppBuilderExtensions
    {
        private const string ConnectionName = "DefaultConnection";

        public static void ConfigureOwinContext(this IAppBuilder app)
        {
            app.CreatePerOwinContext<AppUserManagerArgs>(() => DefaultSell2MeUserManagerArgs);
            app.CreatePerOwinContext<DbRepositoryArgs>(() => DefaultDbRepositoryArgs);
            app.CreatePerOwinContext<AppDbContext>(() => new AppDbContext(ConnectionName));

            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);
            app.CreatePerOwinContext<AppSignInManager>(AppSignInManager.Create);

            app.CreatePerOwinContext<AppEntityDbRepository>(AppEntityDbRepository.Create);
        }

        public static void ConfigureAuth(this IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieName = "LearningSignalR",
                LoginPath = new PathString("/Session/Login"),
                LogoutPath = new PathString("/Session/Logout"),
                Provider = DefaultCookieAuthenticationProvider,
                //CookieSecure = CookieSecureOption.Always,
                //CookieHttpOnly = true,
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            //app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
        }

        private static CookieAuthenticationProvider DefaultCookieAuthenticationProvider
        {
            get
            {
                return new CookieAuthenticationProvider
                {
                    OnValidateIdentity =
                        SecurityStampValidator.OnValidateIdentity<AppUserManager, User, long>(
                            validateInterval: TimeSpan.FromMinutes(30),
                            regenerateIdentityCallback: (manager, user) => manager.GenerateUserIdentityAsync(user),
                            getUserIdCallback: (claim) => long.Parse(claim.GetUserId()))
                };
            }
        }

        private static AppUserManagerArgs DefaultSell2MeUserManagerArgs => new AppUserManagerArgs
        {
            PasswordRequireDigit = ConfigurationService.GetApplicationSetting<bool>("PasswordRequireDigit"),
            PasswordRequireLowercase = ConfigurationService.GetApplicationSetting<bool>("PasswordRequireLowercase"),
            PasswordRequireNonLetterOrDigit = ConfigurationService.GetApplicationSetting<bool>("PasswordRequireNonLetterOrDigit"),
            PasswordRequireUppercase = ConfigurationService.GetApplicationSetting<bool>("PasswordRequireUppercase"),
            PasswordRequiredLength = ConfigurationService.GetApplicationSetting<int>("PasswordRequiredLength"),
            UserAccountLockoutMinutes = ConfigurationService.GetApplicationSetting<int>("UserAccountLockoutMinutes"),
            UserAllowOnlyAlphanumericUserNames = ConfigurationService.GetApplicationSetting<bool>("UserAllowOnlyAlphanumericUserNames"),
            UserLockoutEnabledByDefault = ConfigurationService.GetApplicationSetting<bool>("UserLockoutEnabledByDefault"),
            UserMaxFailedAccessAttemptsBeforeLockout = ConfigurationService.GetApplicationSetting<int>("UserMaxFailedAccessAttemptsBeforeLockout"),
            UserRequireUniqueEmail = ConfigurationService.GetApplicationSetting<bool>("UserRequireUniqueEmail")
        };

        private static DbRepositoryArgs DefaultDbRepositoryArgs => new DbRepositoryArgs
        {
            ConnectionName = ConnectionName,
            ConnectionString = ConfigurationService.GetConnectionString(ConnectionName),
        };
    }
}