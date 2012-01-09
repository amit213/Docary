using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Docary.MvcExtensions;

namespace Docary.Controllers
{
    public class HomeController : DocaryController
    {
        public ActionResult Index()
        {           
            var view = Request.IsAuthenticated ? "Index" : "Welcome";
            var area = Request.ResolveArea();             

            return RedirectToAction(view, "Home", new { Area = area });            
        }
    }
}
