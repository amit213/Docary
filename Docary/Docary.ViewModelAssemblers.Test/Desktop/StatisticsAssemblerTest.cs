using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Docary.ViewModelAssemblers.Desktop;
using Moq;
using Docary.Services;
using Docary.Models;

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
    }
}
