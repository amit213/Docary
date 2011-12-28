using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;
using Docary.Repositories;

namespace Docary.Services
{
    public class UserSettingsService : IUserSettingsService
    {
        private IUserSettingsRepository _userSettingsRepository;
        private IScope _scope;

        public UserSettingsService(
            IUserSettingsRepository userSettingRepository,
            IScope scope)
        {
            _userSettingsRepository = userSettingRepository;
            _scope = scope;
        }

        public UserSettings Get(string userId)
        {
            return _userSettingsRepository.Get(userId);
        }

        public UserSettings Add(UserSettings usersSetting)
        {
            var userSettingAdded = _userSettingsRepository.Add(usersSetting);

            _scope.Commit();

            return userSettingAdded;
        }
    }
}
