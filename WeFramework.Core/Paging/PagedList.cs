using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WeFramework.Core.Paging
{
    /// <summary>
    /// Represents a subset of a collection of objects that can be individually accessed by index and containing metadata about the superset collection of objects this subset was created from.
    /// </summary>
    public class PagedList<T> : BasePagedList<T>
    {
        /// <summary>
        /// Initializes a new default instance.
        /// </summary>
        protected PagedList()
        {
        }

        /// <summary>
		/// Initializes a new instance of the class that divides the supplied superset into subsets the size of the supplied pageSize. 
        /// The instance then only containes the objects contained in the subset specified by index.
		/// </summary>
		/// <param name="superset">The collection of objects to be divided into subsets. If the collection implements , it will be treated as such.</param>
		/// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
		/// <param name="pageSize">The maximum size of any individual subset.</param>
		public PagedList(IQueryable<T> superset, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber));
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            // set source to blank list if superset is null to prevent exceptions
            TotalItemCount = superset == null ? 0 : superset.Count();
            PageSize = pageSize;
            PageCount = TotalItemCount > 0 ? (int)Math.Ceiling(TotalItemCount / (double)PageSize) : 0;
            PageNumber = (pageNumber == int.MaxValue) ? PageCount : pageNumber;
            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < PageCount;
            IsFirstPage = PageNumber == 1;
            IsLastPage = PageNumber >= PageCount;
            FirstItemOnPage = (PageNumber - 1) * PageSize + 1;
            var numberOfLastItemOnPage = FirstItemOnPage + PageSize - 1;
            LastItemOnPage = numberOfLastItemOnPage > TotalItemCount ? TotalItemCount : numberOfLastItemOnPage;

            // add items to internal list
            if (superset != null && TotalItemCount > 0)
            {
                Subset.AddRange(pageNumber == 1 ? superset.Take(pageSize).ToList() : superset.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
            }
        }

        public PagedList(IQueryable<T> superset, Expression<Func<T>> keySelector, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber));
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            // set source to blank list if superset is null to prevent exceptions
            TotalItemCount = superset == null ? 0 : superset.Count();
            PageSize = pageSize;
            PageNumber = pageNumber;
            PageCount = TotalItemCount > 0 ? (int)Math.Ceiling(TotalItemCount / (double)PageSize) : 0;
            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < PageCount;
            IsFirstPage = PageNumber == 1;
            IsLastPage = PageNumber >= PageCount;
            FirstItemOnPage = (PageNumber - 1) * PageSize + 1;
            var numberOfLastItemOnPage = FirstItemOnPage + PageSize - 1;
            LastItemOnPage = numberOfLastItemOnPage > TotalItemCount ? TotalItemCount : numberOfLastItemOnPage;

            // add items to internal list
            if (superset != null && TotalItemCount > 0)
            {
                Subset.AddRange(pageNumber == 1 ? superset.Take(pageSize).ToList() : superset.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
            }
        }


        /// <summary>
        /// Initializes a new instance of the PagedList class that divides the supplied superset into subsets the size of the supplied pageSize. The instance then only containes the objects contained in the subset specified by index.
        /// </summary>
        /// <param name="superset">The collection of objects to be divided into subsets. If the collection implements , it will be treated as such.</param>
        /// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        public PagedList(IEnumerable<T> superset, int pageNumber, int pageSize) : this(superset.AsQueryable<T>(), pageNumber, pageSize)
        {

        }

        /// <summary>
        /// For Clone PagedList
        /// </summary>
        /// <param name="pagedListMetaData">Source PagedList</param>
        /// <param name="superset">Source collection</param>
        public PagedList(PagedListMetaData pagedListMetaData, IEnumerable<T> superset)
        {
            TotalItemCount = pagedListMetaData.TotalItemCount;
            PageSize = pagedListMetaData.PageSize;
            PageNumber = pagedListMetaData.PageNumber;
            PageCount = pagedListMetaData.PageCount;
            HasPreviousPage = pagedListMetaData.HasPreviousPage;
            HasNextPage = pagedListMetaData.HasNextPage;
            IsFirstPage = pagedListMetaData.IsFirstPage;
            IsLastPage = pagedListMetaData.IsLastPage;
            FirstItemOnPage = pagedListMetaData.FirstItemOnPage;
            LastItemOnPage = pagedListMetaData.LastItemOnPage;

            Subset.AddRange(superset);
        }
    }
}