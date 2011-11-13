﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Docary.Services;
using Docary.ViewModels;
using Docary.ViewModelAssemblers;
using Docary.MvcExtensions;
using Docary.ViewModelAssemblers.Mobile;

using Ninject;

namespace Docary.Areas.Mobile.Controllers
{
    public class HomeController : DocaryController
    {
        private IEntryService _entryService;
        private IHomeAssembler _homeAssembler;

        // TODO: Move to Settings
        private const int DAYS_PER_LIST_PAGE = 7;

        public HomeController(IHomeAssembler homeAssembler, IEntryService entryService) 
        {
            _entryService = entryService;
            _homeAssembler = homeAssembler;
        }

        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
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