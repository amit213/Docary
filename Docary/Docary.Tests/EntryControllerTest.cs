using Docary.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.Mvc;

using Docary.Services;
using Docary.ViewModels;
using Docary.Tests.Stubs;
using Docary.Models;
using Docary.Areas.Mobile.Controllers;

namespace Docary.Tests
{    
    [TestClass()]
    public class EntryControllerTest
    {                
        //[TestMethod()]        
        public void Test_Add_Redirects_To_Home_Index_Route_When_Entry_SuccessFully_Added()
        {
            var addActionResult = (RedirectToRouteResult)GetEntryControllerWithEmptyEntryServiceStub().Add(GetEmptyAddEntryViewModel());

            var actualRouteValues = addActionResult.RouteValues;
            
            Assert.AreEqual("Home", actualRouteValues["controller"]);
            Assert.AreEqual("Index", actualRouteValues["action"]);
        }

        //[TestMethod()]
        public void Test_Add_Redirects_To_Route_When_Entry_SuccessFully_Added()
        {
            var addActionResult = GetEntryControllerWithEmptyEntryServiceStub().Add(GetEmptyAddEntryViewModel());

            var expectedActionResultType = typeof(RedirectToRouteResult);
            var actualActionResultType = addActionResult.GetType();

            Assert.AreEqual(expectedActionResultType, actualActionResultType);
        }

        //[TestMethod]
        public void Test_Add_Does_Not_Redirect_To_Route_When_ModelState_Invalid()
        {
            var entryController = GetEntryControllerWithEmptyEntryServiceStub();
               
            entryController.ModelState.AddModelError("SomeError", "SomeEx");
                
            var addActionResult = entryController.Add(GetEmptyAddEntryViewModel());            

            var redirectToRouteResultType = typeof(RedirectToRouteResult);
            var actualActionResultType = addActionResult.GetType();

            Assert.AreNotEqual(redirectToRouteResultType, actualActionResultType);
        }

        private static EntryController GetEntryControllerWithEmptyEntryServiceStub()
        {
            return new EntryController(new EntryServiceEmptyStub());
        }

        private static AddEntryViewModel GetEmptyAddEntryViewModel()
        {
            return new AddEntryViewModel();                        
        }
    }
}
