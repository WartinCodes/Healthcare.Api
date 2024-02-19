using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class HealthInsuranceService : IHealthInsuranceService
    {
        private readonly IHealthInsuranceRepository _healthInsuranceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HealthInsuranceService(IHealthInsuranceRepository healthInsuranceRepository, IUnitOfWork unitOfWork)
        {
            _healthInsuranceRepository = healthInsuranceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<HealthInsurance> Add(HealthInsurance entity)
        {
            var record = await _unitOfWork.HealthInsuranceRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public void Edit(HealthInsurance entity)
        {
            _unitOfWork.HealthInsuranceRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public IQueryable<HealthInsurance> GetAsQueryable()
        {
            return _healthInsuranceRepository.GetAsQueryable();
        }

        public Task<IEnumerable<HealthInsurance>> GetAsync()
        {
            return _healthInsuranceRepository.GetAsync();
        }

        public Task<HealthInsurance> GetHealthInsuranceByIdAsync(int id)
        {
            return _healthInsuranceRepository.GetHealthInsuranceByIdAsync(id);
        }

        public void Remove(HealthInsurance entity)
        {
            _unitOfWork.HealthInsuranceRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}
