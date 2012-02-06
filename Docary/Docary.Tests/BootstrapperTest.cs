using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Docary.App_Start;
using Ninject;
using Docary.ViewModelAssemblers.Desktop;
using Docary.Services;
using Docary.Repositories;
using Docary.Repositories.EF;
using System.Web;

namespace Docary.Tests
{
    [TestClass]
    public class BootstrapperTest
    {
        private IKernel _kernel;

        [TestInitialize]
        public void Setup()
        {         
            _kernel = NinjectMVC3.CreateKernel();
        }

        [TestMethod]
        public void Test_ISessionStore_Can_Be_Resolved()
        {
            AssertDoesNotThrowWhenResolved<ISessionStore>();
        }

        [TestMethod]        
        public void Test_Mobile_IHomeAssembler_Can_Be_Resolved()
        {
            AssertDoesNotThrowWhenResolved<Docary.ViewModelAssemblers.Mobile.IHomeAssembler>();
        }

        [TestMethod]
        public void Test_Mobile_IHomeAssembler_Is_New_Instance()
        {
            AssertNewInstanceIsResolved<Docary.ViewModelAssemblers.Mobile.IHomeAssembler>();
        }

        [TestMethod]
        public void Test_Desktop_IHomeAssembler_Can_Be_Resolved()
        {
            AssertDoesNotThrowWhenResolved<Docary.ViewModelAssemblers.Desktop.IHomeAssembler>();
        }

        [TestMethod]
        public void Test_Desktop_IHomeAssembler_Is_New_Instance()
        {
            AssertNewInstanceIsResolved<Docary.ViewModelAssemblers.Desktop.IHomeAssembler>();
        }

        [TestMethod]
        public void Test_Desktop_IStatisticsAssembler_Can_Be_Resolved()
        {
            AssertDoesNotThrowWhenResolved<Docary.ViewModelAssemblers.Desktop.IStatisticsAssembler>();
        }

        [TestMethod]
        public void Test_Desktop_IStatisticsAssembler_Is_New_Instance()
        {
            AssertNewInstanceIsResolved<Docary.ViewModelAssemblers.Desktop.IStatisticsAssembler>();
        }

        [TestMethod]
        public void Test_IEntryService_Can_Be_Resolved()
        {
            AssertDoesNotThrowWhenResolved<IEntryService>();
        }

        [TestMethod]
        public void Test_IEntryService_Is_New_Instance()
        {
            AssertNewInstanceIsResolved<IEntryService>();
        }

        [TestMethod]
        public void Test_ITimeService_Can_Be_Resolved()
        {
            AssertDoesNotThrowWhenResolved<ITimeService>();
        }

        [TestMethod]
        public void Test_ITimeService_Is_New_Instance()
        {
            AssertNewInstanceIsResolved<ITimeService>();
        }

        [TestMethod]
        public void Test_ISessionStore_Is_New_Instance()
        {
            AssertNewInstanceIsResolved<ISessionStore>();
        }

        [TestMethod]
        public void Test_ITimelineColorService_Can_Be_Resolved()
        {
            AssertDoesNotThrowWhenResolved<ITimelineColorService>();
        }

        [TestMethod]
        public void Test_ITimelineColorService_Is_New_Instance()
        {
            AssertNewInstanceIsResolved<ITimelineColorService>();
        }

        [TestMethod]
        public void Test_IUserSettingsService_Can_Be_Resolved()
        {
            AssertDoesNotThrowWhenResolved<IUserSettingsService>();
        }

        [TestMethod]
        public void Test_IUserSettingsService_Is_New_Instance()
        {
            AssertNewInstanceIsResolved<IUserSettingsService>();
        }

        [TestMethod]
        public void Test_IEntryRepository_Can_Be_Resolved()
        {
            AssertDoesNotThrowWhenResolved<IEntryRepository>();
        }

        [TestMethod]
        public void Test_IEntryRepository_Is_New_Instance()
        {
            AssertNewInstanceIsResolved<IEntryRepository>();
        }

        [TestMethod]
        public void Test_ILocationRepository_Can_Be_Resolved()
        {
            AssertDoesNotThrowWhenResolved<ILocationRepository>();
        }

        [TestMethod]
        public void Test_ILocationRepository_Is_New_Instance()
        {
            AssertNewInstanceIsResolved<ILocationRepository>();
        }

        [TestMethod]
        public void Test_ITagRepository_Can_Be_Resolved()
        {
            AssertDoesNotThrowWhenResolved<ITagRepository>();
        }

        [TestMethod]
        public void Test_ITagRepository_Is_New_Instance()
        {
            AssertNewInstanceIsResolved<ITagRepository>();
        }
        
        [TestMethod]
        public void Test_ITimelineColorRepository_Can_Be_Resolved()
        {
            AssertDoesNotThrowWhenResolved<ITimelineColorRepository>();
        }

        [TestMethod]
        public void Test_ITimelineColorRepository_Is_New_Instance()
        {
            AssertNewInstanceIsResolved<ITimelineColorRepository>();
        }

        [TestMethod]
        public void Test_IUserSettingsRepository_Can_Be_Resolved()
        {
           AssertDoesNotThrowWhenResolved<IUserSettingsRepository>();
        }

        [TestMethod]
        public void Test_IUserSettingsRepository_Is_New_Instance()
        {
            AssertNewInstanceIsResolved<IUserSettingsRepository>();
        }

        [TestMethod]
        public void Test_DocaryContext_Can_Be_Resolved()
        {            
            AssertDoesNotThrowWhenResolved<DocaryContext>();            
        }        

        private void AssertDoesNotThrowWhenResolved<T>() 
        {
            Xunit.Assert.DoesNotThrow(() => _kernel.Get<T>());
        }

        private void AssertNewInstanceIsResolved<T>()
        {
            var instance = _kernel.Get<T>();
            var secondInstance = _kernel.Get<T>();

            Assert.AreNotSame(instance, secondInstance);
        }       
    }
}
