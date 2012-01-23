﻿using System;
using System.Web.Mvc;

using Docary.MvcExtensions;
using Docary.Services;
using Docary.ViewModelAssemblers.Desktop;
using Docary.ViewModels.Desktop;

namespace Docary.Areas.Desktop.Controllers
{
    public class HomeController : DocaryController
    {
        private IHomeAssembler _homeAssembler;
        private IStatisticsAssembler _statisticsAssembler;
     
        public HomeController(
            IHomeAssembler homeAssembler, 
            IStatisticsAssembler statisticsAssembler)            
        {
            _homeAssembler = homeAssembler;
            _statisticsAssembler = statisticsAssembler;            
        }
       
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {         
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

        [HttpPost]
        [Authorize]
        public ActionResult Statistics(HomeStatisticsViewModel statisticsViewModel)
        {
            var model = _statisticsAssembler.AssembleHomeStatisticsViewModel(statisticsViewModel, UserId);

            ViewData.Model = model;

            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }      
    }
}
