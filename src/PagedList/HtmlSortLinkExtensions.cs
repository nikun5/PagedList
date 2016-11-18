using System.Linq.Expressions;
using System.Web.Routing;
using SortedPagedList;

namespace System.Web.Mvc.Html
{
    public static class HtmlSortLinkExtensions
    {
        /// <summary>
        /// Returns glyph iconed anchor element for the specified property
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString SortLinkFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            return SortLinkFor(htmlHelper, expression, null, null, null);
        }

        /// <summary>
        /// Returns glyph iconed anchor element for the specified property and route values
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="routeValues"></param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString SortLinkFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues)
        {
            return SortLinkFor(htmlHelper, expression, null, null, routeValues);
        }

        /// <summary>
        /// Returns glyph iconed anchor element for the specified property and action
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="action"></param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString SortLinkFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string action)
        {
            return SortLinkFor(htmlHelper, expression, action, null, null);
        }

        /// <summary>
        /// Returns glyph iconed anchor element for the specified property, action and route values
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="action"></param>
        /// <param name="routeValues"></param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString SortLinkFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string action, RouteValueDictionary routeValues)
        {
            return SortLinkFor(htmlHelper, expression, action, null, routeValues);
        }

        /// <summary>
        /// Returns glyph iconed anchor element for the specified property, action and controller
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString SortLinkFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string action, string controller)
        {
            return SortLinkFor(htmlHelper, expression, action, controller, null);
        }

        /// <summary>
        /// Returns glyph iconed anchor element for the specified property, action, controller and route values
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="routeValues"></param>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString SortLinkFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string action, string controller, RouteValueDictionary routeValues)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string expressionField = ExpressionHelper.GetExpressionText(expression);

            action = action ?? htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            controller = controller ?? htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            routeValues = routeValues ?? new RouteValueDictionary();

            var anchor = Helper.CreateLinkElement(metadata, htmlHelper.RouteCollection, htmlHelper.ViewContext.RequestContext,
                expressionField, action, controller, routeValues);

            return MvcHtmlString.Create(anchor.ToString(TagRenderMode.Normal));
        }
    }
}