using Docary.Services;

namespace Docary.Areas.Desktop.Controllers
{
    public class EntryController : Docary.Areas.Shared.Controllers.EntryController
    {
        public EntryController(IEntryService entryService) : base(entryService) { }
    }
}
