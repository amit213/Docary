[assembly: WebActivator.PreApplicationStartMethod(typeof(Docary.App_Start.NinjectMVC3), "Start")]
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
    using Docary.Assemblers;

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

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            
            RegisterServices(kernel);

            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IEntryRepository>().To<EntryRepository>();
            kernel.Bind<IDocaryContext>().To<DocaryContext>();
            kernel.Bind<IEntryService>().To<EntryService>();
            kernel.Bind<IHomeAssembler>().To<HomeAssembler>();
        }
    }
}
