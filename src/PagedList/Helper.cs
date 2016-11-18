using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace SortedPagedList
{
    internal static class Helper
    {
        public static IEnumerable<T> Sort<T>(IQueryable<T> superset, string sortField, SortDirection sortDirection)
        {
            return !string.IsNullOrWhiteSpace(sortField) && Enum.IsDefined(typeof (SortDirection), sortDirection)
                ? superset.OrderBy(sortField + " " + sortDirection)
                : superset;
        }

        public static int HandlePageSwitch<T>(List<T> superset, int pageNumber, int pageSize, object newId,
            string newIdName)
        {
            var total = superset.Count;

            if (total <= (pageNumber - 1)*pageSize)
            {
                var firstPage = Convert.ToInt32(total/pageSize);
                return firstPage <= 0 ? 1 : firstPage;
            }

            var index = superset.FindIndex(s =>
                s.GetType().GetProperty(newIdName).GetValue(s).ToString().Equals(newId.ToString()));

            return index != -1
                ? index/pageSize + 1
                : pageNumber;
        }

        public static TagBuilder CreateLinkElement(
            ModelMetadata metadata, RouteCollection routeCollection, RequestContext requestContext,
            string expressionField, string action, string controller, RouteValueDictionary routeValues)
        {
            var container = GetContainer(metadata);

            int pageNumber = container.PageNumber;
            int pageSize = container.PageSize;
            string sortField = container.SortField;
            SortDirection sortDirection = container.SortDirection;

            string displayName = metadata.DisplayName ?? expressionField;

            routeValues = routeValues != null
                ? new RouteValueDictionary(routeValues)
                : new RouteValueDictionary();

            var anchor = GetAnchor(expressionField, displayName, ref sortField, ref sortDirection);

            routeValues[nameof(sortField)] = sortField;
            routeValues[nameof(sortDirection)] = sortDirection;
            routeValues[nameof(pageNumber)] = pageNumber;
            routeValues[nameof(pageSize)] = pageSize;

            string url = UrlHelper.GenerateUrl("default", action, controller, routeValues,
                routeCollection, requestContext, false);

            anchor.Attributes.Add("href", url);
            anchor.InnerHtml = string.IsNullOrWhiteSpace(anchor.InnerHtml)
                ? displayName
                : anchor.InnerHtml;

            return anchor;
        }

        private static TagBuilder GetAnchor(string expressionField, string displayName, ref string sortField,
            ref SortDirection sortDirection)
        {
            var anchor = new TagBuilder("a");
            anchor.AddCssClass("sortLink");

            if (string.IsNullOrWhiteSpace(sortField))
            {
                sortField = expressionField;
            }
            else if (!sortField.Equals(expressionField))
            {
                sortDirection = SortDirection.Ascending;
                sortField = expressionField;
            }
            else
            {
                var icon = new TagBuilder("i");
                icon.AddCssClass("sortIcon");

                if (sortDirection.Equals(SortDirection.Ascending))
                {
                    sortDirection = SortDirection.Descending;
                    icon.AddCssClass("sortAscending");
                }
                else
                {
                    sortDirection = SortDirection.Ascending;
                    icon.AddCssClass("sortDescending");
                }
                anchor.InnerHtml = $"{icon} {displayName}";
            }
            return anchor;
        }

        private static ISortedPagedList GetContainer(ModelMetadata metadata)
        {
            var container = metadata.Container as ISortedPagedList;

            if (container != null)
                return container;

            var sortedContainer = metadata.Container.GetType().GetProperties()
                .FirstOrDefault(p => p.PropertyType.Name.Contains(nameof(SortedPagedList)));
            if (sortedContainer != null)
                container = sortedContainer.GetValue(metadata.Container) as ISortedPagedList;
            if (container == null)
                throw new ArgumentException($"Argument should be of type {nameof(SortedPagedList)}");

            return container;
        }
    }
}
