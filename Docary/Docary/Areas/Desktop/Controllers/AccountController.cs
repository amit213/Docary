using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Docary.Services;

namespace Docary.Areas.Desktop.Controllers
{
    public class AccountController : Docary.Areas.Shared.Controllers.AccountController
    {
        public AccountController(IUserSettingsService userSettingsService) : base(userSettingsService) { }
    }
}
