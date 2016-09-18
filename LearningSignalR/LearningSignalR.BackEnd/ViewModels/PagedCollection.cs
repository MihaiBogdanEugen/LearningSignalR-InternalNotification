using System;
using System.Collections.Generic;

namespace LearningSignalR.BackEnd.ViewModels
{
    public class PagedCollection<T>
    {
        public PagedCollection()
        {
            this.Records = new List<T>();
            this.Header = default(T);
        }

        public PagedCollection(IEnumerable<T> records, int totalNoOfRecords, int? pageNo, int pageSize = Constants.DefaultPageSize) : this()
        {
            this.Records = records;
            this.TotalNoOfRecords = totalNoOfRecords;
            this.TotalNoOfPages = (int)Math.Ceiling((double)totalNoOfRecords / Constants.DefaultPageSize);
            this.CurrentPageNo = pageNo ?? 1;
            this.NoOfRecordsPerPage = pageSize;
        }

        public T Header { get; set; }
        public IEnumerable<T> Records { get; set; }
        public int CurrentPageNo { get; set; }
        public int TotalNoOfRecords { get; set; }
        public int TotalNoOfPages { get; set; }
        public int NoOfRecordsPerPage { get; set; }
    }
}