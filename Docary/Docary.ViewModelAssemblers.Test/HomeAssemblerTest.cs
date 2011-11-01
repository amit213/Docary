using Docary.ViewModelAssemblers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

using Docary.Services;
using Docary.ViewModels;
using Docary.ViewModelAssemblers.Test.Stubs;

namespace Docary.ViewModelAssemblers.Test
{
    [TestClass()]
    public class HomeAssemblerTest
    {
        [TestMethod()]
        public void Test_AssembleHomeIndexViewModel_Initializes_EntryGroups()
        {
            var target = new HomeAssembler(GetEntryServiceStub());

            var actual = target.AssembleHomeIndexViewModel("1");

            Assert.IsNotNull(actual.EntryGroups);
        }

        [TestMethod()]
        public void Test_AssembleHomeIndexViewModel_Groups_Entries_Correctly()
        {            
            var target = new HomeAssembler(GetEntryServiceStub());       
            
            var actual = target.AssembleHomeIndexViewModel("1");

            var firstEntryGroup = actual.EntryGroups.First();
            var secondEntryGroup = actual.EntryGroups.ElementAt(1);

            Assert.AreEqual(firstEntryGroup.Entries.Count(), 2);
            Assert.AreEqual(secondEntryGroup.Entries.Count(), 1);
        }

        private EntryServiceStub GetEntryServiceStub()
        {
            return new EntryServiceStub();
        }       
    }
}
