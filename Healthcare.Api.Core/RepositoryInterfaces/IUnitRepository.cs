using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IUnitRepository : IRepository<Unit>
    {
        Task<Unit> GetUnitByIdAsync(int id);
        Task<Unit?> GetUnitByNameOrShortNameAsync(string name, string shortName);
    }
}
