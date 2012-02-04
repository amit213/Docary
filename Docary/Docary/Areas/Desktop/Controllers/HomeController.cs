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
            HomeIndexViewModel model = null;            

            if (UserSession.HomeIndexFrom.HasValue && UserSession.HomeIndexTo.HasValue)
            {
                model = _homeAssembler.AssembleHomeIndexViewModel(
                    UserId, UserSession.HomeIndexFrom.Value, UserSession.HomeIndexTo.Value);
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

                var userSession = UserSession;
                userSession.HomeIndexFrom = indexViewModel.From;
                userSession.HomeIndexTo = indexViewModel.To;
                UserSession = userSession;
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

            if (UserSession.HomeStatisticsFrom.HasValue && UserSession.HomeStatisticsTo.HasValue)
            {
                model = _statisticsAssembler.AssembleHomeStatisticsViewModel(
                    UserId, UserSession.HomeIndexFrom.Value, UserSession.HomeIndexTo.Value);
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

                var userSession = UserSession;
                userSession.HomeStatisticsFrom = statisticsViewModel.From;
                userSession.HomeStatisticsTo = statisticsViewModel.To;
                UserSession = userSession;
            }
            else
            {
                ViewData.Model = statisticsViewModel;
            }

            return View();
        }

        public ActionResult Welcome()
        {
            throw new Exception("Exception for ELMAH blog post");

            return View();
        }      
    }
}
