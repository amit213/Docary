using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Docary.Services;
using Docary.ViewModels;
using Docary.ViewModelAssemblers;
using Docary.MvcExtensions;

using Ninject;

namespace Docary.Controllers
{
    public class HomeController : DocaryController
    {
        private IEntryService _entryService;
        private IHomeAssembler _homeAssembler;

        public HomeController(IHomeAssembler homeAssembler, IEntryService entryService) 
        {
            _entryService = entryService;
            _homeAssembler = homeAssembler;
        }

        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {              
                ViewData.Model = _homeAssembler.AssembleHomeIndexViewModel(UserId);

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
