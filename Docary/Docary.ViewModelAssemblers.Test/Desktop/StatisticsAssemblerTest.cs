using System;
using Docary.Models;
using Docary.Services;
using Docary.ViewModelAssemblers.Desktop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Docary.ViewModelAssemblers.Test.Desktop
{
    [TestClass]
    public class StatisticsAssemblerTest
    {
        [TestMethod]
        public void Test_AssembleHomeStatisticsViewModel_Fills_In_LatestEntry()
        {
            var entryService = new Mock<IEntryService>();

            var anyEntry = new Entry() { CreatedOn = new DateTime(2010, 10, 8) };
            
            entryService
                .Setup(e => e.GetLatestEntry(It.IsAny<string>()))
                .Returns(anyEntry);

            var statisticsAssembler = new StatisticsAssembler(entryService.Object);

            var statisticsViewModel = statisticsAssembler.AssembleHomeStatisticsViewModel(It.IsAny<string>());

            Assert.IsTrue(statisticsViewModel.HasLatestEntry);
            Assert.IsNotNull(statisticsViewModel.LatestEntry);
        }

        [TestMethod]
        public void Test_AssembleHomeStatisticsViewModel_Fills_In_Null_When_LatestEntry_Is_Null()
        {
            var entryService = new Mock<IEntryService>();            

            entryService
                .Setup(e => e.GetLatestEntry(It.IsAny<string>()))
                .Returns((Entry)null);

            var statisticsAssembler = new StatisticsAssembler(entryService.Object);

            var statisticsViewModel = statisticsAssembler.AssembleHomeStatisticsViewModel(It.IsAny<string>());

            Assert.IsFalse(statisticsViewModel.HasLatestEntry);
            Assert.IsNull(statisticsViewModel.LatestEntry);
        }

        [TestMethod]
        public void Test_AssembleHomeStatisticsViewModel_Fills_In_FirstEntry()
        {
            var entryService = new Mock<IEntryService>();

            var anyEntry = new Entry() { CreatedOn = new DateTime(2010, 10, 8) };

            entryService
                .Setup(e => e.GetFirstRealEntry(It.IsAny<string>()))
                .Returns(anyEntry);

            var statisticsAssembler = new StatisticsAssembler(entryService.Object);

            var statisticsViewModel = statisticsAssembler.AssembleHomeStatisticsViewModel(It.IsAny<string>());

            Assert.IsTrue(statisticsViewModel.HasFirstEntry);
            Assert.IsNotNull(statisticsViewModel.FirstEntry);
        }

        [TestMethod]
        public void Test_AssembleHomeStatisticsViewModel_Fills_In_Null_When_FirstEntry_Is_Null()
        {
            var entryService = new Mock<IEntryService>();            

            entryService
                .Setup(e => e.GetFirstRealEntry(It.IsAny<string>()))
                .Returns((Entry)null);

            var statisticsAssembler = new StatisticsAssembler(entryService.Object);

            var statisticsViewModel = statisticsAssembler.AssembleHomeStatisticsViewModel(It.IsAny<string>());

            Assert.IsFalse(statisticsViewModel.HasFirstEntry);
            Assert.IsNull(statisticsViewModel.FirstEntry);
        }
    }
}
