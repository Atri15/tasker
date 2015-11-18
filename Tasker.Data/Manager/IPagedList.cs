using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Services.Intefaces
{
    public interface IPagedList
    {
            int TotalItemCount { get; }

            int PageNumber { get; }

            int PageSize { get; }

            bool HasPreviousPage { get; }

            bool HasNextPage { get; }

            bool IsFirstPage { get; }

            bool IsLastPage { get; }
    }
}
