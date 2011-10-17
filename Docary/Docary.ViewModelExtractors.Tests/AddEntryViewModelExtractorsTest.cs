using Docary.ViewModelExtractors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Docary.ViewModels;
using Docary.Models;

namespace Docary.ViewModelExtractors.Tests
{   
    [TestClass()]
    public class AddEntryViewModelExtractorsTest
    {     
        [TestMethod()]
        public void Test_ExtractEntry_Extracts_Location()
        {
            var extractedEntry = GetAddEntryViewModel().ExtractEntry();

            Assert.AreEqual("LocationTest", extractedEntry.Location.Name);
        }

        [TestMethod()]
        public void Test_ExtractEntry_Extracts_Activity()
        {
            var extractedEntry = GetAddEntryViewModel().ExtractEntry();

            Assert.AreEqual("ActivityTest", extractedEntry.Activity.Name);
        }

        [TestMethod()]
        public void Test_ExtractEntry_Extracts_Meta()
        {
            var extractedEntry = GetAddEntryViewModel().ExtractEntry();

            Assert.AreEqual("MetaTest", extractedEntry.Meta);
        }

        private AddEntryViewModel GetAddEntryViewModel()
        {
            return new AddEntryViewModel()
            {
                ActivityName = "ActivityTest",
                LocationName = "LocationTest",
                Meta = "MetaTest"
            };
        }
    }
}
