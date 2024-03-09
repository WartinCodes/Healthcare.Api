using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface ICountryService
    {
        IQueryable<Country> GetAsQueryable();
        Task<IEnumerable<Country>> GetAsync();
        Task<Country> GetCountryByIdAsync(int id);
    }
}
