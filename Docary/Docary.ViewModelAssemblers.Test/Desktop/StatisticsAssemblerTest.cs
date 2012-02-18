using System;
using System.Linq;
using Docary.Models;
using Docary.Services;
using Docary.ViewModelAssemblers.Desktop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Docary.ViewModels.Desktop;

namespace Docary.ViewModelAssemblers.Test.Desktop
{
    [TestClass]
    public class StatisticsAssemblerAssembleHomeStatisticsViewModelTest
    {
        private DateTime _baseDateTime = new DateTime(2011, 11, 8, 0, 0, 0, DateTimeKind.Local);

        [TestMethod]
        public void Test_Fills_In_LatestEntry()
        {
            var entryService = GetEntryService();

            var anyEntry = new Entry() { CreatedOn = new DateTime(2010, 10, 8) };
            
            entryService
                .Setup(e => e.GetLatestEntry(It.IsAny<string>()))
                .Returns(anyEntry);

            var statisticsAssembler = new StatisticsAssembler(
                entryService.Object, GetDefaultTimeService().Object, GetDefaultUserSettingsService().Object);

            var statisticsViewModel = new HomeStatisticsViewModel(_baseDateTime, _baseDateTime.AddDays(3));
            var statisticsViewModelResult = statisticsAssembler.AssembleHomeStatisticsViewModel(statisticsViewModel, It.IsAny<string>());

            Assert.IsTrue(statisticsViewModelResult.HasLatestEntry);
            Assert.IsNotNull(statisticsViewModelResult.LatestEntry);
        }

        [TestMethod]
        public void Test_Fills_In_Null_When_LatestEntry_Is_Null()
        {
            var entryService = GetEntryService();           

            entryService
                .Setup(e => e.GetLatestEntry(It.IsAny<string>()))
                .Returns((Entry)null);

            var statisticsAssembler = new StatisticsAssembler(
                entryService.Object, GetDefaultTimeService().Object, GetDefaultUserSettingsService().Object);

            var statisticsViewModel = new HomeStatisticsViewModel(_baseDateTime, _baseDateTime.AddDays(3));
            var statisticsViewModelResult = statisticsAssembler.AssembleHomeStatisticsViewModel(statisticsViewModel, It.IsAny<string>());

            Assert.IsFalse(statisticsViewModelResult.HasLatestEntry);
            Assert.IsNull(statisticsViewModelResult.LatestEntry);
        }

        [TestMethod]
        public void Test_Fills_In_FirstEntry()
        {
            var entryService = GetEntryService();

            var anyEntry = new Entry() { CreatedOn = new DateTime(2010, 10, 8) };

            entryService
                .Setup(e => e.GetFirstRealEntry(It.IsAny<string>()))
                .Returns(anyEntry);

            var statisticsAssembler = new StatisticsAssembler(
                entryService.Object, GetDefaultTimeService().Object, GetDefaultUserSettingsService().Object);

            var statisticsViewModel = new HomeStatisticsViewModel(_baseDateTime, _baseDateTime.AddDays(3));
            var statisticsViewModelResult = statisticsAssembler.AssembleHomeStatisticsViewModel(statisticsViewModel, It.IsAny<string>());

            Assert.IsTrue(statisticsViewModelResult.HasFirstEntry);
            Assert.IsNotNull(statisticsViewModelResult.FirstEntry);
        }

        [TestMethod]
        public void Test_Fills_In_Null_When_FirstEntry_Is_Null()
        {
            var entryService = GetEntryService();        

            entryService
                .Setup(e => e.GetFirstRealEntry(It.IsAny<string>()))
                .Returns((Entry)null);

            var statisticsAssembler = new StatisticsAssembler(
                entryService.Object, GetDefaultTimeService().Object, GetDefaultUserSettingsService().Object);

            var statisticsViewModel = new HomeStatisticsViewModel(_baseDateTime, _baseDateTime.AddDays(3));
            var statisticsViewModelResult = statisticsAssembler.AssembleHomeStatisticsViewModel(statisticsViewModel, It.IsAny<string>());

            Assert.IsFalse(statisticsViewModelResult.HasFirstEntry);
            Assert.IsNull(statisticsViewModelResult.FirstEntry);
        }

        [TestMethod]
        public void Test_Fills_In_Number_Of_Entries()
        {
            var entryService = GetEntryService();

            entryService
                .Setup(e => e.GetNumberOfEntries(It.IsAny<string>()))
                .Returns(5);

            var statisticsAssembler = new StatisticsAssembler(
                entryService.Object, GetDefaultTimeService().Object, GetDefaultUserSettingsService().Object);

            var statisticsViewModel = new HomeStatisticsViewModel(_baseDateTime, _baseDateTime.AddDays(3));
            var statisticsViewModelResult = statisticsAssembler.AssembleHomeStatisticsViewModel(statisticsViewModel, It.IsAny<string>());

            Assert.AreEqual(5, statisticsViewModelResult.NumberOfEntries);
        }

        [TestMethod]
        public void Test_Empty_Model_Is_Returned_When_There_Are_No_Entries()
        {
            var entryService = new Mock<IEntryService>();

            entryService
                .Setup(e => e.GetEntries(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .Returns(new List<Entry>());

            var statisticsAssembler = new StatisticsAssembler(
                entryService.Object, GetDefaultTimeService().Object, GetDefaultUserSettingsService().Object);

            var result = statisticsAssembler.AssembleHomeStatisticsViewModel("someUserId");

            Assert.IsTrue(!result.PerTag.Items.Any());
        }

        [TestMethod]
        public void Test_PerTag_ViewModel_Is_Calculated_Correctly()
        {                   
            var statisticsAssembler = new StatisticsAssembler(
                GetEntryService().Object, GetDefaultTimeService().Object, GetDefaultUserSettingsService().Object);

            var statisticsViewModel = new HomeStatisticsViewModel(_baseDateTime, _baseDateTime.AddDays(3));
            var statisticsViewModelResult = statisticsAssembler.AssembleHomeStatisticsViewModel(statisticsViewModel, It.IsAny<string>());

            var firstPerTag = statisticsViewModelResult.PerTag.Items.First();
            var secondPerTag = statisticsViewModelResult.PerTag.Items.ElementAt(1);

            Assert.AreEqual(1, firstPerTag.Time.Days);
            Assert.AreEqual(12, firstPerTag.Time.Hours);
            Assert.AreEqual(75.0, Math.Ceiling(firstPerTag.Percentage));
            
            Assert.AreEqual(0, secondPerTag.Time.Days);
            Assert.AreEqual(12, secondPerTag.Time.Hours);
            Assert.AreEqual(25, Math.Ceiling(secondPerTag.Percentage));            
        }

        [TestMethod]
        public void Test_Uses_TitleCased_Names_For_The_TagNames() 
        {
            var statisticsAssembler = new StatisticsAssembler(
                GetEntryService().Object, GetDefaultTimeService().Object, GetDefaultUserSettingsService().Object);

            var statisticsViewModel = new HomeStatisticsViewModel(_baseDateTime, _baseDateTime.AddDays(3));
            var statisticsViewModelResult = statisticsAssembler.AssembleHomeStatisticsViewModel(statisticsViewModel, It.IsAny<string>());

            var firstPerTag = statisticsViewModelResult.PerTag.Items.First();

            Assert.AreEqual("Commuting", firstPerTag.TagName);
        }        

        [TestMethod]
        public void Test_Orders_PerTag_Items_Descending()
        {
            var statisticsAssembler = new StatisticsAssembler(
                GetEntryService().Object, GetDefaultTimeService().Object, GetDefaultUserSettingsService().Object);

            var statisticsViewModel = new HomeStatisticsViewModel(_baseDateTime, _baseDateTime.AddDays(3));
            var statisticsViewModelResult = statisticsAssembler.AssembleHomeStatisticsViewModel(statisticsViewModel, It.IsAny<string>());

            var firstPerTag = statisticsViewModelResult.PerTag.Items.First();
            var secondPerTag = statisticsViewModelResult.PerTag.Items.ElementAt(1);

            Assert.IsTrue(firstPerTag.Percentage > secondPerTag.Percentage);
        }        

        private Mock<IUserSettingsService> GetDefaultUserSettingsService()
        {
            var userSettingsServiceMock = new Mock<IUserSettingsService>();
            userSettingsServiceMock
                .Setup(u => u.Get(It.IsAny<string>()))
                .Returns(new UserSettings() { TimeZoneId = "W. Europe Standard Time" });

            return userSettingsServiceMock;                   
        }

        private Mock<ITimeService> GetDefaultTimeService() 
        {
            var timeServiceMock = new Mock<ITimeService>();
            timeServiceMock
                .Setup(t => t.GetNow())
                .Returns(new DateTime(2011, 11, 10, 0, 0, 0));

            return timeServiceMock;
        }

        private Mock<IEntryService> GetEntryService()
        {
            var baseDateTime = new DateTime(2011, 11, 8, 0, 0, 0, DateTimeKind.Local);

            var entries = new List<Entry>() {
                new Entry() 
                    {
                        CreatedOn = baseDateTime,
                        Description = "Test 1",
                        Id = 1,
                        Location = new Location("Brussels", "1"),
                        StoppedOn = baseDateTime.AddHours(6),
                        Tag = new EntryTag("ComMuting", "#FFFHHH", "1"),
                        TagId = 1,
                        UserId = "1"
                    },
                    new Entry() 
                    {
                        CreatedOn = baseDateTime.AddHours(6),
                        Description = "Test 2",
                        Id = 1,
                        Location = new Location("Company", "1"),
                        StoppedOn = baseDateTime.AddHours(18),
                        Tag = new EntryTag("Work", "#FFF000", "1"),
                        TagId = 2,
                        UserId = "1"
                    },
                    new Entry() 
                    {
                        CreatedOn = baseDateTime.AddHours(18),
                        Description = "Test 3",
                        Id = 1,
                        Location = new Location("Geel", "1"),
                        StoppedOn = null,
                        Tag = new EntryTag("ComMuting", "#FFF000", "1"),
                        TagId = 1,
                        UserId = "1"
                    }
            };

            var entryService = new Mock<IEntryService>();
            entryService
                .Setup(e => e.GetEntries(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .Returns(entries);
            entryService
                .Setup(e => e.GetLatestEntry(It.IsAny<string>()))
                .Returns(entries.Last());
            entryService
                .Setup(e => e.GetFirstRealEntry(It.IsAny<string>()))
                .Returns(entries.First());

            return entryService;
        }
    }
}
