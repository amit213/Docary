using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docary.Services.Tests.Stubs
{
    public class TimeServiceStub : ITimeService
    {
        public DateTime GetNow()
        {
            return new DateTime(2011, 10, 18, 15, 30, 4);
        }
    }
}
