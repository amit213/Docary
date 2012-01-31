using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Reflection;
using System.Web.Security;

using Docary.MvcExtensions;

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

        public UserSession UserSession
        {
            get
            {
                var userSession = Session["UserSession"] as UserSession;

                if (userSession == null)
                    return new UserSession();

                return userSession;
            }
            set
            {
                Session["UserSession"] = value;
            }
        }        

        protected override void HandleUnknownAction(string actionName)
        {
            if (!HttpContext.Request.HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase))
                Throw404HttpException(actionName);
            
            TryToRedirectToAnActionNearby(actionName);           
        }

        private void TryToRedirectToAnActionNearby(string actionName)
        {
            var httpGetActionNames = GetAllHttpGetActionNames();
            if (!httpGetActionNames.Any())
                Throw404HttpException(actionName);

            var actionDistanceMap = CalculateLevenshteinDistance(httpGetActionNames, actionName)
                                        .Where(i => i.Value <= 3);
            if (!actionDistanceMap.Any())
                Throw404HttpException(actionName);

            var shortestDistance = actionDistanceMap.Select(v => v.Value).Min();
            var nearestAction = actionDistanceMap.Where(i => i.Value == shortestDistance).First().Key;

            ControllerContext.RouteData.Values["action"] = nearestAction;

            new RedirectResult(Url.RouteUrl(RouteData.Values), permanent: true).ExecuteResult(ControllerContext);
        }

        private void Throw404HttpException(string actionName)
        {
            throw new HttpException(404, string.Format("{0}.{1} is an unknown action.", GetType(), actionName)); 
        }

        private Dictionary<string, int> CalculateLevenshteinDistance(IEnumerable<string> actionList, string actionName)
        {
            return actionList
                    .Select(a => new
                    {
                        Action = a.ToLower(),
                        Distance = Levenshtein.CalculateDistance(a.ToLower(), actionName.ToLower())
                    })                    
                    .ToDictionary(k => k.Action, v => v.Distance);
        }

        private IEnumerable<string> GetAllHttpGetActionNames()
        {
            return GetType()
                    .GetMethods(BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => m.ReturnType == typeof(ActionResult) &&
                                !m.IsSpecialName &&
                                !m.GetCustomAttributes(true).Contains(typeof(HttpPostAttribute)))
                    .Select(m => m.Name)
                    .Distinct();
        }
    }
}
