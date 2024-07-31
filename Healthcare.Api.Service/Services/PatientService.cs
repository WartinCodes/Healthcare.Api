using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHealthPlanService _healthPlanService;
        private readonly IPatientHealthPlanService _patientHealthPlanService;

        public PatientService(
            IPatientRepository patientRepository,
            IPatientHealthPlanService patientHealthPlanService,
            IHealthPlanService healthPlanService,
            IUnitOfWork unitOfWork)
        {
            _patientRepository = patientRepository;
            _healthPlanService = healthPlanService;
            _patientHealthPlanService = patientHealthPlanService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Patient> Add(Patient entity)
        {
            var record = await _unitOfWork.PatientRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public async Task Edit(Patient entity)
        {
            var patientHealthPlans = await _patientHealthPlanService.GetHealthPlansByPatient(entity.Id);
            foreach (var php in patientHealthPlans)
            {
                _patientHealthPlanService.Remove(php);
            }

            foreach (var healthPlan in entity.HealthPlans)
            {
                var healthPlanEntity = await _healthPlanService.GetHealthPlanByIdAsync(healthPlan.Id);
                if (healthPlanEntity == null)
                {
                    await Task.CompletedTask;
                }

                var patientHealthPlan = new PatientHealthPlan { PatientId = entity.Id, HealthPlanId = healthPlan.Id };
                await _patientHealthPlanService.Add(patientHealthPlan);
            }

            _unitOfWork.PatientRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public IQueryable<Patient> GetAsQueryable()
        {
            return _patientRepository.GetAsQueryable();
        }

        public async Task<IEnumerable<Patient>> GetAsync()
        {
            return await _patientRepository.GetAsync();
        }

        public Task<Patient> GetPatientByUserIdAsync(int userId)
        {
            return _patientRepository.GetPatientByUserIdAsync(userId);
        }

        public Task<Patient> GetPatientByIdAsync(int id)
        {
            return _patientRepository.GetPatientByIdAsync(id);
        }

        public void Remove(Patient entity)
        {
            _unitOfWork.PatientRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}
