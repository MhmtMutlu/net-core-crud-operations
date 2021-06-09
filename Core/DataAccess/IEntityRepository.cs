using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    // T has been restricted to be implemented from a class which is implemented from IEntity
    public interface IEntityRepository<T> where T:class, IEntity, new()
    {
        // Defining CRUD operations
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
