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
        public void AssembleHomeIndexViewModelTest()
        {            
            var target = new HomeAssembler(GetEntryServiceStub());       
            
            var actual = target.AssembleHomeIndexViewModel();

            Assert.IsTrue(actual.Entries.Count() > 0);       
        }

        private EntryServiceStub GetEntryServiceStub()
        {
            return new EntryServiceStub();
        }
    }
}
