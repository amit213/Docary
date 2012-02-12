using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MvcMiniProfiler;

namespace Docary.MvcExtensions
{
    public class MiniProfileActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var profiler = MiniProfiler.Current;
            
            var step = profiler.Step("Action: " + filterContext.ActionDescriptor.ActionName);
            
            filterContext.HttpContext.Items["step"] = step;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var step = filterContext.HttpContext.Items["step"] as IDisposable;
            if (step != null)            
                step.Dispose();            
        }
    }
}
