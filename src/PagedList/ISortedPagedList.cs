using System.Web.Helpers;
using PagedList;

namespace SortedPagedList
{
    /// <summary>
    /// Represents a subset of a collection of objects that can be individually accessed by index and containing metadata
    /// and sort information about the superset collection of objects this subset was created from.
    /// </summary>
    /// <remarks>
    /// Represents a subset of a collection of objects that can be individually accessed by index and containing metadata
    /// and sort information about the superset collection of objects this subset was created from.
    /// </remarks>
    /// <typeparam name="T">The type of object the collection should contain.</typeparam>
    /// <seealso cref="IPagedList{T}"/>
    public interface ISortedPagedList<out T> : ISortedPagedList, IPagedList<T>
    {
    }

    /// <summary>
    /// Represents a subset of a collection of objects that can be individually accessed by index and containing metadata
    /// and sort information about the superset collection of objects this subset was created from.
    /// </summary>
    /// <remarks>
    /// Represents a subset of a collection of objects that can be individually accessed by index and containing metadata
    /// and sort information about the superset collection of objects this subset was created from.
    /// </remarks>
    public interface ISortedPagedList : IPagedList
    {
        /// <summary>
        /// Subset sort field
        /// </summary>
        string SortField { get; }

        /// <summary>
        /// Subset sort direction
        /// </summary>
        SortDirection SortDirection { get; }
    }
}