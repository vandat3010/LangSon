using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Namek.Data.EFramework
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void Insert(T entity);
        void InsertRange(IList<T> entities);
        void Edit(T entity, IList<string> propertyNamesNotChanged = null);
        void EditRange(IList<T> entitys, IList<string> propertyNamesNotChanged = null);
        bool Exist(Expression<Func<T, bool>> predicate);
        void Delete(T entity);
        void Delete(object key);
        void DeleteRange(IList<T> list);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        T GetObject(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate, string fieldOrderBy, bool ascending, int skip, int take);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate, string groupBy, string fieldOrderBy, bool ascending, int take);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate, string fieldOrderBy, bool ascending);
        int Count(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        T GetByKey(object id);


    }
}