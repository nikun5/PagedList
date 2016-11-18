using System.Linq;
using System.Web.Helpers;

namespace SortedPagedList
{
    public class SortedPagedList<T> : PagedList.PagedList<T>, ISortedPagedList<T>
    {
        /// <summary>
        /// 	Sort field name from T.
        /// </summary>
        /// <value>
        /// 	Sort field name from T.
        /// </value>
        public string SortField { get; set; }

        /// <summary>
        /// 	Sort order as asc or desc.
        /// </summary>
        /// <value>
        /// 	Sort order as asc or desc.
        /// </value>
        public SortDirection SortDirection { get; set; }

        public SortedPagedList(IQueryable<T> superset, int pageNumber, int pageSize, string sortField, SortDirection sortDirection)
            : base(superset, pageNumber, pageSize)
        {
            SortField = sortField;
            SortDirection = sortDirection;
        }
    }
}