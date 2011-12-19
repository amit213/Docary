using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Models;

namespace Docary.Services
{
    public interface IUserSettingsService
    {
        UserSettings Get(string userId);

        UserSettings Add(UserSettings userSettings);
    }
}
