using System.Linq.Expressions;
using System.Web.Routing;
using SortedPagedList;

namespace System.Web.Mvc.Ajax
{
    public static class AjaxSortLinkExtensions
    {
        /// <summary>
        /// Returns glyph icon anchor element for the specified property
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="ajaxHelper"></param>
        /// <param name="expression"></param>
        /// <param name="ajaxOptions"></param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString SortLinkFor<TModel, TProperty>(this AjaxHelper<TModel> ajaxHelper,
            Expression<Func<TModel, TProperty>> expression, AjaxOptions ajaxOptions)
        {
            return SortLinkFor(ajaxHelper, expression, ajaxOptions, null, null, null);
        }

        /// <summary>
        /// Returns glyph icon anchor element for the specified property and route values
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="ajaxHelper"></param>
        /// <param name="expression"></param>
        /// <param name="ajaxOptions"></param>
        /// <param name="routeValues"></param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString SortLinkFor<TModel, TProperty>(this AjaxHelper<TModel> ajaxHelper,
            Expression<Func<TModel, TProperty>> expression, AjaxOptions ajaxOptions, RouteValueDictionary routeValues)
        {
            return SortLinkFor(ajaxHelper, expression, ajaxOptions, null, null, routeValues);
        }

        /// <summary>
        /// Returns glyph icon anchor element for the specified property and action
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="ajaxHelper"></param>
        /// <param name="expression"></param>
        /// <param name="ajaxOptions"></param>
        /// <param name="action"></param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString SortLinkFor<TModel, TProperty>(this AjaxHelper<TModel> ajaxHelper,
            Expression<Func<TModel, TProperty>> expression, AjaxOptions ajaxOptions, string action)
        {
            return SortLinkFor(ajaxHelper, expression, ajaxOptions, action, null, null);
        }

        /// <summary>
        /// Returns glyph icon anchor element for the specified property, action and route values
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="ajaxHelper"></param>
        /// <param name="expression"></param>
        /// <param name="ajaxOptions"></param>
        /// <param name="action"></param>
        /// <param name="routeValues"></param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString SortLinkFor<TModel, TProperty>(this AjaxHelper<TModel> ajaxHelper,
            Expression<Func<TModel, TProperty>> expression, AjaxOptions ajaxOptions, string action, RouteValueDictionary routeValues)
        {
            return SortLinkFor(ajaxHelper, expression, ajaxOptions, action, null, routeValues);
        }

        /// <summary>
        /// Returns glyph icon anchor element for the specified property, action and controller
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="ajaxHelper"></param>
        /// <param name="expression"></param>
        /// <param name="ajaxOptions"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString SortLinkFor<TModel, TProperty>(this AjaxHelper<TModel> ajaxHelper,
            Expression<Func<TModel, TProperty>> expression, AjaxOptions ajaxOptions, string action, string controller)
        {
            return SortLinkFor(ajaxHelper, expression, ajaxOptions, action, controller, null);
        }

        /// <summary>
        /// Returns glyph icon anchor element for the specified property, action, controller and route values
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="ajaxHelper"></param>
        /// <param name="expression"></param>
        /// <param name="ajaxOptions"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="routeValues"></param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString SortLinkFor<TModel, TProperty>(this AjaxHelper<TModel> ajaxHelper,
            Expression<Func<TModel, TProperty>> expression, AjaxOptions ajaxOptions, string action, string controller, RouteValueDictionary routeValues)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, ajaxHelper.ViewData);
            string expressionField = ExpressionHelper.GetExpressionText(expression);

            action = action ?? ajaxHelper.ViewContext.RouteData.GetRequiredString("action");
            controller = controller ?? ajaxHelper.ViewContext.RouteData.GetRequiredString("controller");
            routeValues = routeValues ?? new RouteValueDictionary();

            var anchor = Helper.CreateLinkElement(metadata, ajaxHelper.RouteCollection, ajaxHelper.ViewContext.RequestContext,
                expressionField, action, controller, routeValues);
            anchor.MergeAttributes((ajaxOptions ?? new AjaxOptions()).ToUnobtrusiveHtmlAttributes());

            return MvcHtmlString.Create(anchor.ToString(TagRenderMode.Normal));
        }
    }
}