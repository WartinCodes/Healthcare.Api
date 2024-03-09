using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CityService(ICityRepository cityRepository, IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<City> GetAsQueryable()
        {
            return _cityRepository.GetAsQueryable();
        }

        public Task<IEnumerable<City>> GetAsync()
        {
            return _cityRepository.GetAllCitiesAsync();
        }

        public Task<IEnumerable<City>> GetCitiesByStateId(int stateId)
        {
            return _cityRepository.GetCitiesByStateId(stateId);
        }

        public Task<City> GetCityByIdAsync(int id)
        {
            return _cityRepository.GetCityByIdAsync(id);
        }
    }
}
