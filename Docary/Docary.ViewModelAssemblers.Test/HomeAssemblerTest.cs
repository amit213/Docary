using System;
using System.Collections.Generic;
using System.Linq;

using Docary.Models;
using Docary.ViewModelAssemblers.Test.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Docary.ViewModelAssemblers.Test
{
    [TestClass()]
    public class HomeAssemblerTest
    {
        [TestMethod()]
        public void Test_AssembleHomeIndexViewModel_Initializes_EntryGroups()
        {
            var target = new HomeAssembler(GetEntryServiceStubForTestingEntryGroups());

            var actual = target.AssembleHomeIndexViewModel("1");

            Assert.IsNotNull(actual.EntryGroups);
        }

        [TestMethod()]
        public void Test_AssembleHomeIndexViewModel_Groups_Entries_Correctly()
        {            
            var target = new HomeAssembler(GetEntryServiceStubForTestingEntryGroups());       
            
            var actual = target.AssembleHomeIndexViewModel("1");

            var firstEntryGroup = actual.EntryGroups.First();
            var secondEntryGroup = actual.EntryGroups.ElementAt(1);

            Assert.AreEqual(firstEntryGroup.Entries.Count(), 2);
            Assert.AreEqual(secondEntryGroup.Entries.Count(), 1);
        }

        private EntryServiceStub GetEntryServiceStubForTestingEntryGroups()
        {
            var entryServiceStub = new EntryServiceStub();                       
            
            var entries = new List<Entry>()
            {
                new Entry() {                 
                    CreatedOn = DateTime.Now.Date,
                    Id = 1,                    
                    Description = "Bla",
                    UserId = "1"
                },
                new Entry() {                    
                    CreatedOn = DateTime.Now.Date.AddHours(15),
                    Id = 2,                    
                    Description = "Bla",
                    UserId = "1"
                },
                new Entry() {                    
                    CreatedOn = DateTime.Now.AddDays(1),
                    Id = 1,                    
                    Description = "Bla",
                    UserId = "1"
                }
            };

            entryServiceStub.Seed(entries);

            return entryServiceStub;
        }       
    }
}
