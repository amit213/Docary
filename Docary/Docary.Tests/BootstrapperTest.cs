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

        [ClassInitialize]
        public void Setup()
        {
            _kernel = NinjectMVC3.CreateKernel();
        }       

        [TestMethod]        
        public void Test_Mobile_IHomeAssembler_Has_An_Implementation()
        {
             _kernel.Get<Docary.ViewModelAssemblers.Mobile.IHomeAssembler>();            
        }

        [TestMethod]
        public void Test_Desktop_IHomeAssembler_Has_An_Implementation()
        {
            _kernel.Get<Docary.ViewModelAssemblers.Desktop.IHomeAssembler>();
        }

        [TestMethod]
        public void Test_Desktop_IStatisticsAssembler_Has_An_Implementation()
        {
            _kernel.Get<Docary.ViewModelAssemblers.Desktop.IStatisticsAssembler>();
        }

        [TestMethod]
        public void Test_IEntryService_Has_An_Implementation()
        {
            _kernel.Get<IEntryService>();
        }

        [TestMethod]
        public void Test_ITimeService_Has_An_Implementation()
        {
            _kernel.Get<ITimeService>();
        }

        [TestMethod]
        public void Test_ITimelineColorService_Has_An_Implementation()
        {
            _kernel.Get<ITimelineColorService>();
        }

        [TestMethod]
        public void Test_IUserSettingsService_Has_An_Implementation()
        {
            _kernel.Get<IUserSettingsService>();
        }

        [TestMethod]
        public void Test_IEntryRepository_Has_An_Implementation()
        {
            _kernel.Get<IEntryRepository>();
        }

        [TestMethod]
        public void Test_ILocationRepository_Has_An_Implementation()
        {
            _kernel.Get<ILocationRepository>();
        }

        [TestMethod]
        public void Test_ITagRepository_Has_An_Implementation()
        {
            _kernel.Get<ITagRepository>();
        }
        
        [TestMethod]
        public void Test_ITimelineColorRepository_Has_An_Implementation()
        {
            _kernel.Get<ITimelineColorRepository>();
        }

        [TestMethod]
        public void Test_IUserSettingsRepository_Has_An_Implementation()
        {
            _kernel.Get<IUserSettingsRepository>();
        }

        [TestMethod]
        public void Test_DocaryContext_Has_An_Implementation()
        {
            _kernel.Get<DocaryContext>();            
        }
    }
}
