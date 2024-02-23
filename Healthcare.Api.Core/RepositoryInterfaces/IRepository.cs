namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAsQueryable();
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> AddAsync(TEntity entity);
        void Remove(TEntity entity);
        void Edit(TEntity entity);
    }
}