using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.Repositories
{
    public interface IEntryRepository : 
        IBaseRepository<Entry>, ICanUpdate<Entry>
    {
        IEnumerable<Entry> Get(DateTime createdOnMin, DateTime createdOnMax, string userId);

        Entry GetLatestEntry(string userId);

        Entry GetFirstRealEntry(string userId);        

        bool IsEmpty(string userId);
    }
}
