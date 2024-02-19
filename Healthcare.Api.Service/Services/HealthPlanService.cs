using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class HealthPlanService : IHealthPlanService
    {
        private readonly IHealthPlanRepository _healthPlanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HealthPlanService(IHealthPlanRepository HealthPlanRepository, IUnitOfWork unitOfWork)
        {
            _healthPlanRepository = HealthPlanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<HealthPlan> Add(HealthPlan entity)
        {
            var record = await _unitOfWork.HealthPlanRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public void Edit(HealthPlan entity)
        {
            _unitOfWork.HealthPlanRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public IQueryable<HealthPlan> GetAsQueryable()
        {
            return _healthPlanRepository.GetAsQueryable();
        }

        public Task<IEnumerable<HealthPlan>> GetAsync()
        {
            return _healthPlanRepository.GetAsync();
        }

        public Task<HealthPlan> GetHealthPlanByIdAsync(int id)
        {
            return _healthPlanRepository.GetHealthPlanByIdAsync(id);
        }

        public void Remove(HealthPlan entity)
        {
            _unitOfWork.HealthPlanRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}