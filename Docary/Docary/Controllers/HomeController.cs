using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string area = Request.Browser.IsMobileDevice ? "Mobile" : "Desktop";
                        
            return RedirectToAction("Index", "Home", new { Area = area });            
        }

    }
}
