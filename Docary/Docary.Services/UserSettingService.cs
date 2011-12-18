using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Models;
using Docary.Repositories;

namespace Docary.Services
{
    public class UserSettingService : IUserSettingService
    {
        private IUserSettingRepository _userSettingsRepository;
       
        public UserSettingService(IUserSettingRepository userSettingRepository)
        {
            _userSettingsRepository = userSettingRepository;
        }

        public UserSetting Get(string userId)
        {
            return _userSettingsRepository.Get(userId);
        }
    }
}
