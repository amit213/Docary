using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

using Docary.MvcExtensions;
using System.Web;

namespace Docary.MvcExtensions
{
    public class DocaryController : Controller
    {
        public string UserId
        {
            get
            {
                return Convert.ToString(Membership.GetUser(HttpContext.User.Identity.Name).ProviderUserKey);
            }
        }       
    }
}
