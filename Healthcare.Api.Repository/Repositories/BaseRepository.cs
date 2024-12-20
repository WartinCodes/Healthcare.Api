using Healthcare.Api.Core.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Healthcare.Api.Repository.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        internal DbContext Context;

        internal DbSet<TEntity> DbSet;

        protected BaseRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> source = FilterAndOrder(DbSet, filter, orderBy, includeProperties);
            return source.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = FilterAndOrder(DbSet, filter, orderBy, includeProperties);
            return await query.ToListAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = DbSet.Add(entity);
            return entityEntry.Entity;
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities).ConfigureAwait(continueOnCapturedContext: false);
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            return (await DbSet.AddAsync(entity).ConfigureAwait(continueOnCapturedContext: false)).Entity;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual async Task DeleteAsync(object id)
        {
            Delete(await DbSet.FindAsync(id));
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }

            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        private IQueryable<TEntity> FilterAndOrder(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            if (filter != null)
            {
                query = query.Where(filter);
            }

            string[] array = includeProperties.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string navigationPropertyPath in array)
            {
                query = query.Include(navigationPropertyPath);
            }

            orderBy?.Invoke(query);
            return query;
        }
    }
}
