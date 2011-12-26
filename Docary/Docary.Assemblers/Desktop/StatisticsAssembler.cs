using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.ViewModels.Desktop;
using Docary.Services;
using Docary.Models;

namespace Docary.ViewModelAssemblers.Desktop
{
    public class StatisticsAssembler : IStatisticsAssembler
    {
        private IEntryService _entryService;
        private ITimeService _timeService;
        private IUserSettingsService _userSettingsService;

        public StatisticsAssembler(
            IEntryService entryService,
            ITimeService timeService,
            IUserSettingsService usersettingsService)
        {
            _entryService = entryService;
            _timeService = timeService;
            _userSettingsService = usersettingsService;
        }

        public HomeStatisticsViewModel AssembleHomeStatisticsViewModel(string userId)
        {
            var homeStatisticsViewModel = new HomeStatisticsViewModel();

            var latestEntry = _entryService.GetLatestEntry(userId);
            var firstEntry = _entryService.GetFirstRealEntry(userId);

            homeStatisticsViewModel.LatestEntry = latestEntry == null ? (DateTime?)null : latestEntry.CreatedOn;
            homeStatisticsViewModel.FirstEntry = firstEntry == null ? (DateTime?)null : firstEntry.CreatedOn;
            homeStatisticsViewModel.NumberOfEntries = _entryService.GetNumberOfEntries(userId);

            if (firstEntry != null && latestEntry != null)
                homeStatisticsViewModel.PerTag = AssembleHomeStatisticsPerTagViewModel(firstEntry, latestEntry, userId);                        

            return homeStatisticsViewModel;
        }

        private HomeStatisticsPerTagViewModel AssembleHomeStatisticsPerTagViewModel(Entry firstEntry, Entry latestEntry, string userId)
        {           
            var homeStatisticsPerTagViewModel = new HomeStatisticsPerTagViewModel();
            var items = new List<HomeStatisticsPerTagItem>();

            var userSettings = _userSettingsService.Get(userId);
            var entries = _entryService.GetEntries(firstEntry.CreatedOn, latestEntry.CreatedOn, userId);
            var sanitizedEntries = new List<Entry>();

            foreach (var entry in entries)
            {
                if (!entry.StoppedOn.HasValue)
                    entry.StoppedOn = TimeZoneInfo.ConvertTimeFromUtc(_timeService.GetNow(), userSettings.TimeZone);
                sanitizedEntries.Add(entry);
            }

            var orderedEntries = sanitizedEntries.OrderBy(e => e.CreatedOn);
            var entryGroups = orderedEntries.GroupBy(e => e.TagId);

            var secondsTotal = orderedEntries.Last().StoppedOn.Value.Subtract(orderedEntries.First().CreatedOn).TotalSeconds;

            foreach (var entryGroup in entryGroups)
            {
                var item = new HomeStatisticsPerTagItem();

                item.Tag = entryGroup.First().Tag;

                TimeSpan timePerGroup = new TimeSpan();
                foreach (var entry in entryGroup)
                    timePerGroup += entry.StoppedOn.Value.Subtract(entry.CreatedOn);

                item.Time = timePerGroup;
                item.Percentage = (timePerGroup.TotalSeconds / secondsTotal) * 100;

                items.Add(item);
            }

            homeStatisticsPerTagViewModel.Items = items.OrderByDescending(i => i.Percentage);

            return homeStatisticsPerTagViewModel;
        }
    }
}
