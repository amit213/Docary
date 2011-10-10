using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.Services
{
    public interface IEntryService
    {
        IEnumerable<Entry> GetEntries();
        IEnumerable<Entry> GetEntries(string user);
        void AddEntry(Entry entry);
    }
}
