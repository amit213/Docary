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
        public void Test_ExtractEntry_Extracts_Tag()
        {
            var extractedEntry = GetAddEntryViewModel().ExtractEntry();

            Assert.AreEqual("TagTest", extractedEntry.Tag.Name);
        }

        [TestMethod()]
        public void Test_ExtractEntry_Extracts_Meta()
        {
            var extractedEntry = GetAddEntryViewModel().ExtractEntry();

            Assert.AreEqual("MetaTest", extractedEntry.Description);
        }

        private AddEntryViewModel GetAddEntryViewModel()
        {
            return new AddEntryViewModel()
            {
                TagName = "TagTest",
                LocationName = "LocationTest",
                Description = "MetaTest"
            };
        }
    }
}
