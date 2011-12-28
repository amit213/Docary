using Docary.Services;

namespace Docary.Areas.Mobile.Controllers
{
    public class AccountController : Docary.Areas.Shared.Controllers.AccountController
    {
        public AccountController(IUserSettingsService userSettingsService) : base(userSettingsService) { }
    }
}
