using Docary.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using Docary.Repositories;
using Docary.Models;
using Docary.Services.Tests.Mocks;

namespace Docary.Services.Tests
{  
    [TestClass()]
    public class EntryServiceTest
    {
        private EntryRepositoryMock _entryRepoMock;
        private LocationRepositoryMock _locationRepoMock;
        private TagRepositoryMock _tagRepoMock;
        private IEntryService _entryServiceMock;

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_AddEntry_Throws_ArgumentNullException_On_Null_Entry()
        {
            InitializeEntryService();

            _entryServiceMock.AddEntry(null);            
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_AddEntry_Throws_ArgumentNullException_On_Null_EmptyUserId()
        {
            InitializeEntryService();

            var newEntryWithoutUserId = new Entry() {                
                Description = "Test",
                Location = new Location() { Name = "Test" },
                Tag = new EntryTag() { Name = "Test" }
            };

            _entryServiceMock.AddEntry(newEntryWithoutUserId);
        }
       
        [TestMethod]
        public void Test_AddEntry_Can_Resolve_Location_By_Name_And_UserId() 
        {
            InitializeEntryService();

            var newEntry = new Entry() {
                Location = new Location() { Name = "Test" }
            };

            Assert.Inconclusive("To implement.");


            //target.AddEntry(null);
        }

        private void InitializeEntryService()
        {
            _entryRepoMock = new EntryRepositoryMock();
            _locationRepoMock = new LocationRepositoryMock();
            _tagRepoMock = new TagRepositoryMock();      

            _entryServiceMock = new EntryService(_entryRepoMock, _locationRepoMock, _tagRepoMock);
        }
    }
}
