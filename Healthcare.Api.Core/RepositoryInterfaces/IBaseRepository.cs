using System.Linq.Expressions;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IBaseRepository<TEntity>
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<TEntity> GetByIdAsync(object id);
        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        void Delete(object id);
        Task DeleteAsync(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}
