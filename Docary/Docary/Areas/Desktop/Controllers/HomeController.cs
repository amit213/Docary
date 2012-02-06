using System;
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
        private ISessionStore _sessionStore;
     
        public HomeController(
            IHomeAssembler homeAssembler, 
            IStatisticsAssembler statisticsAssembler,
            ISessionStore sessionStore)            
        {
            _homeAssembler = homeAssembler;
            _statisticsAssembler = statisticsAssembler;
            _sessionStore = sessionStore;
        }
       
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            HomeIndexViewModel model = null;

            var userSession = _sessionStore.GetUserSession();

            if (userSession.HomeIndexFrom.HasValue && userSession.HomeIndexTo.HasValue)
            {
                model = _homeAssembler.AssembleHomeIndexViewModel(
                    UserId, userSession.HomeIndexFrom.Value, userSession.HomeIndexTo.Value);
            }
            else
            {
                model = _homeAssembler.AssembleHomeIndexViewModel(UserId);
            }            

            ViewData.Model = model;
            ViewBag.EntryGroups = model.EntryGroups;

            return View();           
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(HomeIndexViewModel indexViewModel)
        {
            if (ViewData.ModelState.IsValid)
            {
                var model = _homeAssembler.AssembleHomeIndexViewModel(indexViewModel, UserId);

                ViewData.Model = model;
                ViewBag.EntryGroups = model.EntryGroups;

                var userSession = _sessionStore.GetUserSession();
                userSession.HomeIndexFrom = indexViewModel.From;
                userSession.HomeIndexTo = indexViewModel.To;
                _sessionStore.SaveUserSession(userSession);
            }
            else
            {
                ViewData.Model = indexViewModel;
                ViewBag.EntryGroups = indexViewModel.EntryGroups;
            }

            return View();
        }

        [Authorize]
        public ActionResult Statistics()
        {
            HomeStatisticsViewModel model = null;

            var userSession = _sessionStore.GetUserSession();

            if (userSession.HomeStatisticsFrom.HasValue && userSession.HomeStatisticsTo.HasValue)
            {
                model = _statisticsAssembler.AssembleHomeStatisticsViewModel(
                    UserId, userSession.HomeIndexFrom.Value, userSession.HomeIndexTo.Value);
            }
            else
            {
                model = _statisticsAssembler.AssembleHomeStatisticsViewModel(UserId);
            }

            ViewData.Model = model;

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Statistics(HomeStatisticsViewModel statisticsViewModel)
        {
            if (ModelState.IsValid)
            {
                var model = _statisticsAssembler.AssembleHomeStatisticsViewModel(statisticsViewModel, UserId);

                ViewData.Model = model;

                var userSession = _sessionStore.GetUserSession();
                userSession.HomeStatisticsFrom = statisticsViewModel.From;
                userSession.HomeStatisticsTo = statisticsViewModel.To;
                _sessionStore.SaveUserSession(userSession);
            }
            else
            {
                ViewData.Model = statisticsViewModel;
            }

            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }      
    }
}
