using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CountryService(ICountryRepository countryRepository, IUnitOfWork unitOfWork)
        {
            _countryRepository = countryRepository;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Country> GetAsQueryable()
        {
            return _countryRepository.GetAsQueryable();
        }

        public Task<IEnumerable<Country>> GetAsync()
        {
            return _countryRepository.GetAsync();
        }

        public Task<Country> GetCountryByIdAsync(int id)
        {
            return _countryRepository.GetCountryByIdAsync(id);
        }
    }
}
