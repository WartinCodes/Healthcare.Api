using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface ICityRepository : IRepository<City>
    {
        Task<City> GetCityByIdAsync(int id);
        Task<IEnumerable<City>> GetAllCitiesAsync();
    }
}
