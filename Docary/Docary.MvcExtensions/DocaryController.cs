﻿using System;
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

        protected override void HandleUnknownAction(string actionName)
        {
            if (HttpContext.Request.HttpMethod == "GET")
            {
                var allActionNames = GetType()
                                    .GetMethods(BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance)
                                    .Where(m => m.ReturnType == typeof(ActionResult) &&
                                                !m.IsSpecialName &&
                                                !m.GetCustomAttributes(true).Contains(typeof(HttpPostAttribute)))
                                    .Select(m => m.Name)
                                    .Distinct();

                var actionDistanceDic = allActionNames
                    .Select(a => new 
                        { 
                            Action = a.ToLower(), 
                            Distance = Levenshtein.CalculateDistance(a.ToLower(), actionName.ToLower()) 
                        })
                    .Where(v => v.Distance <= 3)
                    .ToDictionary(v => v.Action);

                if (actionDistanceDic.Any())
                {
                    var actionShortestDistance = actionDistanceDic.OrderBy(v => v.Value).First().Key;

                    ControllerContext.RouteData.Values["action"] = actionShortestDistance;

                    new RedirectResult(Url.RouteUrl(RouteData.Values), permanent: true).ExecuteResult(ControllerContext);                    
                }
                else
                {
                    base.HandleUnknownAction(actionName);
                }
            }
            else
            {
                base.HandleUnknownAction(actionName);
            }
        }
    }
}
