using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.ViewModels.Mobile;
using Docary.Services;
using Docary.Models;

namespace Docary.ViewModelAssemblers.Mobile 
{
    public class HomeAssembler : IHomeAssembler
    {
        private IEntryService _entryService;
        private IUserSettingsService _userSettingService;
        private ITimeService _timeService;

        private const int DAYS_PER_LIST_PAGE = 4;

        public HomeAssembler(
            IEntryService entryService,
            IUserSettingsService userSettingservice,
            ITimeService timeService)
        {
            _entryService = entryService;
            _userSettingService = userSettingservice;
            _timeService = timeService;
        }

        public HomeIndexViewModel AssembleHomeIndexViewModel(string userId)
        {
            var indexViewModel = new HomeIndexViewModel();

            var userTimeZone = _userSettingService.Get(userId).TimeZone;

            var createdOnMin = _timeService.GetNow().Date.AddDays(-DAYS_PER_LIST_PAGE);
            var createdOnMax = DateTime.MaxValue;

            var universalEntries = _entryService.GetEntries(createdOnMin, createdOnMax, userId);
            
            var localizedEntries = new List<Entry>();

            foreach(var entry in universalEntries) 
            {
                entry.CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(entry.CreatedOn, userTimeZone);
                if (entry.StoppedOn.HasValue)
                    entry.StoppedOn = TimeZoneInfo.ConvertTimeFromUtc(entry.StoppedOn.Value, userTimeZone);
                localizedEntries.Add(entry);                
            }
                                        
            var entries = localizedEntries.OrderByDescending(e => e.CreatedOn);
            var groups = entries.GroupBy(e => e.CreatedOn.Date);

            indexViewModel.EntryGroups = new List<HomeIndexViewModelEntryGroup>();            

            foreach (var group in groups)
            {
                var entryGroup = new HomeIndexViewModelEntryGroup();
                entryGroup.Date = group.First().CreatedOn.Date;
                entryGroup.Entries = group.ToList();

                indexViewModel.EntryGroups.Add(entryGroup);
            }           

            return indexViewModel;
        }
    }
}
