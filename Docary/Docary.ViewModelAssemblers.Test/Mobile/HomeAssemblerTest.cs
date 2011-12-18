using System;
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
    public class HomeAssemblerTest
    {
        [TestMethod()]
        public void Test_AssembleHomeIndexViewModel_Initializes_EntryGroups()
        {
            var target = new HomeAssembler(GetEntryServiceStubForTestingEntryGroups(), GetUserSettingStub());

            var createdOnMin = new DateTime(2011, 10, 17, 0, 0, 0, DateTimeKind.Local);
            var createdOnMax = new DateTime(2011, 10, 20, 0, 0, 0, DateTimeKind.Local);

            var actual = target.AssembleHomeIndexViewModel(createdOnMin, createdOnMax, string.Empty);
          
            Assert.IsNotNull(actual.EntryGroups);
        }

        [TestMethod()]
        public void Test_AssembleHomeIndexViewModel_Groups_Entries_Correctly()
        {
            var target = new HomeAssembler(GetEntryServiceStubForTestingEntryGroups(), GetUserSettingStub());

            var createdOnMin = new DateTime(2011, 10, 17, 0, 0, 0, DateTimeKind.Local);
            var createdOnMax = new DateTime(2011, 10, 20, 0, 0, 0, DateTimeKind.Local);

            var actual = target.AssembleHomeIndexViewModel(createdOnMin, createdOnMax, string.Empty);

            var firstEntryGroup = actual.EntryGroups.First();
            var secondEntryGroup = actual.EntryGroups.ElementAt(1);

            Assert.AreEqual(2, firstEntryGroup.Entries.Count());
            Assert.AreEqual(1, secondEntryGroup.Entries.Count());
        }

        private IUserSettingService GetUserSettingStub()
        {
            var userSettingStub = new Mock<IUserSettingService>();

            userSettingStub.Setup(u => u.Get(It.IsAny<string>()))
                            .Returns(new UserSetting() { UserId = "1", TimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time") });

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
