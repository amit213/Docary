using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Docary.Repositories;
using Docary.Models;

namespace Docary.Services.Tests
{
    [TestClass]
    public class TimelineColorServiceTest
    {
        [TestMethod]
        public void Test_GetRandom_Returns_Random_Colors()
        {
            var timelineColorRepository = SetupTimelineColorRepository();
            var timelineColorService = new TimelineColorService(timelineColorRepository.Object);

            var generatedRandoms = new List<TimelineColor>();

            for (var i = 0; i < 500; i++)
                generatedRandoms.Add(timelineColorService.GetRandom());

            var distinctIds = generatedRandoms.Select(r => r.Id).Distinct();

            Assert.IsTrue(distinctIds.Count() > 1);
        }       

        private Mock<ITimelineColorRepository> SetupTimelineColorRepository()
        {
            var timelineColorRepository = new Mock<ITimelineColorRepository>();

            var colorsInRepository = new List<TimelineColor>();

            for (var i = 0; i < 1000; i++)            
                colorsInRepository.Add(new TimelineColor("Color" + i) { Id = i });           
            
            timelineColorRepository
                .Setup(r => r.Get())
                .Returns(colorsInRepository.AsQueryable<TimelineColor>());

            return timelineColorRepository;
        }
    }
}
