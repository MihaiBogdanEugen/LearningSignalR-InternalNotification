using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using LearningSignalR.Db;
using LearningSignalR.Db.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace LearningSignalR.Identity
{
    public static class IdentityExtensions
    {
        private const string Anonymous = "Anonymous";

        public static void AddUserClaims(this ClaimsIdentity identity, User user)
        {
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        }

        public static string GetFullName(this IPrincipal principal)
        {
            if (principal?.Identity == null || principal.Identity.IsAuthenticated == false)
                return Anonymous;
        
            var claimsIdentity = principal.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
                return Anonymous;
        
            var lastNameClaim = claimsIdentity.Claims.FirstOrDefault( );
            var lastName = lastNameClaim == null ? string.Empty : lastNameClaim.Value;
        
            var firstNameClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName);
            var firstName = firstNameClaim == null ? string.Empty : firstNameClaim.Value;
        
            var fullName = (firstName + " " + lastName).Trim();
            return string.IsNullOrEmpty(fullName) ? Anonymous : fullName;
        }
        
        public static void AllSignOut(this IAuthenticationManager authenticationManager)
        {
            authenticationManager.SignOut(
                DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.ExternalBearer,
                DefaultAuthenticationTypes.ExternalCookie,
                DefaultAuthenticationTypes.TwoFactorCookie,
                DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
        }
        
        public static bool IsAdministrator(this IPrincipal principal)
        {
            return principal.HasRoleClaim(DbConstants.Roles.Administrator.Name);
        }
        
        public static bool IsPowerUser(this IPrincipal principal)
        {
            return principal.HasRoleClaim(DbConstants.Roles.PowerUser.Name);
        }
        
        public static bool IsUser(this IPrincipal principal)
        {
            return principal.HasRoleClaim(DbConstants.Roles.User.Name);
        }
        
        public static bool IsClient(this IPrincipal principal)
        {
            return principal.HasRoleClaim(DbConstants.Roles.Client.Name);
        }

        private static bool HasRoleClaim(this IPrincipal principal, string roleName)
        {
            if (principal?.Identity == null || principal.Identity.IsAuthenticated == false)
                return false;

            var claimsIdentity = principal.Identity as ClaimsIdentity;
            return claimsIdentity != null && claimsIdentity.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).Contains(roleName);
        }
    }
}