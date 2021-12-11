using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Namek.Data.EFramework
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dataContext;
        private IDbSet<T> _entityDbSet;

        public Repository(DbContext dataContext)
        {
            _dataContext = dataContext ?? throw new Exception("EFRepository::initialize::dbContext::Canot null");
            _entityDbSet = this._dataContext.Set<T>();
        }
        public DbContext DbContext => _dataContext;
        protected virtual IDbSet<T> Entities => _entityDbSet ?? (_entityDbSet = _dataContext.Set<T>());
        public IQueryable<T> Table => Entities;

        public void Insert(T entity)
        {
            _dataContext.Set<T>().Add(entity);
        }
        
        public void InsertRange(IList<T> entitys)
        {
            _dataContext.Set<T>().AddRange(entitys);
        }

        public void Edit(T entity, IList<string> propertyNamesNotChanged = null)
        {
            _dataContext.Set<T>().Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;

            if (propertyNamesNotChanged != null)
            {
                var propertyNames = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                var propertyNotChangedNames = from x in propertyNames
                                              where propertyNamesNotChanged.Contains(x.Name)
                                              select x.Name;
                foreach (var propertyName in propertyNotChangedNames)
                {
                    try
                    {
                        _dataContext.Entry(entity).Property(propertyName).IsModified = false;
                    }
                    catch
                    {
                        // Accept case property Ignore
                    }
                }
            }
        }

        public void EditRange(IList<T> entitys, IList<string> propertyNamesNotChanged = null)
        {
            foreach (T entity in entitys)
            {
                _dataContext.Entry(entity).State = EntityState.Modified;

                if (propertyNamesNotChanged != null)
                {
                    var propertyNames = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    var propertyNotChangedNames = from x in propertyNames
                                                  where propertyNamesNotChanged.Contains(x.Name)
                                                  select x.Name;
                    foreach (var propertyName in propertyNotChangedNames)
                    {

                        try
                        {
                            _dataContext.Entry(entity).Property(propertyName).IsModified = false;
                        }
                        catch
                        {
                            // Accept case property Ignore
                        }
                    }
                }
            }
        }

        public bool Exist(Expression<Func<T, bool>> predicate)
        {
            return _dataContext.Set<T>().Any(predicate);
        }

        public void Delete(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
        }

        public void Delete(object key)
        {
            T entity = GetByKey(key);
            if (entity != null)
            {
                _dataContext.Set<T>().Remove(entity);
            }
        }

        public void DeleteRange(IList<T> list)
        {
            _dataContext.Set<T>().RemoveRange(list);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dataContext.Set<T>().Where(predicate).ToList();
        }

        public T GetObject(Expression<Func<T, bool>> predicate)
        {
            return _dataContext.Set<T>().Where(predicate).FirstOrDefault();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate, string fieldOrderBy, bool ascending, int skip, int take)
        {
            var p = typeof(T).GetProperty(fieldOrderBy);
            var t = p.PropertyType;
            var pe = Expression.Parameter(typeof(T), "p");
            if (t == typeof(int))
            {
                var exp = Expression.Lambda<Func<T, int>>(Expression.Property(pe, fieldOrderBy), pe);
                return (ascending ? _dataContext.Set<T>().Where(predicate).OrderBy(exp).Skip(skip).Take(take).ToList() : _dataContext.Set<T>().Where(predicate).OrderByDescending(exp).Skip(skip).Take(take).ToList());
            }
            else
            if (t == typeof(int?))
            {
                var exp = Expression.Lambda<Func<T, int?>>(Expression.Property(pe, fieldOrderBy), pe);
                return (ascending ? _dataContext.Set<T>().Where(predicate).OrderBy(exp).Skip(skip).Take(take).ToList() : _dataContext.Set<T>().Where(predicate).OrderByDescending(exp).Skip(skip).Take(take).ToList());
            }
            else
            if (t == typeof(short))
            {
                var exp = Expression.Lambda<Func<T, short>>(Expression.Property(pe, fieldOrderBy), pe);
                return (ascending ? _dataContext.Set<T>().Where(predicate).OrderBy(exp).Skip(skip).Take(take).ToList() : _dataContext.Set<T>().Where(predicate).OrderByDescending(exp).Skip(skip).Take(take).ToList());
            }
            else
            if (t == typeof(short?))
            {
                var exp = Expression.Lambda<Func<T, short?>>(Expression.Property(pe, fieldOrderBy), pe);
                return (ascending ? _dataContext.Set<T>().Where(predicate).OrderBy(exp).Skip(skip).Take(take).ToList() : _dataContext.Set<T>().Where(predicate).OrderByDescending(exp).Skip(skip).Take(take).ToList());
            }
            else
            if (t == typeof(DateTime))
            {
                var exp = Expression.Lambda<Func<T, DateTime>>(Expression.Property(pe, fieldOrderBy), pe);
                return (ascending ? _dataContext.Set<T>().Where(predicate).OrderBy(exp).Skip(skip).Take(take).ToList() : _dataContext.Set<T>().Where(predicate).OrderByDescending(exp).Skip(skip).Take(take).ToList());
            }
            else
            if (t == typeof(DateTime?))
            {
                var exp = Expression.Lambda<Func<T, DateTime?>>(Expression.Property(pe, fieldOrderBy), pe);
                return (ascending ? _dataContext.Set<T>().Where(predicate).OrderBy(exp).Skip(skip).Take(take).ToList() : _dataContext.Set<T>().Where(predicate).OrderByDescending(exp).Skip(skip).Take(take).ToList());
            }
            else
            {
                var exp = Expression.Lambda<Func<T, string>>(Expression.Property(pe, fieldOrderBy), pe);
                return (ascending ? _dataContext.Set<T>().Where(predicate).OrderBy(exp).Skip(skip).Take(take).ToList() : _dataContext.Set<T>().Where(predicate).OrderByDescending(exp).Skip(skip).Take(take).ToList());
            }

        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate, string groupBy, string fieldOrderBy, bool ascending, int take)
        {
            var p = typeof(T).GetProperty(fieldOrderBy);
            var t = p.PropertyType;
            if (t == typeof(int))
            {
                var pe = Expression.Parameter(typeof(T), "p");
                var expr1 = Expression.Lambda<Func<T, int>>(Expression.Property(pe, fieldOrderBy), pe);

                var fieldXExpression = Expression.Property(pe, groupBy);
                var lambda = Expression.Lambda<Func<T, object>>(fieldXExpression, pe);

                if (ascending)
                {
                    var data = _dataContext.Set<T>().Where(predicate).OrderBy(expr1).GroupBy(lambda).Select(x => x.ToList().Take(take)).ToList();
                    var list = new List<T>();
                    foreach (var item in data)
                    {
                        list.AddRange(item);
                    }

                    return list;
                }
                else
                {
                    var data = _dataContext.Set<T>().Where(predicate).OrderByDescending(expr1).GroupBy(lambda).Select(x => x.ToList().Take(take)).ToList();
                    var list = new List<T>();
                    foreach (var item in data)
                    {
                        list.AddRange(item);
                    }

                    return list;
                }
            }
            else
            {
                var pe = Expression.Parameter(typeof(T), "p");
                var expr1 = Expression.Lambda<Func<T, string>>(Expression.Property(pe, fieldOrderBy), pe);
                var fieldXExpression = Expression.Property(pe, groupBy);
                var lambda = Expression.Lambda<Func<T, object>>(
                   fieldXExpression,
                    pe);
                if (ascending)
                {
                    var data = _dataContext.Set<T>().Where(predicate).OrderBy(expr1).GroupBy(lambda).Select(x => x.ToList().Take(take)).ToList();
                    var list = new List<T>();
                    foreach (var item in data)
                    {
                        list.AddRange(item);
                    }

                    return list;
                }
                else
                {
                    var data = _dataContext.Set<T>().Where(predicate).OrderByDescending(expr1).GroupBy(lambda).Select(x => x.ToList().Take(take)).ToList();
                    var list = new List<T>();
                    foreach (var item in data)
                    {
                        list.AddRange(item);
                    }

                    return list;
                }
            }
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate, string fieldOrderBy, bool ascending)
        {
            var p = typeof(T).GetProperty(fieldOrderBy);
            var t = p.PropertyType;
            var pe = Expression.Parameter(typeof(T), "p");
            if (t == typeof(int))
            {
                var exp = Expression.Lambda<Func<T, int>>(Expression.Property(pe, fieldOrderBy), pe);
                return (ascending
                        ? _dataContext.Set<T>().Where(predicate).OrderBy(exp).ToList()
                        : _dataContext.Set<T>().Where(predicate).OrderByDescending(exp).ToList());
            }
            else
            if (t == typeof(int?))
            {
                var exp = Expression.Lambda<Func<T, int?>>(Expression.Property(pe, fieldOrderBy), pe);
                return (ascending
                      ? _dataContext.Set<T>().Where(predicate).OrderBy(exp).ToList()
                      : _dataContext.Set<T>().Where(predicate).OrderByDescending(exp).ToList());
            }
            else
            if (t == typeof(short))
            {
                var exp = Expression.Lambda<Func<T, short>>(Expression.Property(pe, fieldOrderBy), pe);
                return (ascending
                      ? _dataContext.Set<T>().Where(predicate).OrderBy(exp).ToList()
                      : _dataContext.Set<T>().Where(predicate).OrderByDescending(exp).ToList());
            }
            else
            if (t == typeof(short?))
            {
                var exp = Expression.Lambda<Func<T, short?>>(Expression.Property(pe, fieldOrderBy), pe);
                return (ascending
                      ? _dataContext.Set<T>().Where(predicate).OrderBy(exp).ToList()
                      : _dataContext.Set<T>().Where(predicate).OrderByDescending(exp).ToList());
            }
            else
            if (t == typeof(DateTime))
            {
                var exp = Expression.Lambda<Func<T, DateTime>>(Expression.Property(pe, fieldOrderBy), pe);
                return (ascending
                      ? _dataContext.Set<T>().Where(predicate).OrderBy(exp).ToList()
                      : _dataContext.Set<T>().Where(predicate).OrderByDescending(exp).ToList());
            }
            else
            if (t == typeof(DateTime?))
            {
                var exp = Expression.Lambda<Func<T, DateTime?>>(Expression.Property(pe, fieldOrderBy), pe);
                return (ascending
                      ? _dataContext.Set<T>().Where(predicate).OrderBy(exp).ToList()
                      : _dataContext.Set<T>().Where(predicate).OrderByDescending(exp).ToList());
            }
            else
            {
                var exp = Expression.Lambda<Func<T, string>>(Expression.Property(pe, fieldOrderBy), pe);
                return (ascending
                    ? _dataContext.Set<T>().Where(predicate).OrderBy(exp).ToList()
                    : _dataContext.Set<T>().Where(predicate).OrderByDescending(exp).ToList());
            }
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _dataContext.Set<T>().Where(predicate).Count();
        }

        public IEnumerable<T> GetAll()
        {
            return _dataContext.Set<T>().ToList();
        }

        public T GetByKey(object key)
        {
            return _dataContext.Set<T>().Find(key);
        }

        public void Dispose()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
