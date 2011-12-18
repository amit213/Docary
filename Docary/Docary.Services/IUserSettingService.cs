using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Models;

namespace Docary.Services
{
    public interface IUserSettingService
    {
        UserSetting Get(string userId);
    }
}
