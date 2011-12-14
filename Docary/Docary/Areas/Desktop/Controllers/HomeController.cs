using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Docary.ViewModelAssemblers;
using Docary.Services;
using Docary.MvcExtensions;
using Docary.ViewModelAssemblers.Desktop;
using Docary.ViewModels.Desktop;

namespace Docary.Areas.Desktop.Controllers
{
    public class HomeController : DocaryController
    {
        private IHomeAssembler _homeAssembler;
        private IStatisticsAssembler _statisticsAssembler;
        private IEntryService _entryService;
     
        public HomeController(
            IHomeAssembler homeAssembler, 
            IStatisticsAssembler statisticsAssembler,
            IEntryService entryService)
        {
            _homeAssembler = homeAssembler;
            _statisticsAssembler = statisticsAssembler;
            _entryService = entryService;
        }
       
        [HttpGet]
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Welcome");

            var model = _homeAssembler.AssembleHomeIndexViewModel(UserId);

            ViewData.Model = model;
            ViewBag.EntryGroups = model.EntryGroups;

            return View();           
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(HomeIndexViewModel indexViewModel)
        {
            var model = _homeAssembler.AssembleHomeIndexViewModel(indexViewModel, UserId);

            ViewData.Model = model;
            ViewBag.EntryGroups = model.EntryGroups;

            return View();
        }

        [Authorize]
        public ActionResult Statistics()
        {
            var model = _statisticsAssembler.AssembleHomeStatisticsViewModel(UserId);

            ViewData.Model = model;

            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }        
    }
}
