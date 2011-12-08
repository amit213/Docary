using Docary.Services;

namespace Docary.Areas.Mobile.Controllers
{
    public class EntryController : Docary.Areas.Shared.Controllers.EntryController
    {
        public EntryController(IEntryService entryService) : base(entryService) { }
    }
}
