using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LearningSignalR.BackEnd.ViewModels.GetParams
{
    public abstract class BaseGetParams<TSource>
    {
        protected BaseGetParams(int? pageNo, string sortOrder = Constants.DefaultSortOrder)
        {
            this.SortOrder = sortOrder ?? Constants.DefaultSortOrder;
            this.PageNo = pageNo.HasValue ? (pageNo.Value < 1 ? 1 : pageNo.Value) : 1;
        }

        public string SortOrder { get; set; }
        public int? PageNo { get; set; }

        public abstract IEnumerable<Expression<Func<TSource, bool>>> GetFilterPredicates();
    }
}