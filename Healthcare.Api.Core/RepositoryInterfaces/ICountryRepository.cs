using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<Country> GetCountryByIdAsync(int id);
    }
}
