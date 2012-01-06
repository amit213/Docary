using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Repositories;
using Moq;

namespace Docary.Services.Tests
{
    public class EntryServiceFactory
    {
        public static IEntryService SetupEntryService()
        {
            var entryRepository = new Mock<IEntryRepository>();
            var locationRepository = new Mock<ILocationRepository>();
            var tagRepository = new Mock<ITagRepository>();
            var timeLineServiceStub = new Mock<ITimelineColorService>();
            var timeServiceStub = new Mock<ITimeService>();
            var scopeStub = new Mock<IScope>();

            var entryService = new EntryService(
                entryRepository.Object,
                locationRepository.Object,
                tagRepository.Object,
                timeLineServiceStub.Object,
                timeServiceStub.Object,
                scopeStub.Object);

            return entryService;
        }

        public static IEntryService SetupEntryService(
            Mock<IEntryRepository> entryRepositoryMock,
            Mock<ILocationRepository> locationRepositoryMock,
            Mock<ITagRepository> tagRepositoryMock,
            Mock<ITimelineColorService> timelineColorService)
        {
            var scopeStub = new Mock<IScope>();
            var timeServiceStub = new Mock<ITimeService>();     

            return new EntryService(
               entryRepositoryMock.Object,
               locationRepositoryMock.Object,
               tagRepositoryMock.Object,
               timelineColorService.Object,
               timeServiceStub.Object,
               scopeStub.Object);
        }
    }
}
