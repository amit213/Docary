using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Docary.MvcExtensions;

namespace Docary.Controllers
{
    public class AccountController : Controller
    {       
        public ActionResult LogOn()
        {
            return RedirectToAction("LogOn", "Account", new { Area = Request.ResolveArea() });      
        }
    }
}
