using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using Docary.Models;
using Docary.ViewModels;
using Docary.Services;
using Docary.ViewModelExtractors;
using Docary.MvcExtensions;

namespace Docary.Controllers
{
    public class EntryController : DocaryController
    {
        private IEntryService _entryService;

        public EntryController(IEntryService entryService)
        {
            _entryService = entryService;
        }

        [Authorize]
        [HttpGet]
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
                var entry = addEntryViewModel.ExtractEntry();
                entry.UserId = UserId;

                _entryService.AddEntry(entry);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(addEntryViewModel);
            }
        }      
    }
}
