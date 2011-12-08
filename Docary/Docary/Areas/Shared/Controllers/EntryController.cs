using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Docary.MvcExtensions;
using Docary.Services;
using Docary.ViewModels;
using Docary.ViewModelExtractors;

namespace Docary.Areas.Shared.Controllers
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
