using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

using Docary.Services;

namespace Docary.Areas.Mobile.Controllers
{
    public class AccountController : Docary.Areas.Shared.Controllers.AccountController
    {
        public AccountController(IUserSettingsService userSettingsService) : base(userSettingsService) { }
    }
}
