using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Docary.Areas.Shared.Controllers;
using Docary.MvcExtensions;
using LowercaseRoutesMVC;
using MvcMiniProfiler;
using System;

namespace Docary
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {        
        public static void RegisterRoutes(RouteCollection routes)
        {            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRouteLowercase(
                "Default", // Route name
                "{controller}/{action}", // URL with parameters
                new { controller = "Home", action = "Index" }, // Parameter defaults
                new[] { "Docary.Controllers" }
            );
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {            
            filters.Add(new MiniProfileActionAttribute());
        }

        public static void RegisterProfiler()
        {
            MiniProfilerEF.Initialize();
        }

        protected void Application_BeginRequest()
        {       
            MiniProfiler.Start();         
        }

        protected void Application_AuthorizeRequest(object sender, EventArgs e)
        {
            if (User.Identity.Name != "Nemmie")
                MvcMiniProfiler.MiniProfiler.Stop(true);
        }

        protected void Application_EndRequest()
        {         
            MiniProfiler.Stop();         
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
                if (Response.StatusCode == 404)
                    routeData.Values["action"] = "NotFound";
            }

            var errorsController = (IController)new ErrorsController();
            var requestContext = new RequestContext(new HttpContextWrapper(Context), routeData);
            errorsController.Execute(requestContext);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterProfiler();            
            RegisterRoutes(RouteTable.Routes);
            RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}