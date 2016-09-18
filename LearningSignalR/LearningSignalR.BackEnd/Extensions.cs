using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using LearningSignalR.BackEnd.ViewModels;
using LearningSignalR.BackEnd.ViewModels.GetParams;

namespace LearningSignalR.BackEnd
{
    public static class Extensions
    {
        public static List<SelectListItem> GetBoolSelectListItems(this int? selectedValue)
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

        public static List<SelectListItem> GetBoolSelectListItems(this int selectedValue)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Selected = selectedValue == Constants.Yes.Id,
                    Value = Constants.Yes.IdAsString,
                    Text = Constants.Yes.Name
                },
                new SelectListItem
                {
                    Selected = selectedValue == Constants.No.Id,
                    Value = Constants.No.IdAsString,
                    Text = Constants.No.Name
                }
            };

            return list;
        }

        public static IEnumerable<int> AsInts(this IEnumerable<SelectListItem> items)
        {
            var temp = 0;
            return (from value in items.Select(x => x.Value)
                where int.TryParse(value, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out temp)
                select temp).ToArray();
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> collection, string sortExpression)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (!collection.Any() || string.IsNullOrEmpty(sortExpression))
                return collection;

            sortExpression = sortExpression.ToLowerInvariant();

            var completeSortExpression = string.Empty;
            if (sortExpression.EndsWith("_asc"))
                completeSortExpression = sortExpression.Substring(0, sortExpression.Length - "_asc".Length) + " ascending";
            else if (sortExpression.EndsWith("_desc"))
                completeSortExpression = sortExpression.Substring(0, sortExpression.Length - "_desc".Length) + " descending";

            if (string.IsNullOrWhiteSpace(completeSortExpression) == false)
                collection = collection.OrderBy(completeSortExpression, StringComparer.InvariantCultureIgnoreCase);

            return collection;
        }

        public static async Task<PagedCollection<TResult>> FilterSortPageAsync<TSource, TResult, TParam>(this IQueryable<TSource> query,
            Expression<Func<TSource, TResult>> selector,
            TParam getParams = null,
            string defaultSortOrder = Constants.DefaultSortOrder,
            int defaultPageSize = Constants.DefaultPageSize)
            where TParam : BaseGetParams<TSource>
        {
            string correctSortOrder;
            int correctPageNo;

            if (getParams == null)
            {
                correctSortOrder = defaultSortOrder;
                correctPageNo = 0;
            }
            else
            {
                correctSortOrder = getParams.SortOrder;
                correctPageNo = getParams.PageNo - 1 ?? 0;

                var predicates = getParams.GetFilterPredicates();
                query = predicates.Aggregate(query, (current, predicate) => current.Where(predicate));
            }

            var totalNoOfRecords = await query.CountAsync();
            if (totalNoOfRecords == 0)
                return new PagedCollection<TResult>(new List<TResult>(), 0, 0);

            var records = await query
                .Select(selector)
                .Sort(correctSortOrder)
                .Skip(correctPageNo * defaultPageSize)
                .Take(defaultPageSize)
                .ToListAsync();

            return new PagedCollection<TResult>(records, totalNoOfRecords, correctPageNo, defaultPageSize);
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (var obj in list)
                action(obj);
        }
    }
}