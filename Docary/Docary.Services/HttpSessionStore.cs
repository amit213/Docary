using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Docary.Models;

namespace Docary.Services
{
    public class HttpSessionStore : ISessionStore
    {
        public void Save(string key, object o)
        {
            HttpContext.Current.Session[key] = o;
        }

        public object Get(string key)
        {
            return HttpContext.Current.Session[key];
        }

        public void SaveUserSession(UserSession userSession)
        {
            Save("UserSession", userSession);            
        }

        public UserSession GetUserSession()
        {
            var userSession = Get("UserSession") as UserSession;

            if (userSession == null)
                return new UserSession();

            return userSession;
        }
    }
}
