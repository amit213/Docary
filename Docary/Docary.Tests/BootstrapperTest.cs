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
        public void Test_Mobile_IHomeAssembler_Can_Be_Resolved()
        {
            Xunit.Assert.DoesNotThrow(
                (() => _kernel.Get<Docary.ViewModelAssemblers.Mobile.IHomeAssembler>()));
        }

        [TestMethod]
        public void Test_Desktop_IHomeAssembler_Can_Be_Resolved()
        {
            Xunit.Assert.DoesNotThrow(
                (() => _kernel.Get<Docary.ViewModelAssemblers.Desktop.IHomeAssembler>()));
        }

        [TestMethod]
        public void Test_Desktop_IStatisticsAssembler_Can_Be_Resolved()
        {
            Xunit.Assert.DoesNotThrow(
                (() => _kernel.Get<Docary.ViewModelAssemblers.Desktop.IStatisticsAssembler>()));
        }

        [TestMethod]
        public void Test_IEntryService_Can_Be_Resolved()
        {
            Xunit.Assert.DoesNotThrow(
                () => _kernel.Get<IEntryService>());
        }

        [TestMethod]
        public void Test_ITimeService_Can_Be_Resolved()
        {
            Xunit.Assert.DoesNotThrow(
                () => _kernel.Get<ITimeService>());
        }

        [TestMethod]
        public void Test_ITimelineColorService_Can_Be_Resolved()
        {
            Xunit.Assert.DoesNotThrow(
                () => _kernel.Get<ITimelineColorService>());
        }

        [TestMethod]
        public void Test_IUserSettingsService_Can_Be_Resolved()
        {
            Xunit.Assert.DoesNotThrow(
                () => _kernel.Get<IUserSettingsService>());
        }

        [TestMethod]
        public void Test_IEntryRepository_Can_Be_Resolved()
        {
            Xunit.Assert.DoesNotThrow(
                () => _kernel.Get<IEntryRepository>());
        }

        [TestMethod]
        public void Test_ILocationRepository_Can_Be_Resolved()
        {
            Xunit.Assert.DoesNotThrow(
                () => _kernel.Get<ILocationRepository>());
        }

        [TestMethod]
        public void Test_ITagRepository_Can_Be_Resolved()
        {
            Xunit.Assert.DoesNotThrow(
                () => _kernel.Get<ITagRepository>());
        }
        
        [TestMethod]
        public void Test_ITimelineColorRepository_Can_Be_Resolved()
        {
            Xunit.Assert.DoesNotThrow(
                () => _kernel.Get<ITimelineColorRepository>());
        }

        [TestMethod]
        public void Test_IUserSettingsRepository_Can_Be_Resolved()
        {
            Xunit.Assert.DoesNotThrow(
                () => _kernel.Get<IUserSettingsRepository>());
        }

        [TestMethod]
        public void Test_DocaryContext_Can_Be_Resolved()
        {            
            Xunit.Assert.DoesNotThrow(
                () => _kernel.Get<DocaryContext>());            
        }
    }
}
