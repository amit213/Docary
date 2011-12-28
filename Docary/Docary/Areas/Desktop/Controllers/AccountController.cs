using Docary.Services;

namespace Docary.Areas.Desktop.Controllers
{
    public class AccountController : Docary.Areas.Shared.Controllers.AccountController
    {
        public AccountController(IUserSettingsService userSettingsService) : base(userSettingsService) { }
    }
}
