﻿[assembly: WebActivator.PreApplicationStartMethod(typeof(Docary.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Docary.App_Start.NinjectMVC3), "Stop")]

namespace Docary.App_Start
{
    using System.Reflection;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Mvc;
    using Docary.Services;
    using Docary.Repositories;
    using Docary.Repositories.EF;
    using Docary.ViewModelAssemblers;
    using Docary.ViewModelAssemblers.Mobile;

    public static class NinjectMVC3
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {           
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
            DynamicModuleUtility.RegisterModule(typeof(HttpApplicationInitializationModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }        

        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            
            Register(kernel);

            return kernel;
        }        

        private static void Register(IKernel kernel)
        {
            RegisterServices(kernel);
            RegisterAssemblers(kernel);
            RegisterRepositories(kernel);            
        }

        private static void RegisterAssemblers(IKernel kernel)
        {
            kernel.Bind<Docary.ViewModelAssemblers.Mobile.IHomeAssembler>().To<Docary.ViewModelAssemblers.Mobile.HomeAssembler>();
            kernel.Bind<Docary.ViewModelAssemblers.Desktop.IHomeAssembler>().To<Docary.ViewModelAssemblers.Desktop.HomeAssembler>();
            kernel.Bind<Docary.ViewModelAssemblers.Desktop.IStatisticsAssembler>().To<Docary.ViewModelAssemblers.Desktop.StatisticsAssembler>();
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ISessionStore>().To<HttpSessionStore>();
            kernel.Bind<IEntryService>().To<EntryService>();
            kernel.Bind<ITimeService>().To<TimeService>();
            kernel.Bind<ITimelineColorService>().To<TimelineColorService>();
            kernel.Bind<IUserSettingsService>().To<UserSettingsService>();
        }

        private static void RegisterRepositories(IKernel kernel) 
        {
            kernel.Bind<IEntryRepository>().To<EntryRepository>();
            kernel.Bind<ILocationRepository>().To<LocationRepository>();
            kernel.Bind<ITagRepository>().To<TagRepository>();
            kernel.Bind<ITimelineColorRepository>().To<TimelineColorRepository>();
            kernel.Bind<IUserSettingsRepository>().To<UserSettingsRepository>();
            
            kernel.Bind<DocaryContext>().ToSelf().InRequestScope();

            kernel.Bind<IScope>().To<Scope>();
        }
    }
}
