using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    // TEntity has been restricted to be implemented from a class which is implemented from IEntity
    // TContext has been restricted to be implemented from DbContext
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        // Implemented CRUD operations from IEntityRepository
        // Used IDisposible pattern with "using"
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                // Catching object which is coming as an entity
                var addedEntity = context.Entry(entity);
                // Using EntityFramework commands to add
                addedEntity.State = EntityState.Added;
                // Saving database changes
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                // Catching object which is coming as an entity
                var addedEntity = context.Entry(entity);
                // Using EntityFramework commands to delete
                addedEntity.State = EntityState.Deleted;
                // Saving database changes
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                // Return a TEntity which matches with filter
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                // Returning TEntity as a list form
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                // Catching object which is coming as an entity
                var addedEntity = context.Entry(entity);
                // Using EntityFramework commands to update
                addedEntity.State = EntityState.Modified;
                // Saving database changes
                context.SaveChanges();
            }
        }
    }
}
