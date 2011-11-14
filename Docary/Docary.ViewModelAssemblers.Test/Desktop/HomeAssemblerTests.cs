﻿using System;
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

            var createdOnMin = new DateTime(2011, 10, 18, 0, 0, 0);
            var createdOnMax = new DateTime(2011, 10, 21, 0, 0, 0);
            
            var actual = target.AssembleHomeIndexViewModel(createdOnMin, createdOnMax, "1");

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
            var target = new HomeAssembler(GetEntryServiceStubForTestingEntryGroups());

            var createdOnMin = new DateTime(2011, 10, 18, 0, 0, 0);
            var createdOnMax = new DateTime(2011, 10, 21, 0, 0, 0);

            var actual = target.AssembleHomeIndexViewModel(createdOnMin, createdOnMax, "1");

            var firstEntryGroup = actual.EntryGroups.First();
            var secondEntryGroup = actual.EntryGroups.ElementAt(1);
            var thirdEntryGroup = actual.EntryGroups.ElementAt(2);

            Assert.AreEqual(94, firstEntryGroup.Entries.First().Percentage);
            Assert.AreEqual(6, secondEntryGroup.Entries.First().Percentage);
            Assert.AreEqual(50, secondEntryGroup.Entries.ElementAt(1).Percentage); 
            Assert.AreEqual(44, secondEntryGroup.Entries.ElementAt(2).Percentage);
            Assert.AreEqual(100, thirdEntryGroup.Entries.First().Percentage);
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
            stub.Setup(e => e.GetEntries(new DateTime(2011, 10, 18, 0, 0, 0), new DateTime(2011, 10, 21, 0, 0, 0), "1"))
                .Returns(entries);               

            return stub.Object;
        }       
    }
}
