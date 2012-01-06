using Docary.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Docary.Repositories;
using Docary.Models;
using Docary.Repositories.EF;
using Moq;

namespace Docary.Services.Tests
{      
    [TestClass()]
    public class EntryServiceTest
    {   
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_AddEntry_Throws_ArgumentNullException_On_Null_Entry()
        {
            EntryServiceFactory
                .SetupEntryService()
                .AddEntry(null);            
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_AddEntry_Throws_ArgumentNullException_On_Null_EmptyUserId()
        {          
            var newEntryWithoutUserId = new Entry() {                
                Description = "Test",
                Location = new Location() { Name = "Test" },
                Tag = new EntryTag() { Name = "Test" }
            };

            EntryServiceFactory
                .SetupEntryService()
                .AddEntry(newEntryWithoutUserId);
        }
       
        [TestMethod]
        public void Test_AddEntry_Adds_Unresolvable_Location() 
        {
            var entryRepository = new Mock<IEntryRepository>();
            var locationRepository = new Mock<ILocationRepository>();
            var tagRepository = new Mock<ITagRepository>();
            var timeLineServiceStub = new Mock<ITimelineColorService>();            

            timeLineServiceStub.Setup(t => t.GetRandom())
                               .Returns(new TimelineColor("#FFF"));
            entryRepository.Setup(e => e.IsEmpty(It.IsAny<string>()))
                           .Returns(false);
            locationRepository.Setup(l => l.Find(It.IsAny<string>(), It.IsAny<string>()))
                              .Returns((Location)null);                               

            var newEntry = new Entry() {
                Location = new Location() { Name = "Unresolvable_Location_Name" },
                Tag = new EntryTag() { Name = "Test_Tag_Name" },
                UserId = "1"
            };

            EntryServiceFactory.SetupEntryService(
                entryRepository,
                locationRepository,
                tagRepository,
                timeLineServiceStub).AddEntry(newEntry);

            locationRepository.Verify(r => r.Add(It.IsAny<Location>()), Times.Once());           
        }

        [TestMethod]
        public void Test_AddEntry_Does_Not_Add_Existing_Location()
        {
            var entryRepository = new Mock<IEntryRepository>();
            var locationRepository = new Mock<ILocationRepository>();
            var tagRepository = new Mock<ITagRepository>();
            var timeLineServiceStub = new Mock<ITimelineColorService>();                        

            timeLineServiceStub.Setup(t => t.GetRandom())
                                .Returns(new TimelineColor("#FFF"));
            entryRepository.Setup(e => e.IsEmpty(It.IsAny<string>()))
                            .Returns(false);
            locationRepository.Setup(l => l.Find(It.IsAny<string>(), It.IsAny<string>()))
                                .Returns(new Location());

            var entryService = EntryServiceFactory.SetupEntryService(
                entryRepository,
                locationRepository,
                tagRepository,
                timeLineServiceStub);

            var newEntry = new Entry()
            {
                Location = new Location() { Name = "TestLocation" },
                Tag = new EntryTag() { Name = "TestTag" },
                UserId = "1"
            };

            entryService.AddEntry(newEntry);

            locationRepository.Verify(l => l.Add(It.IsAny<Location>()), Times.Never());
        }

        [TestMethod]
        public void Test_AddEntry_Does_Not_Add_Existing_Tag()
        {
            var entryRepository = new Mock<IEntryRepository>();
            var locationRepository = new Mock<ILocationRepository>();
            var tagRepository = new Mock<ITagRepository>();
            var timeLineServiceStub = new Mock<ITimelineColorService>();                  
          
            entryRepository.Setup(e => e.IsEmpty(It.IsAny<string>()))
                            .Returns(false);
            tagRepository.Setup(l => l.Find(It.IsAny<string>(), It.IsAny<string>()))
                                .Returns(new EntryTag());

            var entryService = EntryServiceFactory.SetupEntryService(
                entryRepository,
                locationRepository,
                tagRepository,
                timeLineServiceStub);

            var newEntry = new Entry()
            {
                Location = new Location() { Name = "TestLocation" },
                Tag = new EntryTag() { Name = "TestTag" },
                UserId = "1"
            };

            entryService.AddEntry(newEntry);

            tagRepository.Verify(e => e.Add(It.IsAny<EntryTag>()), Times.Never());
        }

        [TestMethod]
        public void Test_AddEntry_Adds_Unresolvable_EntryTag()
        {
            var entryRepository = new Mock<IEntryRepository>();
            var locationRepository = new Mock<ILocationRepository>();
            var tagRepository = new Mock<ITagRepository>();
            var timeLineServiceStub = new Mock<ITimelineColorService>();            

            entryRepository.Setup(e => e.IsEmpty(It.IsAny<string>()))
                            .Returns(false);
            tagRepository.Setup(l => l.Find(It.IsAny<string>(), It.IsAny<string>()))
                                .Returns((EntryTag)null);
            timeLineServiceStub.Setup(t => t.GetRandom())
                            .Returns(new TimelineColor("#FFF"));

            var entryService = EntryServiceFactory.SetupEntryService(
                entryRepository,
                locationRepository,
                tagRepository,
                timeLineServiceStub); 

            var newEntry = new Entry()
            {
                Location = new Location() { Name = "Test_Location_Name" },
                Tag = new EntryTag() { Name = "Unresolvable_Tag_Name" },
                UserId = "1"
            };

            entryService.AddEntry(newEntry);

            tagRepository.Verify(e => e.Add(It.IsAny<EntryTag>()), Times.Once());
        }            

        [TestMethod]
        public void Test_AddEntry_Adds_Entry()
        {
            var entryRepository = new Mock<IEntryRepository>();
            var locationRepository = new Mock<ILocationRepository>();
            var tagRepository = new Mock<ITagRepository>();
            var timeLineServiceStub = new Mock<ITimelineColorService>();                       

            timeLineServiceStub.Setup(t => t.GetRandom())
                            .Returns(new TimelineColor("#FFF"));
            entryRepository.Setup(e => e.IsEmpty(It.IsAny<string>()))
                            .Returns(false);

            var entryService = EntryServiceFactory.SetupEntryService(
                entryRepository,
                locationRepository,
                tagRepository,
                timeLineServiceStub);               

            var newEntry = new Entry()
            {
                Location = new Location() { Name = "Test_Location" },
                Tag = new EntryTag() { Name = "Test_Tag" },
                UserId = "1"
            };

            entryService.AddEntry(newEntry);

            entryRepository.Verify(e => e.Add(It.IsAny<Entry>()), Times.Once());
        }

        [TestMethod]
        public void Test_Add_Adds_And_Also_Adds_OffTheGridEntry_When_Empty_EntryRepository()
        {
            var entryRepository = new Mock<IEntryRepository>();
            var locationRepository = new Mock<ILocationRepository>();
            var tagRepository = new Mock<ITagRepository>();
            var timeLineServiceStub = new Mock<ITimelineColorService>();                 

            timeLineServiceStub.Setup(t => t.GetRandom())
                            .Returns(new TimelineColor("#FFF"));
            entryRepository.Setup(e => e.IsEmpty(It.IsAny<string>()))
                            .Returns(true);

            var entryService = EntryServiceFactory.SetupEntryService(
                entryRepository,
                locationRepository,
                tagRepository,
                timeLineServiceStub);

            var newEntry = new Entry()
            {
                Location = new Location() { Name = "Test_Location" },
                Tag = new EntryTag() { Name = "Test_Tag" },
                UserId = "1"
            };

            entryService.AddEntry(newEntry);

            entryRepository.Verify(e => e.Add(It.IsAny<Entry>()), Times.Exactly(2));
        }

        [TestMethod]
        public void Test_GetNumberOfEntries_Returns_The_Number_Of_Entries()
        {
            var entryRepository = new Mock<IEntryRepository>();
            var locationRepository = new Mock<ILocationRepository>();
            var tagRepository = new Mock<ITagRepository>();
            var timeLineServiceStub = new Mock<ITimelineColorService>();
                   
            entryRepository.Setup(e => e.Count(It.IsAny<string>())).Returns(5);

            var entryService = EntryServiceFactory.SetupEntryService(
                entryRepository,
                locationRepository,
                tagRepository,
                timeLineServiceStub);

            Assert.AreEqual(5, entryService.GetNumberOfEntries("1"));
        }      
        
    }
}
