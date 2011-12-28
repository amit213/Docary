using System.Web;
using System.Web.Mvc;
using Docary.MvcExtensions;

using Docary.Services;
using Docary.ViewModelAssemblers.Mobile;

namespace Docary.Areas.Mobile.Controllers
{
    public class HomeController : DocaryController
    {        
        private IHomeAssembler _homeAssembler;        

        public HomeController(IHomeAssembler homeAssembler)             
        {     
            _homeAssembler = homeAssembler;            
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
