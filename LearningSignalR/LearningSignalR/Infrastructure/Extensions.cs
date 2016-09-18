using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Mvc;
using LearningSignalR.BackEnd;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LearningSignalR.Infrastructure
{
    public static class Extensions
    {
        public static string AsJson(this object value)
        {
            return JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public static IHtmlString AsHtmlJson(this object value)
        {
            using (var stringWriter = new StringWriter())
            using (var jsonWriter = new JsonTextWriter(stringWriter))
            {
                var serializer = new JsonSerializer
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                jsonWriter.QuoteName = false;
                serializer.Serialize(jsonWriter, value);

                return new HtmlString(stringWriter.ToString());
            }
        }

        private static string GetSortingClass(this string sortOrder, string prefix)
        {
            var sortField = sortOrder.Substring(0, sortOrder.IndexOf("_", StringComparison.Ordinal));

            return "sorting" + (sortField.Equals(prefix, StringComparison.InvariantCultureIgnoreCase)
                       ? sortOrder.Substring(sortOrder.IndexOf("_", StringComparison.Ordinal))
                       : string.Empty);
        }

        private static string GetSortingParam(this string sortOrder, string prefix)
        {
            sortOrder = sortOrder.ToLowerInvariant();
            prefix = prefix.ToLowerInvariant();

            var ascOrder = prefix + "_asc";
            var descOrder = prefix + "_desc";

            return string.CompareOrdinal(sortOrder, ascOrder) == 0 ? descOrder : ascOrder;
        }

        public static List<SelectListItem> GetBoolSelectListItems(int? selectedValue = null)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Selected = selectedValue.HasValue && selectedValue.Value == Constants.Yes.Id,
                    Value = Constants.Yes.IdAsString,
                    Text = Constants.Yes.Name
                },
                new SelectListItem
                {
                    Selected = selectedValue.HasValue && selectedValue.Value == Constants.No.Id,
                    Value = Constants.No.IdAsString,
                    Text = Constants.No.Name
                }
            };

            return list;
        }

        public static void AddSortInfo(this ViewDataDictionary viewData, string sortOrder, string prefix)
        {
            if (viewData == null)
                throw new ArgumentNullException(nameof(viewData));

            if (string.IsNullOrEmpty(prefix))
                throw new ArgumentNullException(nameof(prefix));

            if (string.IsNullOrEmpty(sortOrder))
                throw new ArgumentNullException(nameof(sortOrder));

            var prefiLowerCaps = prefix.ToLowerInvariant();
            viewData[prefix + "SortClass"] = sortOrder.GetSortingClass(prefiLowerCaps);
            viewData[prefix + "SortParam"] = sortOrder.GetSortingParam(prefiLowerCaps);
        }

        public static string AsText(this DateTime dateTime, string format = Constants.DateTimeFormat)
        {
            return dateTime.ToString(format, CultureInfo.InvariantCulture);
        }

        public static string AsText(this DateTime? dateTime, string format = Constants.DateTimeFormat)
        {
            return dateTime?.ToString(format, CultureInfo.InvariantCulture) ?? string.Empty;
        }

        public static string AsRoTimeZoneText(this DateTime dateTime, string format = Constants.DateTimeFormat)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, Constants.RoTimeZoneInfo).AsText(format);
        }

        public static string AsRoTimeZoneText(this DateTime? dateTime, string format = Constants.DateTimeFormat)
        {
            return dateTime.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(dateTime.Value, Constants.RoTimeZoneInfo).AsText(format) : string.Empty;
        }

        public static DateTime? AsRoTimeZoneDateTime(this string dateTimeAsText, string format = Constants.DateTimeFormat)
        {
            if (string.IsNullOrEmpty(dateTimeAsText))
                return null;

            DateTime temp;
            if (DateTime.TryParseExact(dateTimeAsText, format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out temp))
                return temp;

            return null;
        }

        public static DateTime? AsDate(this string dateTimeAsText, string format = Constants.DateFormat)
        {
            var temp = dateTimeAsText.AsRoTimeZoneDateTime(format);

            if (temp.HasValue)
                return TimeZoneInfo.ConvertTimeToUtc(temp.Value, Constants.RoTimeZoneInfo);

            return null;
        }

        public static DateTime? AsDateTime(this string dateTimeAsText, string format = Constants.DateTimeFormat)
        {
            var temp = dateTimeAsText.AsRoTimeZoneDateTime(format);

            if (temp.HasValue)
                return TimeZoneInfo.ConvertTimeToUtc(temp.Value, Constants.RoTimeZoneInfo);

            return null;
        }

        public static string AsRoPriceText(this decimal value)
        {
            return value.ToString("F") + " RON";
        }

        public static string AsKmText(this decimal value)
        {
            return value.ToString("F") + " km";
        }
    }
}