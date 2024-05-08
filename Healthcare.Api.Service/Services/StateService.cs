using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StateService(IStateRepository stateRepository, IUnitOfWork unitOfWork)
        {
            _stateRepository = stateRepository;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<State> GetAsQueryable()
        {
            return _stateRepository.GetAsQueryable();
        }

        public Task<IEnumerable<State>> GetAsync()
        {
            return _stateRepository.GetAllStateAsync();
        }

        public Task<IEnumerable<State>> GetStatesByCountryId(int countryId)
        {
            return _stateRepository.GetStatesByCountryId(countryId);
        }
    }
}
