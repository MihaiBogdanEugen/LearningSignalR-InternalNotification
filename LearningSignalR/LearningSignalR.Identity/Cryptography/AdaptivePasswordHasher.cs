using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.AspNet.Identity;

namespace LearningSignalR.Identity.Cryptography
{
    /// <summary>
    /// https://github.com/brockallen/BrockAllen.IdentityReboot/blob/master/source/BrockAllen.IdentityReboot/PasswordHasher/AdaptivePasswordHasher.cs
    /// </summary>
    public class AdaptivePasswordHasher : IPasswordHasher
    {
        static AdaptivePasswordHasher()
        {
            AdaptivePasswordHasher.Default = new AdaptivePasswordHasher(DateTime.Now.Year);
        }

        public static AdaptivePasswordHasher Default { get; private set; }

        private const char PasswordHashingIterationCountSeparator = '.';

        private int IterationCount { get; }

        public AdaptivePasswordHasher()
        {
        }

        public AdaptivePasswordHasher(int iterations)
        {
            if (iterations <= 0)
                throw new ArgumentException("Invalid iterations");

            this.IterationCount = iterations;
        }

        private static string HashPasswordInternal(string password, int count)
        {
            var result = Crypto.HashPassword(password, count);
            return result;
        }

        private static bool VerifyHashedPasswordInternal(string hashedPassword, string providedPassword, int count)
        {
            var result = Crypto.VerifyHashedPassword(hashedPassword, providedPassword, count);
            return result;
        }

        private int GetIterationCount()
        {
            var count = this.IterationCount;
            if (count <= 0)
            {
                count = AdaptivePasswordHasher.GetIterationsFromYear(this.GetCurrentYear());
            }
            return count;
        }

        public string HashPassword(string password)
        {
            var count = this.GetIterationCount();
            var result = HashPasswordInternal(password, count);
            return EncodeIterations(count) +
                   PasswordHashingIterationCountSeparator + result;
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword))
                return PasswordVerificationResult.Failed;

            if (hashedPassword.Contains(PasswordHashingIterationCountSeparator))
            {
                var parts = hashedPassword.Split(PasswordHashingIterationCountSeparator);
                if (parts.Length != 2)
                    return PasswordVerificationResult.Failed;

                var count = DecodeIterations(parts[0]);
                if (count <= 0)
                    return PasswordVerificationResult.Failed;

                hashedPassword = parts[1];

                if (VerifyHashedPasswordInternal(hashedPassword, providedPassword, count))
                    return this.GetIterationCount() != count
                        ? PasswordVerificationResult.SuccessRehashNeeded
                        : PasswordVerificationResult.Success;
            }
            else if (Crypto.VerifyHashedPassword(hashedPassword, providedPassword))
            {
                return PasswordVerificationResult.SuccessRehashNeeded;
            }

            return PasswordVerificationResult.Failed;
        }

        private static string EncodeIterations(int count)
        {
            return count.ToString("X");
        }

        private static int DecodeIterations(string prefix)
        {
            int val;
            if (int.TryParse(prefix, System.Globalization.NumberStyles.HexNumber, null, out val))
                return val;

            return -1;
        }

        // from OWASP : https://www.owasp.org/index.php/Password_Storage_Cheat_Sheet
        private const int StartYear = 2000;
        private const int StartCount = 1000;

        private static int GetIterationsFromYear(int year)
        {
            if (year <= StartYear)
                return StartCount;

            var diff = (year - StartYear)/2;
            var mul = (int) Math.Pow(2, diff);
            var count = StartCount*mul;

            // if we go negative, then we wrapped (expected in year ~2044). 
            // Int32.Max is best we can do at this point
            if (count < 0)
                count = int.MaxValue;
            return count;
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        internal static bool SlowEqualsInternal(string a, string b)
        {
            if (object.ReferenceEquals(a, b))
                return true;

            if (a == null || b == null || a.Length != b.Length)
                return false;

            var same = true;
            for (var i = 0; i < a.Length; i++)
                same &= (a[i] == b[i]);

            return same;
        }

        protected virtual int GetCurrentYear()
        {
            return DateTime.Now.Year;
        }
    }
}