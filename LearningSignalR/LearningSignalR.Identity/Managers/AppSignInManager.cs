using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using LearningSignalR.Db.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace LearningSignalR.Identity.Managers
{
    public class AppSignInManager : SignInManager<User, long>
    {
        #region Constructors

        public AppSignInManager(AppUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager) { }

        public static AppSignInManager Create(IdentityFactoryOptions<AppSignInManager> options, IOwinContext context)
        {
            return new AppSignInManager(context.GetUserManager<AppUserManager>(), context.Authentication);
        }

        #endregion Constructors

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return ((AppUserManager)this.UserManager).GenerateUserIdentityAsync(user);
        }

        public async Task<AdvancedSignInStatus> AdvancedPasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            if (this.UserManager == null)
                return AdvancedSignInStatus.Failure;

            var user = await this.UserManager.FindByNameAsync(userName);
            if (user == null)
                return AdvancedSignInStatus.Failure;

            var userId = user.Id;

            if (await this.UserManager.IsLockedOutAsync(userId))
                return AdvancedSignInStatus.LockedOut;

            if (user.IsDisabled)
                return AdvancedSignInStatus.Disabled;

            if (await this.UserManager.CheckPasswordAsync(user, password))
            {
                await this.UserManager.ResetAccessFailedCountAsync(userId);
                return await this.AdvancedSignInOrTwoFactor(user, isPersistent);
            }

            if (shouldLockout == false)
                return AdvancedSignInStatus.Failure;

            await this.UserManager.AccessFailedAsync(userId);
            if (await this.UserManager.IsLockedOutAsync(userId))
                return AdvancedSignInStatus.LockedOut;

            return AdvancedSignInStatus.Failure;
        }

        private async Task<AdvancedSignInStatus> AdvancedSignInOrTwoFactor(User user, bool isPersistent)
        {
            var idAsString = user.Id.ToString(NumberFormatInfo.InvariantInfo);
            var isTwoFactorEnabled = await this.UserManager.GetTwoFactorEnabledAsync(user.Id);
            var noOfTwoFactorProviders = (await this.UserManager.GetValidTwoFactorProvidersAsync(user.Id)).Count;
            var isTwoFactorBrowserRemembered = await this.AuthenticationManager.TwoFactorBrowserRememberedAsync(idAsString);

            if (isTwoFactorEnabled && noOfTwoFactorProviders > 0 && !isTwoFactorBrowserRemembered)
            {
                var identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, idAsString));
                identity.AddUserClaims(user);
                this.AuthenticationManager.SignIn(identity);
                return AdvancedSignInStatus.RequiresVerification;
            }

            await this.AdvancedSignInAsync(user, isPersistent, false);
            return AdvancedSignInStatus.Success;
        }

        private async Task AdvancedSignInAsync(User user, bool isPersistent, bool rememberBrowser)
        {
            var userIdentity = await this.CreateUserIdentityAsync(user);
            var idAsString = user.Id.ToString(NumberFormatInfo.InvariantInfo);

            this.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            if (rememberBrowser)
            {
                var rememberBrowserIdentity = this.AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(idAsString);
                this.AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity, rememberBrowserIdentity);
            }
            else
            {
                this.AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity);
            }
        }
    }
}