using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace GrandeGift.Services
{
    public interface IDataService<T>
    {
        IEnumerable<T> GetAll();
        T GetSingle(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Query(Expression<Func<T, bool>> predicate);
        void Create(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Update1(T entity);
        void Saving();

    }
}
