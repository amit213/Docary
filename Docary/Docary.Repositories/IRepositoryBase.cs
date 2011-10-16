using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docary.Repositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> Get();
        T Add(T item);
        void Delete(int id);
    }
}
