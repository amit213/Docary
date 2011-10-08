using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

namespace Docary.Repositories.EF.Tests.Fakes
{
    public class FakeDbSet<T> : IDbSet<T> where T : class
    {
        HashSet<T> _data;
        IQueryable _query;

        public FakeDbSet()
        {
            _data = new HashSet<T>();
            _query = _data.AsQueryable();
        }

        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public void Add(T item)
        {
            _data.Add(item);
        }

        public void Remove(T item)
        {
            _data.Remove(item);
        }

        public void Attach(T item)
        {
            _data.Add(item);
        }

        public void Detach(T item)
        {
            _data.Remove(item);
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        T IDbSet<T>.Add(T entity)
        {
            this._data.Add(entity);

            return entity;
        }

        T IDbSet<T>.Attach(T entity)
        {
            this._data.Add(entity);

            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<T> Local
        {
            get { throw new NotImplementedException(); }
        }

        T IDbSet<T>.Remove(T entity)
        {
            var copy = entity;
            this._data.Remove(entity);
            return copy;
        }
    }
}
