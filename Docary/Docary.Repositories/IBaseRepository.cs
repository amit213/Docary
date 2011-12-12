using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docary.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        IQueryable<TEntity> Get();

        TEntity Add(TEntity entity);

        void Delete(int id);
    }
}
