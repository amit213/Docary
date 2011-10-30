using Docary.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

using Docary.Repositories;
using Docary.Models;
using Docary.Services.Tests.Mocks;
using Docary.Services.Tests.Stubs;

namespace Docary.Services.Tests
{  
    [TestClass()]
    public class EntryServiceTest
    {
        private EntryRepositoryMock _entryRepoMock;
        private LocationRepositoryMock _locationRepoMock;
        private TagRepositoryMock _tagRepoMock;
        private IEntryService _entryServiceMock;
        private ITimeService _timeServiceStub;

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
        public void Test_AddEntry_Adds_Unresolvable_Location() 
        {
            InitializeEntryService();

            var newEntry = new Entry() {
                Location = new Location() { Name = "Unresolvable_Location_Name" },
                Tag = new EntryTag() { Name = "Test_Tag_Name" },
                UserId = "1"
            };

            _entryServiceMock.AddEntry(newEntry);

            var locationAdded = _locationRepoMock.Locations.Where(e => e.Name == "Unresolvable_Location_Name").FirstOrDefault();

            Assert.IsNotNull(locationAdded);
        }

        [TestMethod]
        public void Test_AddEntry_Does_Not_Add_Existing_Location()
        {
            InitializeEntryService();

            var originalLocationCount = _locationRepoMock.Locations.Count();

            var newEntry = new Entry()
            {
                Location = new Location() { Name = "TestLocation" },
                Tag = new EntryTag() { Name = "TestTag" },
                UserId = "1"
            };

            _entryServiceMock.AddEntry(newEntry);

            var locationCountAfterAdding = _locationRepoMock.Locations.Count();

            Assert.AreEqual(locationCountAfterAdding, originalLocationCount);
        }

        [TestMethod]
        public void Test_AddEntry_Does_Not_Add_Existing_Tag()
        {
            InitializeEntryService();

            var originalTagCount = _tagRepoMock.Tags.Count();

            var newEntry = new Entry()
            {
                Location = new Location() { Name = "TestLocation" },
                Tag = new EntryTag() { Name = "TestTag" },
                UserId = "1"
            };

            _entryServiceMock.AddEntry(newEntry);

            var tagCountAfterAdding = _tagRepoMock.Tags.Count();

            Assert.AreEqual(tagCountAfterAdding, originalTagCount);
        }

        [TestMethod]
        public void Test_AddEntry_Adds_Unresolvable_EntryTag()
        {
            InitializeEntryService();

            var newEntry = new Entry()
            {
                Location = new Location() { Name = "Test_Location_Name" },
                Tag = new EntryTag() { Name = "Unresolvable_Tag_Name" },
                UserId = "1"
            };

            _entryServiceMock.AddEntry(newEntry);

            var tagAdded = _tagRepoMock.Tags.Where(t => t.Name == "Unresolvable_Tag_Name").FirstOrDefault();

            Assert.IsNotNull(tagAdded);
        }

        [TestMethod]
        public void Test_AddEntry_Adds_Entry()
        {
            InitializeEntryService();

            var entryCountBeforeAdding = _entryRepoMock.Entries.Count();

            var newEntry = new Entry()
            {
                Location = new Location() { Name = "Test_Location" },
                Tag = new EntryTag() { Name = "Test_Tag" },
                UserId = "1"
            };
                        
            _entryServiceMock.AddEntry(newEntry);

            var entryCountAfterAdding = _entryRepoMock.Entries.Count();

            var entryCountDiff = entryCountAfterAdding - entryCountBeforeAdding;

            Assert.AreEqual(1, entryCountDiff);
        }

        [TestMethod]
        public void Test_AddEntry_Updates_Previous_Entry_Stopped_On_Property_To_Now()
        {
            InitializeEntryService();

            var lastEntryBeforeAdding = _entryRepoMock.Entries.Where(e => e.StoppedOn == null).First();

            var newEntry = new Entry()
            {
                Location = new Location() { Name = "Test_Location" },
                Tag = new EntryTag() { Name = "Test_Tag" },
                UserId = "1"
            };

            _entryServiceMock.AddEntry(newEntry);

            var lastEntryAfterAdding = _entryRepoMock.Entries.Where(e => e.Id == lastEntryBeforeAdding.Id).First();
            
            Assert.AreEqual(_timeServiceStub.GetNow(), lastEntryAfterAdding.StoppedOn);
        }       

        private void InitializeEntryService()
        {
            _entryRepoMock = new EntryRepositoryMock();
            _locationRepoMock = new LocationRepositoryMock();
            _tagRepoMock = new TagRepositoryMock();
            _timeServiceStub = new TimeServiceStub();

            _entryServiceMock = new EntryService(_entryRepoMock, _locationRepoMock, _tagRepoMock, _timeServiceStub);
        }
    }
}
