using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface ISupportService
    {
        IQueryable<Support> GetAsQueryable();
        Task<IEnumerable<Support>> GetAsync();
        Task<Support> Add(Support entity);
        void Remove(Support entity);
        void Edit(Support entity);
    }
}
