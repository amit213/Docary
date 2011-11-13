using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docary.Areas.Desktop.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Welcome");
            }
        }

        public ActionResult Welcome()
        {
            return View();
        }
    }
}
