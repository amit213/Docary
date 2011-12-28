using System;
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
        private IHomeAssembler _homeAssembler;
        private ITimeService _timeService;              

        public HomeController(
            IHomeAssembler homeAssembler,          
            ITimeService timeService) 
        {     
            _homeAssembler = homeAssembler;
            _timeService = timeService;
        }

        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var model = _homeAssembler.AssembleHomeIndexViewModel(UserId);

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
