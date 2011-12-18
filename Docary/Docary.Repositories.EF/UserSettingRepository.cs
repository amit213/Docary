using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Models;
using Docary.Repositories;

namespace Docary.Repositories.EF
{
    public class UserSettingRepository : RepositoryBase<UserSetting>, IUserSettingRepository
    {
        public UserSettingRepository(DocaryContext context)
            : base(context)
        {

        }

        public UserSetting Get(string userId)
        {
            return new UserSetting() { UserId = userId, TimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time") };
        }
    }
}
