using System.Collections.Generic;

namespace WeFramework.Core.Paging
{
    /// <summary>
    /// Represents a subset of a collection of objects that can be individually accessed by index and 
    /// containing metadata about the superset collection of objects this subset was created from.
    /// </summary>
    public interface IPagedList<out T> : IPagedList, IEnumerable<T>
    {
        ///<summary>
        /// Gets the element at the specified index.
        ///</summary>
        T this[int index] { get; }

        ///<summary>
        /// Gets the number of elements contained on this page.
        ///</summary>
        int Count { get; }

        ///<summary>
        /// Gets a non-enumerable copy of this paged list.
        ///</summary>
        IPagedList GetMetaData();
    }

    /// <summary>
    /// Represents a subset of a collection of objects that can be individually accessed by index and 
    /// containing metadata about the superset collection of objects this subset was created from.
    /// </summary>
    public interface IPagedList
    {
        /// <summary>
        /// Total number of subsets within the superset.
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// Total number of objects contained within the superset.
        /// </summary>
        int TotalItemCount { get; }

        /// <summary>
        /// One-based index of this subset within the superset.
        /// </summary>
        int PageNumber { get; }

        /// <summary>
        /// Maximum size any individual subset.
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Returns true if this is NOT the first subset within the superset.
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Returns true if this is NOT the last subset within the superset.
        /// </summary>

        bool HasNextPage { get; }

        /// <summary>
        /// Returns true if this is the first subset within the superset.
        /// </summary>
        bool IsFirstPage { get; }

        /// <summary>
        /// Returns true if this is the last subset within the superset.
        /// </summary>
        bool IsLastPage { get; }

        /// <summary>
        /// One-based index of the first item in the paged subset.
        /// </summary>
        int FirstItemOnPage { get; }

        /// <summary>
        /// One-based index of the last item in the paged subset.
        /// </summary>
        int LastItemOnPage { get; }
    }
}