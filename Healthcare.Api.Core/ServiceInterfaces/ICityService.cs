using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAsync();
        Task<City> GetCityByIdAsync(int id);
    }
}
