﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Docary.Models;
using Docary.ViewModels;
using Docary.Services;
using Docary.ViewModelExtractors;

namespace Docary.Controllers
{
    public class EntryController : Controller
    {
        private IEntryService _entryService;

        public EntryController(IEntryService entryService)
        {
            _entryService = entryService;
        }

        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(AddEntryViewModel addEntryViewModel)
        {
            if (ModelState.IsValid)
            {
                _entryService.AddEntry(addEntryViewModel.ExtractEntry());

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(addEntryViewModel);
            }
        }

        [Authorize]
        public JsonResult Delete(int id)
        {
            _entryService.DeleteEntry(id);

            return Json(new { success = true });
        }

    }
}
