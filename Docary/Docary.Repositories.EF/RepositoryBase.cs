using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docary.Repositories.EF
{
    public abstract class RepositoryBase<TEntity> where TEntity : class
    {
        private DocaryContext _context;

        public RepositoryBase(DocaryContext context)
        {
            _context = context;
        }

        protected DocaryContext Context
        {
            get
            {
                return _context;
            }
        }

        public virtual IQueryable<TEntity> Get()
        {
            return Context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            entity = Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();

            return entity;
        }

        public void Delete(int id)
        {
            var entryToDelete = (TEntity)Context.Set<TEntity>().Find(id);         

            Context.Set<TEntity>().Remove(entryToDelete);
            Context.SaveChanges();
        }
    }
}
