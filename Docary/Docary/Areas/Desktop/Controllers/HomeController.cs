using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Docary.ViewModelAssemblers;
using Docary.Services;
using Docary.MvcExtensions;
using Docary.ViewModelAssemblers.Desktop;

namespace Docary.Areas.Desktop.Controllers
{
    public class HomeController : DocaryController
    {
        private IHomeAssembler _homeAssembler;
        private IEntryService _entryService;

        // TODO: Move to Settings
        private const int DAYS_PER_LIST_PAGE =607;

        public HomeController(IHomeAssembler homeAssembler, IEntryService entryService)
        {
            _homeAssembler = homeAssembler;
            _entryService = entryService;
        }

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var model = _homeAssembler.AssembleHomeIndexViewModel(
                   DateTime.Now.Date.AddDays(-DAYS_PER_LIST_PAGE), DateTime.MaxValue, UserId);

                ViewData.Model = model;
                ViewBag.EntryGroups = model.EntryGroups;

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
