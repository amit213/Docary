[assembly: WebActivator.PreApplicationStartMethod(typeof(Docary.App_Start.DatabaseSetup), "Start")]

namespace Docary.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Docary.Services;

    public static class DatabaseSetup
    {
        public static void Start()
        {
            new DocarySetup().Run();            
        }
    }
}