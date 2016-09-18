using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace LearningSignalR.BackEnd.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RequiredIfCheckedAttribute : ValidationAttribute
    {
        public string OtherProperty { get; private set; }

        public string OtherPropertyDisplayName { get; internal set; }

        public override bool RequiresValidationContext => true;

        public RequiredIfCheckedAttribute(string otherProperty)
        {
            if (otherProperty == null)
                throw new ArgumentNullException(nameof(otherProperty));
            this.OtherProperty = otherProperty;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, this.ErrorMessageString,
                name,
                this.OtherPropertyDisplayName ?? this.OtherProperty);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var errorMessage = string.Format(CultureInfo.CurrentCulture, "Unknown property: {0}", this.OtherProperty);

            var property = validationContext.ObjectType.GetProperty(this.OtherProperty);
            if (property == null)
                return new ValidationResult(errorMessage);

            var isCheckedAsObject = property.GetValue(validationContext.ObjectInstance, null);
            if (isCheckedAsObject == null)
                return new ValidationResult(errorMessage);

            var isCheckedAsString = isCheckedAsObject.ToString();
            var isChecked = false;
            if (bool.TryParse(isCheckedAsString, out isChecked))
            {
                if (isChecked)
                    return value == null
                        ? new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName))
                        : null;
            }
            else
                return new ValidationResult(errorMessage);

            if (this.OtherPropertyDisplayName == null)
                this.OtherPropertyDisplayName = RequiredIfCheckedAttribute.GetDisplayNameForProperty(validationContext.ObjectType, this.OtherProperty);

            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }

        private static string GetDisplayNameForProperty(Type containerType, string propertyName)
        {
            var propertyDescriptor = RequiredIfCheckedAttribute.GetTypeDescriptor(containerType).GetProperties().Find(propertyName, true);
            if (propertyDescriptor == null)
            {
                var errorMessage = string.Format(CultureInfo.CurrentCulture, "Property {1} not found in {0}",
                    containerType.FullName,
                    propertyName);
                throw new ArgumentException(errorMessage);
            }

            var enumerable = propertyDescriptor.Attributes.Cast<Attribute>().ToList();

            var displayAttribute = enumerable.OfType<DisplayAttribute>().FirstOrDefault();
            if (displayAttribute != null)
                return displayAttribute.GetName();

            var displayNameAttribute = enumerable.OfType<DisplayNameAttribute>().FirstOrDefault();
            return displayNameAttribute != null ? displayNameAttribute.DisplayName : propertyName;
        }

        private static ICustomTypeDescriptor GetTypeDescriptor(Type type)
        {
            return new AssociatedMetadataTypeTypeDescriptionProvider(type).GetTypeDescriptor(type);
        }
    }
}