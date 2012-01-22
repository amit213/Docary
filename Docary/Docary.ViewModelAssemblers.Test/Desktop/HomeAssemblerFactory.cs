using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Services;
using Docary.Models;
using Moq;
using Docary.ViewModelAssemblers.Desktop;

namespace Docary.ViewModelAssemblers.Test.Desktop
{
    public class HomeAssemblerFactory
    {
        public static HomeAssembler SetupHomeAssembler()
        {
            return new HomeAssembler(
                GetEntryServiceStub(),
                GetTimeServiceStub(),
                GetUserSettingsServiceStub());
        }      

        private static IUserSettingsService GetUserSettingsServiceStub()
        {
            var userSettingsServiceStub = new Mock<IUserSettingsService>();

            userSettingsServiceStub.Setup(u => u.Get(It.IsAny<string>()))
                                    .Returns(new UserSettings() { UserId = "1", TimeZoneId = "W. Europe Standard Time" });

            return userSettingsServiceStub.Object;
        }

        private static ITimeService GetTimeServiceStub()
        {
            var timeServiceStub = new Mock<ITimeService>();

            timeServiceStub.Setup(s => s.GetNow())
                           .Returns(new DateTime(2011, 10, 18, 12, 30, 00));

            return timeServiceStub.Object;
        }

        private static IEntryService GetEntryServiceStub()
        {
            var createdOnBase = new DateTime(2011, 10, 18, 1, 30, 30, DateTimeKind.Utc);

            var entries = new List<Entry>()
            {
                new Entry() { CreatedOn = createdOnBase, StoppedOn = createdOnBase.AddHours(24), Id = 1, UserId = "1", Tag = new EntryTag() { Name = "TeST1", Color = "Yellow" } },
                new Entry() { CreatedOn = createdOnBase.AddHours(24), StoppedOn = createdOnBase.AddHours(36), Id = 2, UserId = "1", Tag = new EntryTag() { Name = "Test2", Color = "Green" } },
                new Entry() { CreatedOn = createdOnBase.AddHours(36), Id = 1, UserId = "1", Tag = new EntryTag() { Name = "Test3", Color = "Red" } }
            };

            var stub = new Mock<IEntryService>();

            stub.Setup(e => e.GetEntries(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .Returns(entries);

            return stub.Object;
        }       
    }
}
