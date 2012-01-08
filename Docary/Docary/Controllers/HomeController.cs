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
            return RedirectToAction("Index", "Home", new { Area = Request.ResolveArea() });            
        }
    }
}
