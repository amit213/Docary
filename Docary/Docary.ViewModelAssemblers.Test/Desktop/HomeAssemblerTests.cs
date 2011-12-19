using System;
using System.Collections.Generic;
using System.Linq;
using Docary.Models;
using Docary.Services;
using Docary.ViewModelAssemblers.Desktop;
using Docary.ViewModels.Desktop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Docary.ViewModelAssemblers.Test.Desktop
{
    [TestClass]
    public class HomeAssemblerTests
    {
        private const string TIMEZONE = "W. Europe Standard Time";

        [TestMethod()]
        public void Test_AssembleHomeIndexViewModel_Defaults_Dates()
        {        
            var actual =  GetHomeAssembler().AssembleHomeIndexViewModel("1");

            Assert.IsNotNull(actual.To);
            Assert.IsNotNull(actual.From);
        }
   
        [TestMethod()]
        public void Test_AssembleHomeIndexViewModel_Groups_Entries_Correctly()
        {           
            var createdOnMin = new DateTime(2011, 10, 18, 0, 0, 0, DateTimeKind.Local);
            var createdOnMax = new DateTime(2011, 10, 21, 0, 0, 0, DateTimeKind.Local);

            var homeIndexViewModel = new HomeIndexViewModel(createdOnMin, createdOnMax);

            var actual = GetHomeAssembler().AssembleHomeIndexViewModel(homeIndexViewModel, It.IsAny<string>());

            var firstEntryGroup = actual.EntryGroups.First();
            var secondEntryGroup = actual.EntryGroups.ElementAt(1);
            var thirdEntryGroup = actual.EntryGroups.ElementAt(2);

            Assert.AreEqual(1, firstEntryGroup.Entries.Count());
            Assert.AreEqual(3, secondEntryGroup.Entries.Count());
            Assert.AreEqual(1, thirdEntryGroup.Entries.Count());
        }
      
        [TestMethod()]
        public void Test_AssembleHomeIndexViewModel_Calculates_EntryPercentages_Correctly()
        {
            var createdOnMin = new DateTime(2011, 10, 18, 0, 0, 0, DateTimeKind.Local);
            var createdOnMax = new DateTime(2011, 10, 21, 0, 0, 0, DateTimeKind.Local);

            var homeIndexViewModel = new HomeIndexViewModel(createdOnMin, createdOnMax);

            var actual = GetHomeAssembler().AssembleHomeIndexViewModel(homeIndexViewModel, It.IsAny<string>());

            var firstEntryGroup = actual.EntryGroups.First();
            var secondEntryGroup = actual.EntryGroups.ElementAt(1);
            var thirdEntryGroup = actual.EntryGroups.ElementAt(2);

            Assert.AreEqual(85, Convert.ToInt32(firstEntryGroup.Entries.First().Percentage));           
            Assert.AreEqual(15, Convert.ToInt32(secondEntryGroup.Entries.First().Percentage));
            Assert.AreEqual(50, Convert.ToInt32(secondEntryGroup.Entries.ElementAt(1).Percentage)); 
            Assert.AreEqual(35, Convert.ToInt32(secondEntryGroup.Entries.ElementAt(2).Percentage));
            Assert.AreEqual(100, Convert.ToInt32(thirdEntryGroup.Entries.First().Percentage));
        }

        [TestMethod()]
        public void Test_AssembleHomeIndexViewModel_Populates_The_Tag_Property()
        {
            var createdOnMin = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(TIMEZONE));
            var createdOnMax = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddDays(3), TimeZoneInfo.FindSystemTimeZoneById(TIMEZONE));

            var homeIndexViewModel = new HomeIndexViewModel(createdOnMin, createdOnMax);

            var actual = GetHomeAssembler().AssembleHomeIndexViewModel(homeIndexViewModel, It.IsAny<string>());

            foreach (var entryGroup in actual.EntryGroups)
            {
                foreach (var entry in entryGroup.Entries)               
                    Assert.IsFalse(string.IsNullOrEmpty(entry.Tag));                                    
            }
        }

        private HomeAssembler GetHomeAssembler()
        {
            return new HomeAssembler(
                GetEntryServiceStub(), 
                GetTimeServiceStub(),
                GetUserSettingsServiceStub());
        }

        private IUserSettingsService GetUserSettingsServiceStub()
        {
            var userSettingsServiceStub = new Mock<IUserSettingsService>();

            userSettingsServiceStub.Setup(u => u.Get(It.IsAny<string>()))
                                    .Returns(new UserSettings() { UserId = "1", TimeZoneId = "W. Europe Standard Time" });

            return userSettingsServiceStub.Object;
        }

        private ITimeService GetTimeServiceStub()
        {
            var timeServiceStub = new Mock<ITimeService>();

            timeServiceStub.Setup(s => s.GetNow())
                           .Returns(new DateTime(2011, 10, 18, 12, 30 , 00));

            return timeServiceStub.Object;
        }

        private IEntryService GetEntryServiceStub()
        {
            var createdOnBase = new DateTime(2011, 10, 18, 1, 30, 30, DateTimeKind.Utc);

            var entries = new List<Entry>()
            {
                new Entry() { CreatedOn = createdOnBase, StoppedOn = createdOnBase.AddHours(24), Id = 1, UserId = "1", Tag = new EntryTag() { Name = "Test1" } },
                new Entry() { CreatedOn = createdOnBase.AddHours(24), StoppedOn = createdOnBase.AddHours(36), Id = 2, UserId = "1", Tag = new EntryTag() { Name = "Test2" } },
                new Entry() { CreatedOn = createdOnBase.AddHours(36), Id = 1, UserId = "1", Tag = new EntryTag() { Name = "Test3" } }
            };

            var stub = new Mock<IEntryService>();

            stub.Setup(e => e.GetEntries(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .Returns(entries);
            
            return stub.Object;
        }       
    }
}
