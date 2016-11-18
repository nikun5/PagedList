using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;

namespace SortedPagedList
{
    public static class SortedPagedListExtensions
    {
        /// <summary>
        /// Creates a subset of this collection of objects that can be individually accessed by index and containing metadata 
        /// and sort information about the collection of objects the subset was created from.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>
        /// <param name="superset">The collection of objects to be divided into subsets. If the collection implements <see cref="IQueryable{T}"/>, it will be treated as such.</param>
        /// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        /// <param name="sortField">The sort field the subset will be sorted by.</param>
        /// <param name="sortDirection">The sort order the subset will be sorted by.</param>
        /// <returns>An ordered subset of this collection of objects that can be individually accessed by index and containing metadata
        /// and sort information about the collection of objects the subset was created from.</returns>
        /// <seealso cref="SortedPagedList{T}"/>
        public static ISortedPagedList<T> ToSortedPagedList<T>(this IQueryable<T> superset, int pageNumber, int pageSize,
            string sortField, SortDirection sortDirection)
        {
            superset = Helper.Sort(superset, sortField, sortDirection).AsQueryable();

            return new SortedPagedList<T>(superset, pageNumber, pageSize, sortField, sortDirection);
        }

        /// <summary>
        /// Creates a subset of this collection of objects that can be individually accessed by index and containing metadata 
        /// and sort information about the collection of objects the subset was created from.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>
        /// <param name="superset">The collection of objects to be divided into subsets. If the collection implements <see cref="IEnumerable{T}"/>, it will be treated as such.</param>
        /// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        /// <param name="sortField">The sort field the subset will be sorted by.</param>
        /// <param name="sortDirection">The sort order the subset will be sorted by.</param>
        /// <returns>An ordered subset of this collection of objects that can be individually accessed by index and containing metadata
        /// and sort information about the collection of objects the subset was created from.</returns>
        /// <seealso cref="SortedPagedList{T}"/>
        public static ISortedPagedList<T> ToSortedPagedList<T>(this IEnumerable<T> superset, int pageNumber, int pageSize,
            string sortField, SortDirection sortDirection)
        {
            return superset.AsQueryable().ToSortedPagedList(pageNumber, pageSize, sortField, sortDirection);
        }

        /// <summary>
        /// Creates a subset of this collection of objects that can be individually accessed by index and containing metadata 
        /// and sort information about the collection of objects the subset was created from.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>
        /// <param name="superset">The collection of objects to be divided into subsets. If the collection implements <see cref="IQueryable{T}"/>, it will be treated as such.</param>
        /// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        /// <param name="sortField">The sort field the subset will be sorted by.</param>
        /// <param name="sortDirection">The sort order the subset will be sorted by.</param>
        /// <param name="newId">The new inserted row id in the superset.</param>
        /// <param name="newIdName">The new inserted row id name in the superset, default is "Id".</param>
        /// <returns>An ordered subset of this collection of objects with the pageNumber the new item is present on
        /// that can be individually accessed by index and containing metadata
        /// and sort information about the collection of objects the subset was created from.</returns>
        /// <seealso cref="SortedPagedList{T}"/>
        public static ISortedPagedList<T> ToSortedPagedList<T>(this IQueryable<T> superset, int pageNumber, int pageSize,
            string sortField, SortDirection sortDirection, object newId, string newIdName = "Id")
        {
            superset = Helper.Sort(superset, sortField, sortDirection).AsQueryable();

            pageNumber = Helper.HandlePageSwitch(superset.ToList(), pageNumber, pageSize, newId, newIdName);

            return new SortedPagedList<T>(superset, pageNumber, pageSize, sortField, sortDirection);
        }

        /// <summary>
        /// Creates a subset of this collection of objects that can be individually accessed by index and containing metadata 
        /// and sort information about the collection of objects the subset was created from.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>
        /// <param name="superset">The collection of objects to be divided into subsets. If the collection implements <see cref="IEnumerable{T}"/>, it will be treated as such.</param>
        /// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        /// <param name="sortField">The sort field the subset will be sorted by.</param>
        /// <param name="sortDirection">The sort order the subset will be sorted by.</param>
        /// <param name="newId">The new inserted row id in the superset.</param>
        /// <param name="newIdName">The new inserted row id name in the superset, default is "Id".</param>
        /// <returns>An ordered subset of this collection of objects with the pageNumber the new item is present on
        /// that can be individually accessed by index and containing metadata
        /// and sort information about the collection of objects the subset was created from.</returns>
        /// <seealso cref="SortedPagedList{T}"/>
        public static ISortedPagedList<T> ToSortedPagedList<T>(this IEnumerable<T> superset, int pageNumber, int pageSize,
            string sortField, SortDirection sortDirection, object newId, string newIdName = "Id")
        {
            return superset.AsQueryable().ToSortedPagedList(pageNumber, pageSize, sortField, sortDirection, newId, newIdName);
        }
    }
}