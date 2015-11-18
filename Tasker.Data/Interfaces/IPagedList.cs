namespace Tasker.Data.Interfaces
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

        int ActionLinkCount { get; }
    }
}
