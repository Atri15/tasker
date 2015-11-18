using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tasker.Data.Interfaces;

namespace Tasker.Data.Manager
{
    public class PagedList<T> : IEnumerable<T>, IPagedList
    {
        private readonly IEnumerable<T> _superset;

        public int PageCount { get; private set; }

        public IEnumerable<int> CurrentIndexes
        {
            get
            {
                var indexes = Enumerable.Range(1, PageCount);
                var skip = PageNumber - (int)((double)ActionLinkCount / 2 + 0.5);
                if (skip + ActionLinkCount > PageCount)
                {
                    skip = PageCount - ActionLinkCount;
                }

                return indexes.Skip(skip).Take(ActionLinkCount);
            }
        }

        public PagedList(IEnumerable<T> superset, int? pageNumber = null, int? pageSize = null)
        {
            PageSize = pageSize ?? 3;
            PageNumber = pageNumber ?? 1;

            if (superset == null)
            {
                superset = Enumerable.Empty<T>();
            }

            _superset = superset;

            TotalItemCount = superset.Count();
            PageCount = TotalItemCount > 0
                ? (int)Math.Ceiling(TotalItemCount / (double)PageSize)
                : 0;
            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < PageCount;
            IsFirstPage = PageNumber == 1;
            IsLastPage = PageNumber >= PageCount;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _superset.Skip(PageSize * (PageNumber - 1)).Take(PageSize).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int ActionLinkCount
        {
            get { return 7; }
        }

        public int TotalItemCount { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public bool HasPreviousPage { get; private set; }
        public bool HasNextPage { get; private set; }
        public bool IsFirstPage { get; private set; }
        public bool IsLastPage { get; private set; }
    }
}