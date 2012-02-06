using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Models;

namespace Docary.Services
{
    public interface ISessionStore
    {
        void Save(string key, object o);

        object Get(string key);

        void SaveUserSession(UserSession userSession);

        UserSession GetUserSession();
    }
}
