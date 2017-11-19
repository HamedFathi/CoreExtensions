using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreExtensions
{
    public static class QueryableExtensions
    {
        public static bool Exists<T>(this IQueryable<T> source)
        {
            // ReSharper disable once UseMethodAny.0
            return source.Count() > 0;
        }

        public static bool Exists<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate)
        {
            return source.Count(predicate) > 0;
        }

        public static IQueryable<T> PagedList<T, TResult>(this IQueryable<T> source,
                    int pageIndex, int pageSize, Expression<Func<T, TResult>> orderByProperty
                    , bool isAscendingOrder, out int rowsCount)
        {
            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException("pageIndex must be greater than zero");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException("pageSize must be greater than zero");
            }

            var src = source;

            rowsCount = source.Count();

            src = isAscendingOrder ? src.OrderBy(orderByProperty) : src.OrderByDescending(orderByProperty);

            var result = src.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return result;
        }

        public static Task<List<T>> ToListAsync<T>(this IQueryable<T> list)
        {
            return Task.Run(() => list.ToList());
        }

        public static string ToSqlQuery<T>(this IQueryable<T> source)
        {
            var query = string.IsNullOrEmpty(Convert.ToString(source)) ? "" : source.ToString().Replace("[Extent", "[D");
            return query;
        }
    }
}
