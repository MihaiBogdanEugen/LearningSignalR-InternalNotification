using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace LearningSignalR.BackEnd.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class StrongPasswordAttribute : ValidationAttribute
    {
        public int RequiredLength { get; set; }
        public bool RequireNonLetterOrDigit { get; set; }
        public bool RequireDigit { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }

        public StrongPasswordAttribute(int requiredLength = 8,
            bool requireNonLetterOrDigit = true,
            bool requireDigit = true,
            bool requireLowercase = true,
            bool requireUppercase = true)
        {
            if (requiredLength < 1)
                throw new ArgumentNullException(nameof(requiredLength));

            this.RequiredLength = requiredLength;
            this.RequireNonLetterOrDigit = requireNonLetterOrDigit;
            this.RequireDigit = requireDigit;
            this.RequireLowercase = requireLowercase;
            this.RequireUppercase = requireUppercase;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var password = value.ToString();

            return this.CheckPassword(password);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.InvariantCulture, this.ErrorMessageString, this.RequiredLength);
        }

        private bool CheckPassword(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            if (text.Length < this.RequiredLength)
                return false;

            if (this.RequireDigit && text.Any(char.IsDigit) == false)
                return false;

            if (this.RequireUppercase && text.Any(char.IsUpper) == false)
                return false;

            if (this.RequireLowercase && text.Any(char.IsLower) == false)
                return false;

            if (this.RequireNonLetterOrDigit && text.All(char.IsLetterOrDigit))
                return false;

            return true;
        }
    }
}