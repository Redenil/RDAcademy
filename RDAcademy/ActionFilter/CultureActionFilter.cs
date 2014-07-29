

namespace RDAcademy.ActionFilter
{
    using System.Diagnostics;
    using System.Globalization;
    using System.Threading;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    /// <summary>
    /// 
    /// </summary>
    public class CultureActionFilter : ActionFilterAttribute
    {
        private readonly string culture;

        private Stopwatch stopwatch = new Stopwatch();

        public CultureActionFilter(string culture)
        {
            this.culture = culture;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            stopwatch.Stop();

            Debug.WriteLine("After Action / Elasped Time " +stopwatch.ElapsedMilliseconds);
        }

        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            stopwatch.Start();
            Debug.WriteLine("BeforeAction");

            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }
    }
}
