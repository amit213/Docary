using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Docary.Services;
using Docary.ViewModels;

using Ninject;

namespace Docary.Controllers
{
    public class HomeController : Controller
    {
        private IEntryService _entryService;

        public HomeController(IEntryService entryService)
        {
            _entryService = entryService;
        }

        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var indexViewModel = new HomeIndexViewModel()
                {
                    Entries = _entryService.GetEntries()
                };

                ViewData.Model = indexViewModel;

                return View();
            }
            else
            {
                return RedirectToAction("NewUser");
            }
        }

        public ActionResult NewUser()
        {
            return View();
        }
    }
}
