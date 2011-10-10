using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Docary.Models;
using Docary.ViewModels;
using Docary.Services;

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
        public ActionResult Add(AddEntryViewModel addEntryViewModel)
        {
            if (addEntryViewModel.Entry == null)
            {
                return View(new AddEntryViewModel());
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _entryService.AddEntry(addEntryViewModel.Entry);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(addEntryViewModel);
                }
            }
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
