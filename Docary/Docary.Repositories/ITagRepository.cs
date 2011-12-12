using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.Repositories
{
    public interface ITagRepository : IBaseRepository<EntryTag>
    {
        EntryTag Find(string name, string userId);
    }
}
