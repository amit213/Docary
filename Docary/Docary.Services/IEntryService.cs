using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.Services
{
    public interface IEntryService
    {       
        IEnumerable<Entry> GetEntries(string userId);
        void AddEntry(Entry entry);        
    }
}
