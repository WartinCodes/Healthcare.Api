using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class PatientHealthPlanService : IPatientHealthPlanService
    {
        private readonly IPatientHealthPlanRepository _patientHealthPlanRepository;
        private readonly IHealthPlanService _healthPlanService;
        private readonly IUnitOfWork _unitOfWork;

        public PatientHealthPlanService(IPatientHealthPlanRepository patientHealthPlanRepository, IHealthPlanService healthPlanService, IUnitOfWork unitOfWork)
        {
            _patientHealthPlanRepository = patientHealthPlanRepository;
            _unitOfWork = unitOfWork;
            _healthPlanService = healthPlanService;
        }

        public async Task<PatientHealthPlan> Add(PatientHealthPlan entity)
        {
            var record = await _unitOfWork.PatientHealthPlanRepository.InsertAsync(entity);
            _unitOfWork.Save();
            return record;
        }

        public async Task Update(Patient entity)
        {
            var patientHealthPlans = await GetHealthPlansByPatient(entity.Id);

            foreach (var existingHealthPlan in patientHealthPlans)
            {
                _patientHealthPlanRepository.Delete(existingHealthPlan);
            }

            foreach (var healthPlan in entity.HealthPlans)
            {
                var healthPlanEntity = await _healthPlanService.GetHealthPlanByIdAsync(healthPlan.Id);
                if (healthPlanEntity == null) continue;
                
                var newPatientHealthPlan = new PatientHealthPlan { PatientId = entity.Id, HealthPlanId = healthPlan.Id };
                await _patientHealthPlanRepository.InsertAsync(newPatientHealthPlan);
            }
        }

        public IQueryable<PatientHealthPlan> GetAsQueryable()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PatientHealthPlan>> GetAsync()
        {
            return _patientHealthPlanRepository.GetAsync();
        }

        public async Task<IEnumerable<PatientHealthPlan>> GetHealthPlansByPatient(int patientId)
        {
            return await _patientHealthPlanRepository.GetHealthPlansByPatientIdAsync(patientId);
        }

        public void Remove(PatientHealthPlan entity)
        {
            _unitOfWork.PatientHealthPlanRepository.Delete(entity);
        }
    }
}
