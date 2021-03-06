﻿using System;
using System.Collections.Generic;
using System.Linq;

using Docary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Docary.Services;
using Docary.ViewModelAssemblers.Mobile;
using Moq;

namespace Docary.ViewModelAssemblers.Test.Mobile
{
    [TestClass()]
    public class HomeAssemblerAssembleHomeIndexViewModelTest
    {
        [TestMethod()]
        public void Test_Initializes_EntryGroups()
        {
            var target = new HomeAssembler(
                GetEntryServiceStubForTestingEntryGroups(), 
                GetUserSettingStub(),
                GetTimeServiceStub());           
            
            var actual = target.AssembleHomeIndexViewModel(string.Empty);
          
            Assert.IsNotNull(actual.EntryGroups);
        }

        [TestMethod()]
        public void Test_Groups_Entries_Correctly()
        {
            var target = new HomeAssembler(
                GetEntryServiceStubForTestingEntryGroups(), 
                GetUserSettingStub(),
                GetTimeServiceStub());            

            var actual = target.AssembleHomeIndexViewModel(string.Empty);

            var firstEntryGroup = actual.EntryGroups.First();
            var secondEntryGroup = actual.EntryGroups.ElementAt(1);

            Assert.AreEqual(2, firstEntryGroup.Entries.Count());
            Assert.AreEqual(1, secondEntryGroup.Entries.Count());
        }

        private ITimeService GetTimeServiceStub()
        {
            var timeServiceStub = new Mock<ITimeService>();

            timeServiceStub
                .Setup(t => t.GetNow())
                .Returns(new DateTime(2011, 10, 17, 0, 0, 0, DateTimeKind.Utc));

            return timeServiceStub.Object;
        }

        private IUserSettingsService GetUserSettingStub()
        {
            var userSettingStub = new Mock<IUserSettingsService>();

            userSettingStub.Setup(u => u.Get(It.IsAny<string>()))
                            .Returns(new UserSettings() { UserId = "1", TimeZoneId = "W. Europe Standard Time" });

            return userSettingStub.Object;
        }

        private IEntryService GetEntryServiceStubForTestingEntryGroups()
        {
            var createdOnBase = new DateTime(2011, 10, 18, 1, 30, 30);

            var entries = new List<Entry>()
            {
                new Entry() { CreatedOn = createdOnBase, Id = 1, UserId = "1" },
                new Entry() { CreatedOn = createdOnBase.AddHours(24), Id = 2, UserId = "1" },
                new Entry() { CreatedOn = createdOnBase.AddHours(36), Id = 1, UserId = "1" }
            };

            var stub = new Mock<IEntryService>();

            stub.Setup(e => e.GetEntries(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .Returns(entries);

            return stub.Object;                                  
        }       
    }
}
