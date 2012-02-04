using System;
using System.Collections.Generic;
using System.Linq;
using Docary.Models;
using Docary.Services;
using Docary.ViewModelAssemblers.Desktop;
using Docary.ViewModels.Desktop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Docary.ViewModelAssemblers.Test.Desktop
{
    [TestClass]
    public class HomeAssemblerAssembleHomeIndexViewModelTests
    {
        private const string TIMEZONE = "W. Europe Standard Time";      

        [TestMethod()]
        public void Test_AssembleHomeIndexViewModel_Defaults_Dates()
        {
            var actual = HomeAssemblerFactory.SetupHomeAssembler().AssembleHomeIndexViewModel("1");

            Assert.IsNotNull(actual.To);
            Assert.IsNotNull(actual.From);
        }
   
        [TestMethod()]
        public void Test_Groups_Entries_Correctly()
        {           
            var createdOnMin = new DateTime(2011, 10, 18, 0, 0, 0, DateTimeKind.Local);
            var createdOnMax = new DateTime(2011, 10, 21, 0, 0, 0, DateTimeKind.Local);

            var homeIndexViewModel = new HomeIndexViewModel(createdOnMin, createdOnMax);

            var actual = HomeAssemblerFactory.SetupHomeAssembler().AssembleHomeIndexViewModel(homeIndexViewModel, It.IsAny<string>());

            var firstEntryGroup = actual.EntryGroups.First();
            var secondEntryGroup = actual.EntryGroups.ElementAt(1);
            var thirdEntryGroup = actual.EntryGroups.ElementAt(2);

            Assert.AreEqual(1, firstEntryGroup.Entries.Count());
            Assert.AreEqual(3, secondEntryGroup.Entries.Count());
            Assert.AreEqual(1, thirdEntryGroup.Entries.Count());
        }
      
        [TestMethod()]
        public void Test_Calculates_EntryPercentages_Correctly()
        {
            var createdOnMin = new DateTime(2011, 10, 18, 0, 0, 0, DateTimeKind.Local);
            var createdOnMax = new DateTime(2011, 10, 21, 0, 0, 0, DateTimeKind.Local);

            var homeIndexViewModel = new HomeIndexViewModel(createdOnMin, createdOnMax);

            var actual = HomeAssemblerFactory.SetupHomeAssembler().AssembleHomeIndexViewModel(homeIndexViewModel, It.IsAny<string>());

            var firstEntryGroup = actual.EntryGroups.First();
            var secondEntryGroup = actual.EntryGroups.ElementAt(1);
            var thirdEntryGroup = actual.EntryGroups.ElementAt(2);

            Assert.AreEqual(85, Convert.ToInt32(firstEntryGroup.Entries.First().Percentage));           
            Assert.AreEqual(15, Convert.ToInt32(secondEntryGroup.Entries.First().Percentage));
            Assert.AreEqual(50, Convert.ToInt32(secondEntryGroup.Entries.ElementAt(1).Percentage)); 
            Assert.AreEqual(35, Convert.ToInt32(secondEntryGroup.Entries.ElementAt(2).Percentage));
            Assert.AreEqual(100, Convert.ToInt32(thirdEntryGroup.Entries.First().Percentage));
        }

        [TestMethod()]
        public void Test_Populates_The_Tag_Property()
        {
            var createdOnMin = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(TIMEZONE));
            var createdOnMax = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddDays(3), TimeZoneInfo.FindSystemTimeZoneById(TIMEZONE));

            var homeIndexViewModel = new HomeIndexViewModel(createdOnMin, createdOnMax);

            var actual = HomeAssemblerFactory.SetupHomeAssembler().AssembleHomeIndexViewModel(homeIndexViewModel, It.IsAny<string>());

            foreach (var entryGroup in actual.EntryGroups)
            {
                foreach (var entry in entryGroup.Entries)               
                    Assert.IsFalse(string.IsNullOrEmpty(entry.Tag));                                    
            }
        }       

        [TestMethod()]
        public void Test_Populates_The_Legenda()
        {
            var createdOnMin = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(TIMEZONE));
            var createdOnMax = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddDays(3), TimeZoneInfo.FindSystemTimeZoneById(TIMEZONE));

            var homeIndexViewModel = new HomeIndexViewModel(createdOnMin, createdOnMax);

            var actual = HomeAssemblerFactory.SetupHomeAssembler().AssembleHomeIndexViewModel(homeIndexViewModel, It.IsAny<string>());

            Assert.IsNotNull(actual.Legenda);           
        }

        [TestMethod()]
        public void Test_Populates_The_Legenda_With_Three_Tags()
        {
            var createdOnMin = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(TIMEZONE));
            var createdOnMax = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddDays(3), TimeZoneInfo.FindSystemTimeZoneById(TIMEZONE));

            var homeIndexViewModel = new HomeIndexViewModel(createdOnMin, createdOnMax);

            var actual = HomeAssemblerFactory.SetupHomeAssembler().AssembleHomeIndexViewModel(homeIndexViewModel, It.IsAny<string>());

            Assert.IsTrue(actual.Legenda.Count == 3);
        }      
    }
}
