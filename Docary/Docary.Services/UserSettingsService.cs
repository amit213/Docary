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
        private IUnitOfWork _unitOfWork;

        public UserSettingsService(
            IUserSettingsRepository userSettingRepository,
            IUnitOfWork unitOfWork)
        {
            _userSettingsRepository = userSettingRepository;
            _unitOfWork = unitOfWork;
        }

        public UserSettings Get(string userId)
        {
            return _userSettingsRepository.Get(userId);
        }

        public UserSettings Add(UserSettings usersSetting)
        {
            var userSettingAdded = _userSettingsRepository.Add(usersSetting);

            _unitOfWork.Commit();

            return userSettingAdded;
        }
    }
}
