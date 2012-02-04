using Docary.ViewModelExtractors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Docary.ViewModels;
using Docary.Models;

namespace Docary.ViewModelExtractors.Tests
{   
    [TestClass()]
    public class AddEntryViewModelExtractorsExtractEntryTest
    {
        private Entry _extractedEntry;

        [TestInitialize]
        public void Initialize()
        {
            var addEntryViewModel =  new AddEntryViewModel()
            {
                TagName = "TagTest",
                LocationName = "LocationTest",
                Description = "MetaTest"
            };

            _extractedEntry = addEntryViewModel.ExtractEntry();
        }

        [TestMethod()]
        public void Test_Extracts_Location()
        {
            Assert.AreEqual("LocationTest", _extractedEntry.Location.Name);
        }

        [TestMethod()]
        public void Test_Extracts_Tag()
        {          
            Assert.AreEqual("TagTest", _extractedEntry.Tag.Name);
        }

        [TestMethod()]
        public void Test_Extracts_Meta()
        {
            Assert.AreEqual("MetaTest", _extractedEntry.Description);
        }       
    }
}
