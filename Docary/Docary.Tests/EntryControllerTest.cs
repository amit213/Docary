using Docary.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Docary.Services;
using Docary.ViewModels;
using System.Web.Mvc;

namespace Docary.Tests
{
    ///</summary>
    [TestClass()]
    public class EntryControllerTest
    {                
        [TestMethod()]        
        public void Add_Redirects_To_Home_Index_When_EntryAdded()
        {
            var target = new EntryController(null); // TODO: Initialize to an appropriate value
            AddEntryViewModel addEntryViewModel = null; // TODO: Initialize to an appropriate value
            ActionResult expected = null; // TODO: Initialize to an appropriate value
            ActionResult actual;
            actual = target.Add(addEntryViewModel);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
