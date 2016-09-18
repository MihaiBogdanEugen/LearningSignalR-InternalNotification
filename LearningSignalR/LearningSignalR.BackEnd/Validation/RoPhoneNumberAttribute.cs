using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace LearningSignalR.BackEnd.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RoPhoneNumberAttribute : ValidationAttribute
    {
        public RoPhoneNumberAttribute()
        {
            this.AllowEmpty = false;
        }

        public bool AllowEmpty { get; set; }

        public override bool IsValid(object value)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
                return this.AllowEmpty;

            if (value.GetType() != typeof(string))
                throw new InvalidOperationException("RoPhoneNumberAttribute can only be used on string properties.");

            var text = (string)value;
            return text.IsValidRoPhoneNumber(this.AllowEmpty);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, this.ErrorMessageString, name);
        }
    }
}