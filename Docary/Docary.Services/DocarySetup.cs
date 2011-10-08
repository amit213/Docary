using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

using Docary.Repositories.EF;

namespace Docary.Services
{
    public class DocarySetup : IDocarySetup
    {
        public void Run()
        {
            Database.SetInitializer(new DocaryDbInitializer());
        }
    }
}
