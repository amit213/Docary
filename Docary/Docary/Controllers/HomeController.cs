using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Docary.Services;
using Docary.ViewModels;
using Docary.ViewModelAssemblers;

using Ninject;

namespace Docary.Controllers
{
    public class HomeController : Controller
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
                ViewData.Model = _homeAssembler.AssembleHomeIndexViewModel();

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
