using System;
using LearningSignalR.BackEnd.ViewModels;

namespace LearningSignalR.BackEnd
{
    public static class Constants
    {
        public const string DateFormat = "dd/MM/yyyy";
        public const string DateTimeFormat = "dd/MM/yyyy HH:mm";
        public static readonly TimeZoneInfo RoTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("E. Europe Standard Time");
        public const int DefaultPageSize = 10;
        public const string DefaultSortOrder = "addedat_desc";

        public static IdName Yes => new IdName { Id = 1, IdAsString = "1", Name = "Yes" };
        public static IdName No => new IdName { Id = 0, IdAsString = "0", Name = "No" };
    }
}