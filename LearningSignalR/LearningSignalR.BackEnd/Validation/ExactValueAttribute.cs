using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace LearningSignalR.BackEnd.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ExactValueAttribute : ValidationAttribute
    {
        public ExactValueAttribute()
        {
            this.AllowEmpty = false;
            this.ExpectedValue = null;
            this.Type = typeof(string);
        }

        public bool AllowEmpty { get; set; }
        public object ExpectedValue { get; set; }
        public Type Type { get; set; }

        public override bool IsValid(object value)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
                return this.AllowEmpty;

            try
            {
                var expectedValue = Convert.ChangeType(this.ExpectedValue, this.Type);
                var actualValue = Convert.ChangeType(value, this.Type);
                return expectedValue.Equals(actualValue);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, this.ErrorMessageString, name);
        }
    }
}