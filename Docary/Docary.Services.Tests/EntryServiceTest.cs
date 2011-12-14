using Docary.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

using Docary.Repositories;
using Docary.Models;

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
            SetupReallyBasicEntryService().AddEntry(null);            
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

            SetupReallyBasicEntryService().AddEntry(newEntryWithoutUserId);
        }
       
        [TestMethod]
        public void Test_AddEntry_Adds_Unresolvable_Location() 
        {
            var entryRepository = new Mock<IEntryRepository>();
            var locationRepository = new Mock<ILocationRepository>();
            var tagRepository = new Mock<ITagRepository>();
            var timeLineServiceStub = new Mock<ITimelineColorService>();
            var timeServiceStub = new Mock<ITimeService>();

            timeLineServiceStub.Setup(t => t.GetRandom())
                                .Returns(new TimelineColor("#FFF"));
            entryRepository.Setup(e => e.IsEmpty(It.IsAny<string>()))
                            .Returns(false);
            locationRepository.Setup(l => l.Find(It.IsAny<string>(), It.IsAny<string>()))
                                .Returns((Location)null);
                                
            var entryService = new Mock<EntryService>(
                entryRepository.Object,
                locationRepository.Object,
                tagRepository.Object,
                timeLineServiceStub.Object,
                timeServiceStub.Object);        

            var newEntry = new Entry() {
                Location = new Location() { Name = "Unresolvable_Location_Name" },
                Tag = new EntryTag() { Name = "Test_Tag_Name" },
                UserId = "1"
            };

            entryService.Object.AddEntry(newEntry);

            locationRepository.Verify(r => r.Add(It.IsAny<Location>()), Times.Once());           
        }

        [TestMethod]
        public void Test_AddEntry_Does_Not_Add_Existing_Location()
        {
            var entryRepository = new Mock<IEntryRepository>();
            var locationRepository = new Mock<ILocationRepository>();
            var tagRepository = new Mock<ITagRepository>();
            var timeLineServiceStub = new Mock<ITimelineColorService>();
            var timeServiceStub = new Mock<ITimeService>();

            timeLineServiceStub.Setup(t => t.GetRandom())
                                .Returns(new TimelineColor("#FFF"));
            entryRepository.Setup(e => e.IsEmpty(It.IsAny<string>()))
                            .Returns(false);
            locationRepository.Setup(l => l.Find(It.IsAny<string>(), It.IsAny<string>()))
                                .Returns(new Location());

            var entryService = new Mock<EntryService>(
                entryRepository.Object,
                locationRepository.Object,
                tagRepository.Object,
                timeLineServiceStub.Object,
                timeServiceStub.Object);                 

            var newEntry = new Entry()
            {
                Location = new Location() { Name = "TestLocation" },
                Tag = new EntryTag() { Name = "TestTag" },
                UserId = "1"
            };

            entryService.Object.AddEntry(newEntry);

            locationRepository.Verify(l => l.Add(It.IsAny<Location>()), Times.Never());
        }

        [TestMethod]
        public void Test_AddEntry_Does_Not_Add_Existing_Tag()
        {
            var entryRepository = new Mock<IEntryRepository>();
            var locationRepository = new Mock<ILocationRepository>();
            var tagRepository = new Mock<ITagRepository>();
            var timeLineServiceStub = new Mock<ITimelineColorService>();
            var timeServiceStub = new Mock<ITimeService>();
          
            entryRepository.Setup(e => e.IsEmpty(It.IsAny<string>()))
                            .Returns(false);
            tagRepository.Setup(l => l.Find(It.IsAny<string>(), It.IsAny<string>()))
                                .Returns(new EntryTag());

            var entryService = new Mock<EntryService>(
               entryRepository.Object,
               locationRepository.Object,
               tagRepository.Object,
               timeLineServiceStub.Object,
               timeServiceStub.Object);   

            var newEntry = new Entry()
            {
                Location = new Location() { Name = "TestLocation" },
                Tag = new EntryTag() { Name = "TestTag" },
                UserId = "1"
            };

            entryService.Object.AddEntry(newEntry);

            tagRepository.Verify(e => e.Add(It.IsAny<EntryTag>()), Times.Never());
        }

        [TestMethod]
        public void Test_AddEntry_Adds_Unresolvable_EntryTag()
        {
            var entryRepository = new Mock<IEntryRepository>();
            var locationRepository = new Mock<ILocationRepository>();
            var tagRepository = new Mock<ITagRepository>();
            var timeLineServiceStub = new Mock<ITimelineColorService>();
            var timeServiceStub = new Mock<ITimeService>();

            entryRepository.Setup(e => e.IsEmpty(It.IsAny<string>()))
                            .Returns(false);
            tagRepository.Setup(l => l.Find(It.IsAny<string>(), It.IsAny<string>()))
                                .Returns((EntryTag)null);
            timeLineServiceStub.Setup(t => t.GetRandom())
                            .Returns(new TimelineColor("#FFF"));

            var entryService = new Mock<EntryService>(
               entryRepository.Object,
               locationRepository.Object,
               tagRepository.Object,
               timeLineServiceStub.Object,
               timeServiceStub.Object);   

            var newEntry = new Entry()
            {
                Location = new Location() { Name = "Test_Location_Name" },
                Tag = new EntryTag() { Name = "Unresolvable_Tag_Name" },
                UserId = "1"
            };

            entryService.Object.AddEntry(newEntry);

            tagRepository.Verify(e => e.Add(It.IsAny<EntryTag>()), Times.Once());
        }            

        [TestMethod]
        public void Test_AddEntry_Adds_Entry()
        {
            var entryRepository = new Mock<IEntryRepository>();
            var locationRepository = new Mock<ILocationRepository>();
            var tagRepository = new Mock<ITagRepository>();
            var timeLineServiceStub = new Mock<ITimelineColorService>();
            var timeServiceStub = new Mock<ITimeService>();

            timeLineServiceStub.Setup(t => t.GetRandom())
                            .Returns(new TimelineColor("#FFF"));
            entryRepository.Setup(e => e.IsEmpty(It.IsAny<string>()))
                            .Returns(false);

            var entryService = new Mock<EntryService>(
               entryRepository.Object,
               locationRepository.Object,
               tagRepository.Object,
               timeLineServiceStub.Object,
               timeServiceStub.Object);   

            var newEntry = new Entry()
            {
                Location = new Location() { Name = "Test_Location" },
                Tag = new EntryTag() { Name = "Test_Tag" },
                UserId = "1"
            };

            entryService.Object.AddEntry(newEntry);

            entryRepository.Verify(e => e.Add(It.IsAny<Entry>()), Times.Once());
        }

        [TestMethod]
        public void Test_Add_Adds_Also_Adds_OffTheGridEntry_When_Empty_EntryRepository()
        {
            var entryRepository = new Mock<IEntryRepository>();
            var locationRepository = new Mock<ILocationRepository>();
            var tagRepository = new Mock<ITagRepository>();
            var timeLineServiceStub = new Mock<ITimelineColorService>();
            var timeServiceStub = new Mock<ITimeService>();

            timeLineServiceStub.Setup(t => t.GetRandom())
                            .Returns(new TimelineColor("#FFF"));
            entryRepository.Setup(e => e.IsEmpty(It.IsAny<string>()))
                            .Returns(true);

            var entryService = new Mock<EntryService>(
               entryRepository.Object,
               locationRepository.Object,
               tagRepository.Object,
               timeLineServiceStub.Object,
               timeServiceStub.Object);

            var newEntry = new Entry()
            {
                Location = new Location() { Name = "Test_Location" },
                Tag = new EntryTag() { Name = "Test_Tag" },
                UserId = "1"
            };

            entryService.Object.AddEntry(newEntry);

            entryRepository.Verify(e => e.Add(It.IsAny<Entry>()), Times.Exactly(2));
        }         
       
        private IEntryService SetupReallyBasicEntryService()
        {
            var entryRepository = new Mock<IEntryRepository>();
            var locationRepository = new Mock<ILocationRepository>();
            var tagRepository = new Mock<ITagRepository>();
            var timeLineServiceStub = new Mock<ITimelineColorService>();
            var timeServiceStub = new Mock<ITimeService>();

            var entryService = new EntryService(
                entryRepository.Object,
                locationRepository.Object,
                tagRepository.Object,
                timeLineServiceStub.Object,
                timeServiceStub.Object);

            return entryService;
        }

        private Mock<ITimelineColorService> SetupTimelineColorServiceStub()
        {
            var timeLineServiceStub = new Mock<ITimelineColorService>();

            timeLineServiceStub
                .Setup(s => s.GetRandom())
                .Returns(new TimelineColor("Color") { Id = 1 });

            return timeLineServiceStub;
        }

        private Mock<ITimeService> SetUpTimeServiceStub()
        {
            var timeServiceStub =  new Mock<ITimeService>();

            timeServiceStub.Setup(ts => ts.GetNow()).Returns(new DateTime(2011, 10, 18, 15, 30, 4));

            return timeServiceStub;
        }
    }
}
