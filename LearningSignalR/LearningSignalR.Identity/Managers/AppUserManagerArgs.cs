using System;

namespace LearningSignalR.Identity.Managers
{
    public class AppUserManagerArgs : IDisposable
    {
        public bool UserAllowOnlyAlphanumericUserNames { get; set; }

        public bool UserRequireUniqueEmail { get; set; }

        public bool UserLockoutEnabledByDefault { get; set; }

        public int UserAccountLockoutMinutes { get; set; }

        public int UserMaxFailedAccessAttemptsBeforeLockout { get; set; }

        public int PasswordRequiredLength { get; set; }

        public bool PasswordRequireNonLetterOrDigit { get; set; }

        public bool PasswordRequireDigit { get; set; }

        public bool PasswordRequireLowercase { get; set; }

        public bool PasswordRequireUppercase { get; set; }

        public void Dispose()
        {
            // do nothing
        }
    }
}