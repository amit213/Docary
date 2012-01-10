using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Docary.MvcExtensions;

using LowercaseRoutesMVC;
using Docary.Areas.Shared.Controllers;

namespace Docary
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {        
        public static void RegisterRoutes(RouteCollection routes)
        {            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRouteLowercase(
                "Default", // Route name
                "{controller}/{action}", // URL with parameters
                new { controller = "Home", action = "Index" }, // Parameter defaults
                new[] { "Docary.Controllers" }
            );
        }

        protected void Application_Error()
        {
            Response.TrySkipIisCustomErrors = true;

            var exception = Server.GetLastError();
            var httpException = exception as HttpException;

            Response.Clear();
            Server.ClearError();           
            
            var routeData = new RouteData();
            routeData.DataTokens["area"] = HttpContext.Current.Request.ResolveDestinationArea();
            routeData.Values["controller"] = "Errors";
            routeData.Values["action"] = "General";
            routeData.Values["exception"] = exception;
            
            Response.StatusCode = 500;
            
            if (httpException != null)
            {
                Response.StatusCode = httpException.GetHttpCode();
                if (Response.StatusCode == 403)
                    routeData.Values["action"] = "Forbidden";
            }

            var errorsController = (IController)new ErrorsController();
            var requestContext = new RequestContext(new HttpContextWrapper(Context), routeData);
            errorsController.Execute(requestContext);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            RegisterRoutes(RouteTable.Routes);     
        }
    }
}