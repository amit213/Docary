using System;
using System.Collections.Generic;
using System.Linq;

using Docary.Models;
using Docary.ViewModelAssemblers.Test.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Docary.Services;

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

        private EntryService GetEntryServiceStubForTestingEntryGroups()
        {
            var entryRepositoryStub = new EntryRepositoryStub();            

            var createdOnBase = new DateTime(2011, 10, 18, 1, 30, 30);

            var entries = new List<Entry>()
            {
                new Entry() {                 
                    CreatedOn = createdOnBase,
                    Id = 1,                                       
                    UserId = "1"
                },
                new Entry() {                    
                    CreatedOn = createdOnBase.AddHours(24),
                    Id = 2,                                        
                    UserId = "1"
                },
                new Entry() {                    
                    CreatedOn = createdOnBase.AddHours(36),
                    Id = 1,                                        
                    UserId = "1"
                }
            };

            entryRepositoryStub.Seed(entries);

            return new EntryService(entryRepositoryStub, null, null, null);
        }       
    }
}
