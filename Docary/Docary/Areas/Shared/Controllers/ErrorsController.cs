using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docary.Areas.Shared.Controllers
{
    public class ErrorsController : Controller
    {       
        public ActionResult General()
        {
            return View();
        }        

        public ActionResult NotFound()
        {
            return View();
        }
    }
}
