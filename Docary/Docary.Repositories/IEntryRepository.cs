using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.Repositories
{
    public interface IEntryRepository
    {
        IQueryable<Entry> GetEntries();
        void AddEntry(Entry entry);
    }
}
