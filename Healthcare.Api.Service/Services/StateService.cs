using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _StateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StateService(IStateRepository StateRepository, IUnitOfWork unitOfWork)
        {
            _StateRepository = StateRepository;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<State> GetAsQueryable()
        {
            return _StateRepository.GetAsQueryable();
        }

        public Task<IEnumerable<State>> GetAsync()
        {
            return _StateRepository.GetAllStateAsync();
        }

        public Task<IEnumerable<State>> GetStatesByCountryId(int countryId)
        {
            return _StateRepository.GetStatesByCountryId(countryId);
        }
    }
}
