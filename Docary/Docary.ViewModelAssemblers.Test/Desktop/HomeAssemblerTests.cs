using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Docary.ViewModelAssemblers.Desktop;
using Docary.Services;
using Docary.Models;
using Moq;

namespace Docary.ViewModelAssemblers.Test.Desktop
{
    [TestClass]
    public class HomeAssemblerTests
    {        
        [TestMethod()]
        public void Test_AssembleHomeIndexViewModel_Groups_Entries_Correctly()
        {
            var target = new HomeAssembler(GetEntryServiceStubForTestingEntryGroups());

            var actual = target.AssembleHomeIndexViewModel(DateTime.MinValue, DateTime.MaxValue, "1");

            var firstEntryGroup = actual.EntryGroups.First();
            var secondEntryGroup = actual.EntryGroups.ElementAt(1);

            Assert.AreEqual(1, firstEntryGroup.Entries.Count());
            Assert.AreEqual(2, secondEntryGroup.Entries.Count());
        }

        [TestMethod()]
        public void Test_AssembleHomeIndexViewModel_Calculates_EntryPercentages_Correctly()
        {
            var target = new HomeAssembler(GetEntryServiceStubForTestingEntryGroups());

            var actual = target.AssembleHomeIndexViewModel(DateTime.MinValue, DateTime.MaxValue, "1");

            var firstEntryGroup = actual.EntryGroups.First();
            var secondEntryGroup = actual.EntryGroups.ElementAt(1);            
        }

        private IEntryService GetEntryServiceStubForTestingEntryGroups()
        {
            var createdOnBase = new DateTime(2011, 10, 18, 1, 30, 30);

            var entries = new List<Entry>()
            {
                new Entry() { CreatedOn = createdOnBase, StoppedOn = createdOnBase.AddHours(24), Id = 1, UserId = "1" },
                new Entry() { CreatedOn = createdOnBase.AddHours(24), StoppedOn = createdOnBase.AddHours(36), Id = 2, UserId = "1" },
                new Entry() { CreatedOn = createdOnBase.AddHours(36), Id = 1, UserId = "1" }
            };

            var stub = new Mock<IEntryService>();

            stub.Setup(e => e.GetEntries(DateTime.MinValue, DateTime.MaxValue, "1"))
                .Returns(entries);

            return stub.Object;
        }       
    }
}
