using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docary.Controllers
{
    public class EntryController : Controller
    {
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            return View();
        }
    }
}
