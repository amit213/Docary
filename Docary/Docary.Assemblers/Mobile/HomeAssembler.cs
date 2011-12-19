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

        public HomeAssembler(
            IEntryService entryService,
            IUserSettingsService userSettingservice)
        {
            _entryService = entryService;
            _userSettingService = userSettingservice;
        }

        public HomeIndexViewModel AssembleHomeIndexViewModel(DateTime createdOnMin, DateTime createdOnMax, string userId)
        {
            var indexViewModel = new HomeIndexViewModel();

            var userTimeZone = _userSettingService.Get(userId).TimeZone;
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
