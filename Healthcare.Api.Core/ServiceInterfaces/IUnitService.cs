using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IUnitService
    {
        IQueryable<Unit> GetAsQueryable();
        Task<Unit> GetUnitByIdAsync(int id);
        Task<Unit> Add(Unit entity);
        void Remove(Unit entity);
        Task Edit(Unit entity);
    }
}
