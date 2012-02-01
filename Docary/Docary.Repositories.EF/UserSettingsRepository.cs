using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;
using Docary.Repositories;

namespace Docary.Repositories.EF
{
    public class UserSettingsRepository : RepositoryBase<UserSettings>, IUserSettingsRepository
    {
        public UserSettingsRepository(DocaryContext context)
            : base(context) { }

        public UserSettings Get(string userId)
        {
            return Get().Where(u => u.UserId == userId).FirstOrDefault();
        }
    }
}
