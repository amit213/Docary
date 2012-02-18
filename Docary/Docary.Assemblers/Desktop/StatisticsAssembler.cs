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

        private UserSettings _userSettings;

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
            var defaultStatisticsModel = new HomeStatisticsViewModel();            

            var userSettings = GetUserSettings(userId);

            SetDefaultDatesWhenEmpty(defaultStatisticsModel, userSettings.TimeZone);                                          

            return AssembleHomeStatisticsViewModel(defaultStatisticsModel, userId);
        }

        public HomeStatisticsViewModel AssembleHomeStatisticsViewModel(string userId, DateTime from, DateTime to)
        {
            var statisticsModel = new HomeStatisticsViewModel();

            statisticsModel.From = from;
            statisticsModel.To = to;

            return AssembleHomeStatisticsViewModel(statisticsModel, userId);
        }

        public HomeStatisticsViewModel AssembleHomeStatisticsViewModel(HomeStatisticsViewModel statisticsViewmodel, string userId)
        {
            var userSettings = GetUserSettings(userId);

            var latestEntry = _entryService.GetLatestEntry(userId);
            var firstEntry = _entryService.GetFirstRealEntry(userId);

            if (latestEntry != null)
            {
                var createdOn = new DateTime(latestEntry.CreatedOn.Ticks, DateTimeKind.Utc);
                statisticsViewmodel.LatestEntry = TimeZoneInfo.ConvertTimeFromUtc(createdOn, userSettings.TimeZone);
            }
            if (firstEntry != null)
            {
                var createdOn = new DateTime(firstEntry.CreatedOn.Ticks, DateTimeKind.Utc);
                statisticsViewmodel.FirstEntry = TimeZoneInfo.ConvertTimeFromUtc(createdOn, userSettings.TimeZone);
            }
            statisticsViewmodel.NumberOfEntries = _entryService.GetNumberOfEntries(userId);
            statisticsViewmodel.PerTag = AssembleHomeStatisticsPerTagViewModel(statisticsViewmodel.From.Value, statisticsViewmodel.To.Value, userId);

            return statisticsViewmodel;
        }

        private HomeStatisticsPerTagViewModel AssembleHomeStatisticsPerTagViewModel(DateTime from, DateTime to, string userId)
        {           
            var homeStatisticsPerTagViewModel = new HomeStatisticsPerTagViewModel();
            var items = new List<HomeStatisticsPerTagItem>();

            var entries = _entryService.GetEntries(from, to, userId);

            if (!entries.Any())
                return homeStatisticsPerTagViewModel;

            var userSettings = GetUserSettings(userId);

            var sanitizedEntries = new List<Entry>();
            foreach (var entry in entries)
            {
                if (!entry.StoppedOn.HasValue)
                    entry.StoppedOn = _timeService.GetNow();
                sanitizedEntries.Add(entry);
            }

            var orderedEntries = sanitizedEntries.OrderBy(e => e.CreatedOn);
            var entryGroups = orderedEntries.GroupBy(e => e.TagId);

            var secondsTotal = orderedEntries.Last().StoppedOn.Value.Subtract(orderedEntries.First().CreatedOn).TotalSeconds;

            foreach (var entryGroup in entryGroups)
            {
                var item = new HomeStatisticsPerTagItem();

                var firstTag = entryGroup.First().Tag;
                item.TagName = firstTag.TitleCasedName;
                item.TagColor = firstTag.Color;

                TimeSpan timePerGroup = new TimeSpan();
                foreach (var entry in entryGroup)
                    timePerGroup += entry.StoppedOn.Value.Subtract(entry.CreatedOn);

                item.Time = timePerGroup;
                item.Percentage = (timePerGroup.TotalSeconds / secondsTotal) * 100;
                item.PercentageAsString = item.Percentage > 0 ? item.Percentage.ToString("f2") : "0";

                items.Add(item);
            }

            homeStatisticsPerTagViewModel.Items = items.OrderByDescending(i => i.Percentage);

            return homeStatisticsPerTagViewModel;
        }

        private void SetDefaultDatesWhenEmpty(HomeStatisticsViewModel homeStatisticsViewModel, TimeZoneInfo userTimeZone)
        {
            var nowDate = _timeService.GetNow();
            var defaultFromDate = nowDate.AddDays(-14);
            var defaultToDate = nowDate.AddDays(1);

            if (!homeStatisticsViewModel.From.HasValue)
                homeStatisticsViewModel.From = TimeZoneInfo.ConvertTimeFromUtc(defaultFromDate, userTimeZone).Date;
            if (!homeStatisticsViewModel.To.HasValue)
                homeStatisticsViewModel.To = TimeZoneInfo.ConvertTimeFromUtc(defaultToDate, userTimeZone).Date;
        }

        private UserSettings GetUserSettings(string userId)
        {
            if (_userSettings == null)            
                _userSettings = _userSettingsService.Get(userId);             
            return _userSettings;
        }
    }
}
