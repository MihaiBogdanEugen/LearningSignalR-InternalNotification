using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Design.PluralizationServices;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Globalization;
using System.Linq.Expressions;

namespace LearningSignalR.Db
{
    public static class ModelConfigurationExtensions
    {
        internal static string Pluralize(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var service = PluralizationService.CreateService(CultureInfo.InvariantCulture);
            return service.IsPlural(text) 
                ? text 
                : service.Pluralize(text);
        }

        internal static PrimitivePropertyConfiguration HasUniqueIndex(this PrimitivePropertyConfiguration propertyConfiguration, string name, int order = 0, bool isClustered = false)
        {
            return propertyConfiguration.HasIndex(name, order, isUnique: true, isClustered: isClustered);
        }

        internal static PrimitivePropertyConfiguration HasIndex(this PrimitivePropertyConfiguration propertyConfiguration, string name, int order, bool isUnique = false, bool isClustered = false)
        {
            var indexAnnotation = new IndexAnnotation(new IndexAttribute(name)
            {
                Order = order,
                IsUnique = isUnique,
                IsClustered = isClustered,
            });

            return propertyConfiguration.HasColumnAnnotation("Index", indexAnnotation);
        }

        internal static string CapitalFirstLetter(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (text.Length == 1)
                return text.ToUpperInvariant();

            return text.Substring(0, 1).ToUpperInvariant() + text.Substring(1).ToLowerInvariant();
        }

        public static PrimitivePropertyConfiguration IdentityColumn<TStructuralType, T>(
            this EntityTypeConfiguration<TStructuralType> model,
            Expression<Func<TStructuralType, T>> propertyExpression,
            string columnName,
            int? columnOrder = null)
            where T : struct
            where TStructuralType : class
        {
            var result = model.Property(propertyExpression)
                .HasColumnName(columnName)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            if (columnOrder.HasValue)
                result = result.HasColumnOrder(columnOrder.Value);

            model.HasKey(propertyExpression);
            
            return result;
        }

        public static PrimitivePropertyConfiguration Column<TStructuralType, T>(this StructuralTypeConfiguration<TStructuralType> model, Expression<Func<TStructuralType, T>> propertyExpression, string columnName, int? columnOrder = null, bool isRequired = true)
            where T : struct
            where TStructuralType : class
        {
            var result = isRequired
                ? model.Property(propertyExpression).HasColumnName(columnName).IsRequired()
                : model.Property(propertyExpression).HasColumnName(columnName).IsOptional();

            if (columnOrder.HasValue)
                result = result.HasColumnOrder(columnOrder.Value);

            return result;
        }

        public static PrimitivePropertyConfiguration Column<TStructuralType, T>(this StructuralTypeConfiguration<TStructuralType> model, Expression<Func<TStructuralType, T?>> propertyExpression, string columnName, int? columnOrder = null)
            where T : struct
            where TStructuralType : class
        {
            var result = model.Property(propertyExpression).HasColumnName(columnName).IsOptional();

            if (columnOrder.HasValue)
                result = result.HasColumnOrder(columnOrder.Value);

            return result;
        }

        public static PrimitivePropertyConfiguration ImageColumn<TStructuralType>(this StructuralTypeConfiguration<TStructuralType> model,
            Expression<Func<TStructuralType, byte[]>> propertyExpression, string columnName, int? columnOrder = null, bool isRequired = true)
            where TStructuralType : class
        {
            var result = isRequired
                ? model.Property(propertyExpression).HasColumnName(columnName).HasColumnType("bytea").IsRequired()
                : model.Property(propertyExpression).HasColumnName(columnName).HasColumnType("bytea").IsOptional();

            if (columnOrder.HasValue)
                result = result.HasColumnOrder(columnOrder.Value);

            return result;
        }

        public static PrimitivePropertyConfiguration DateTimeColumn<TStructuralType>(this StructuralTypeConfiguration<TStructuralType> model, Expression<Func<TStructuralType, DateTime?>> propertyExpression, string columnName, int? columnOrder = null)
            where TStructuralType : class
        {
            var result = model.Property(propertyExpression).HasColumnName(columnName).IsOptional();

            if (columnOrder.HasValue)
                result = result.HasColumnOrder(columnOrder.Value);

            return result;
        }

        public static PrimitivePropertyConfiguration DateTimeColumn<TStructuralType>(this StructuralTypeConfiguration<TStructuralType> model,
            Expression<Func<TStructuralType, DateTime>> propertyExpression, string columnName, int? columnOrder = null, bool isRequired = true)
            where TStructuralType : class
        {
            var result = isRequired
                ? model.Property(propertyExpression).HasColumnName(columnName).IsRequired()
                : model.Property(propertyExpression).HasColumnName(columnName).IsOptional();

            if (columnOrder.HasValue)
                result = result.HasColumnOrder(columnOrder.Value);

            return result;
        }

        public static StringPropertyConfiguration StringColumn<TStructuralType>(this StructuralTypeConfiguration<TStructuralType> model,
            Expression<Func<TStructuralType, string>> propertyExpression, string columnName, int? columnOrder = null, bool isRequired = true, int stringMaxLength = DbConstants.StringLength)
            where TStructuralType : class
        {
            var result = isRequired
                ? model.Property(propertyExpression).HasColumnName(columnName).IsRequired().HasMaxLength(stringMaxLength)
                : model.Property(propertyExpression).HasColumnName(columnName).IsOptional().HasMaxLength(stringMaxLength);

            if (columnOrder.HasValue)
                result = result.HasColumnOrder(columnOrder.Value);

            return result;
        }

        public static StringPropertyConfiguration MaxStringColumn<TStructuralType>(this StructuralTypeConfiguration<TStructuralType> model,
            Expression<Func<TStructuralType, string>> propertyExpression, string columnName, int? columnOrder = null, bool isRequired = true)
            where TStructuralType : class
        {
            var result = isRequired
                ? model.Property(propertyExpression).HasColumnName(columnName).IsRequired().HasColumnType("text")
                : model.Property(propertyExpression).HasColumnName(columnName).IsOptional().HasColumnType("text");

            if (columnOrder.HasValue)
                result = result.HasColumnOrder(columnOrder.Value);

            return result;
        }

        public static DecimalPropertyConfiguration DecimalColumn<TStructuralType>(this StructuralTypeConfiguration<TStructuralType> model,
            Expression<Func<TStructuralType, decimal>> propertyExpression, string columnName, int? columnOrder = null, bool isRequired = true, byte precision = DbConstants.DecimalPrecision, byte scale = DbConstants.DecimalScale)
            where TStructuralType : class
        {
            var result = isRequired
                ? model.Property(propertyExpression).HasColumnName(columnName).IsRequired().HasPrecision(precision, scale)
                : model.Property(propertyExpression).HasColumnName(columnName).IsOptional().HasPrecision(precision, scale);

            if (columnOrder.HasValue)
                result = result.HasColumnOrder(columnOrder.Value);

            return result;
        }

        public static DecimalPropertyConfiguration DecimalColumn<TStructuralType>(this StructuralTypeConfiguration<TStructuralType> model,
            Expression<Func<TStructuralType, decimal?>> propertyExpression, string columnName, int? columnOrder = null, byte precision = DbConstants.DecimalPrecision, byte scale = DbConstants.DecimalScale)
            where TStructuralType : class
        {
            var result = model.Property(propertyExpression).HasColumnName(columnName).IsOptional().HasPrecision(precision, scale);

            if (columnOrder.HasValue)
                result = result.HasColumnOrder(columnOrder.Value);

            return result;
        }

        public static DecimalPropertyConfiguration GeographyDecimalColumn<TStructuralType>(this StructuralTypeConfiguration<TStructuralType> model,
            Expression<Func<TStructuralType, decimal>> propertyExpression, string columnName, int? columnOrder = null, bool isRequired = true, byte precision = DbConstants.GeographyDecimalPrecision, byte scale = DbConstants.GeographyDecimalScale)
            where TStructuralType : class
        {
            return model.DecimalColumn(propertyExpression, columnName, columnOrder, isRequired, precision, scale);
        }

        public static DecimalPropertyConfiguration GeographyDecimalColumn<TStructuralType>(this StructuralTypeConfiguration<TStructuralType> model,
            Expression<Func<TStructuralType, decimal?>> propertyExpression, string columnName, int? columnOrder = null, byte precision = DbConstants.GeographyDecimalPrecision, byte scale = DbConstants.GeographyDecimalScale)
            where TStructuralType : class
        {
            return model.DecimalColumn(propertyExpression, columnName, columnOrder, precision, scale);
        }

        private static string Truncate(this string source, int length)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            if (source.Length > length)
                source = source.Substring(0, length);

            return source;
        }
    }
}