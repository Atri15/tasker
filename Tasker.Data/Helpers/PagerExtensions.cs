using System.Collections.Generic;
using Tasker.Data.Managers;

namespace Tasker.Data.Helpers
{
    public static class PagerExtensions
    {
        public static PagedList<T> AsPagedList<T>(this IEnumerable<T> list, int? page = null, int? pageSize = null)
            where T : class
        {
            return new PagedList<T>(list, page, pageSize);
        }
    }
}