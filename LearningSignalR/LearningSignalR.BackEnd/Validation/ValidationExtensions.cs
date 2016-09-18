using System;
using System.Text.RegularExpressions;

namespace LearningSignalR.BackEnd.Validation
{
    public static class ValidationExtensions
    {
        private static readonly Regex EmailRegex = new Regex("^((([a-z]|\\d|[!#\\$%&'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])+(\\.([a-z]|\\d|[!#\\$%&'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])+)*)|((\\x22)((((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(([\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x7f]|\\x21|[\\x23-\\x5b]|[\\x5d-\\x7e]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(\\\\([\\x01-\\x09\\x0b\\x0c\\x0d-\\x7f]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF]))))*(((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(\\x22)))@((([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.)+(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.?$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        private static readonly Regex RoPhoneNumberRegex = new Regex(@"^0\d{9}$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public static bool IsValid(this string text, int maxLength, bool allowEmpty = false)
        {
            if (maxLength < 0)
                throw new ArgumentNullException(nameof(maxLength));

            if (string.IsNullOrEmpty(text))
                return allowEmpty;

            return text.Length <= maxLength;
        }

        public static bool IsValidEmail(this string text, int maxLength, bool allowEmpty = false)
        {
            if (maxLength < 0)
                throw new ArgumentNullException(nameof(maxLength));

            if (string.IsNullOrEmpty(text))
                return allowEmpty;

            if (text.Length > maxLength)
                return false;

            return ValidationExtensions.EmailRegex.IsMatch(text);
        }

        public static bool IsValidRoPhoneNumber(this string text, bool allowEmpty = false)
        {
            if (string.IsNullOrEmpty(text))
                return allowEmpty;

            return ValidationExtensions.RoPhoneNumberRegex.IsMatch(text);
        }

    }
}