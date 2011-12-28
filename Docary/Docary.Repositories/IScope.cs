using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docary.Repositories
{
    public interface IScope
    {
        void Commit();
    }
}
