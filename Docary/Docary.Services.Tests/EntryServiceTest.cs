using Docary.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Docary.Repositories;
using Docary.Models;
using Docary.Repositories.EF;
using Moq;
using Docary.Services.Tests.Builder;

namespace Docary.Services.Tests
{      
    [TestClass()]
    public class EntryServiceArgumentTest
    {
        private IEntryService _entryService;

        [TestInitialize()]
        public void Initialize()
        {
            _entryService = EntryServiceFactory.SetupEntryService();
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_AddEntry_Throws_ArgumentNullException_On_Null_Entry()
        {           
            _entryService.AddEntry(null);            
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
              
            _entryService.AddEntry(newEntryWithoutUserId);
        }
    }

    [TestClass()]
    public class EntryServiceAddEntryTest
    {
        private Mock<IEntryRepository> _entryRepository;
        private Mock<ILocationRepository> _locationRepository;
        private Mock<ITagRepository> _tagRepository;
        private Mock<ITimelineColorService> _timeLineColorService;

        [TestInitialize()]
        public void Initialize()
        {
            _entryRepository = new Mock<IEntryRepository>();
            _locationRepository = new Mock<ILocationRepository>();
            _tagRepository = new Mock<ITagRepository>();
            _timeLineColorService = new Mock<ITimelineColorService>();  
        }

        [TestMethod]
        public void Test_Adds_Unresolvable_Location() 
        {
            _timeLineColorService
                .Setup(t => t.GetRandom())
                .Returns(new TimelineColor("#FFF"));
            _entryRepository
                .Setup(e => e.IsEmpty(It.IsAny<string>()))
                .Returns(false);
            _locationRepository
                .Setup(l => l.Find(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((Location)null);                                                   
            
            EntryServiceFactory
                .SetupEntryService(_entryRepository, _locationRepository, _tagRepository, _timeLineColorService)
                .AddEntry(EntryBuilder.Build("Unresolvable_Location_Name", "Test_Tag_Name", "1"));            
            
            _locationRepository.Verify(r => r.Add(It.IsAny<Location>()), Times.Once());           
        }

        [TestMethod]
        public void Test_Does_Not_Add_Existing_Location()
        {
            _timeLineColorService
                .Setup(t => t.GetRandom())
                .Returns(new TimelineColor("#FFF"));
            _entryRepository
                .Setup(e => e.IsEmpty(It.IsAny<string>()))
                .Returns(false);
            _locationRepository
                .Setup(l => l.Find(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Location());                      

            EntryServiceFactory
                .SetupEntryService(_entryRepository, _locationRepository, _tagRepository, _timeLineColorService)
                .AddEntry(EntryBuilder.Build("Unresolvable_Location_Name", "Test_Tag_Name", "1"));

            _locationRepository.Verify(l => l.Add(It.IsAny<Location>()), Times.Never());
        }

        [TestMethod]
        public void Test_Does_Not_Add_Existing_Tag()
        {            
            _entryRepository
                .Setup(e => e.IsEmpty(It.IsAny<string>()))
                .Returns(false);
            _tagRepository
                .Setup(l => l.Find(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new EntryTag());           

            EntryServiceFactory
                .SetupEntryService(_entryRepository, _locationRepository, _tagRepository, _timeLineColorService)
                .AddEntry(EntryBuilder.Build("TestLocation", "TestTag", "1"));

            _tagRepository.Verify(e => e.Add(It.IsAny<EntryTag>()), Times.Never());
        }

        [TestMethod]
        public void Test_Adds_Unresolvable_EntryTag()
        {          
            _entryRepository
                .Setup(e => e.IsEmpty(It.IsAny<string>()))
                .Returns(false);
            _tagRepository
                .Setup(l => l.Find(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((EntryTag)null);
            _timeLineColorService
                .Setup(t => t.GetRandom())
                .Returns(new TimelineColor("#FFF"));                     

            EntryServiceFactory
                .SetupEntryService(_entryRepository, _locationRepository, _tagRepository, _timeLineColorService)
                .AddEntry(EntryBuilder.Build("Test_Location_Name", "Unresolvable_Tag_Name", "1"));

            _tagRepository.Verify(e => e.Add(It.IsAny<EntryTag>()), Times.Once());
        }            

        [TestMethod]
        public void Test_Adds_Entry()
        {
            _timeLineColorService
                .Setup(t => t.GetRandom())
                .Returns(new TimelineColor("#FFF"));
            _entryRepository
                .Setup(e => e.IsEmpty(It.IsAny<string>()))
                .Returns(false);

            EntryServiceFactory
                .SetupEntryService(_entryRepository, _locationRepository, _tagRepository, _timeLineColorService)
                .AddEntry(EntryBuilder.Build("TestLocation", "TestTag", "1"));    

            _entryRepository.Verify(e => e.Add(It.IsAny<Entry>()), Times.Once());
        }

        [TestMethod]
        public void Test_Add_Adds_And_Also_Adds_OffTheGridEntry_When_Empty_EntryRepository()
        {
            _timeLineColorService
                .Setup(t => t.GetRandom())
                .Returns(new TimelineColor("#FFF"));
            _entryRepository
                .Setup(e => e.IsEmpty(It.IsAny<string>()))
                .Returns(true);                     

            EntryServiceFactory
                .SetupEntryService(_entryRepository, _locationRepository, _tagRepository, _timeLineColorService)
                .AddEntry(EntryBuilder.Build("TestLocation", "TestTag", "1"));            

            _entryRepository.Verify(e => e.Add(It.IsAny<Entry>()), Times.Exactly(2));
        }
    }

    [TestClass()]
    public class EntryServiceGetNumberOfEntriesTest {
        private Mock<IEntryRepository> _entryRepository;
        private Mock<ILocationRepository> _locationRepository;
        private Mock<ITagRepository> _tagRepository;
        private Mock<ITimelineColorService> _timeLineColorService;

        [TestInitialize()]
        public void Initialize()
        {
            _entryRepository = new Mock<IEntryRepository>();
            _locationRepository = new Mock<ILocationRepository>();
            _tagRepository = new Mock<ITagRepository>();
            _timeLineColorService = new Mock<ITimelineColorService>();
        }

        [TestMethod]
        public void Test_Returns_The_Number_Of_Entries()
        {                  
            _entryRepository
                .Setup(e => e.Count(It.IsAny<string>()))
                .Returns(5);

           var entryService = EntryServiceFactory.SetupEntryService(
               _entryRepository, _locationRepository, _tagRepository, _timeLineColorService);

            Assert.AreEqual(5, entryService.GetNumberOfEntries("1"));
        }              
    }
}
