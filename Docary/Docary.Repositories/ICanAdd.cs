using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docary.Repositories
{
    public interface ICanAdd<T>
    {
        T Add(T item);
    }
}
