using System.Web.Mvc;

using LowercaseRoutesMVC;

namespace Docary.Areas.Desktop
{
    public class DesktopAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Desktop";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRouteLowercase(
                "Desktop_default",
                "Desktop/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
