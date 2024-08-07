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
            var record = await _unitOfWork.PatientRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public async Task Edit(Patient entity)
        {
            _unitOfWork.PatientRepository.Update(entity);
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<Patient>> GetAsync()
        {
            return await _patientRepository.GetAsync(
                null,
                null,
                "User,User.Address,User.Address.City,User.Address.City.State,User.Address.City.State.Country,HealthPlans,HealthPlans.HealthInsurance"
            );
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
            _unitOfWork.PatientRepository.Delete(entity);
            _unitOfWork.Save();
        }

        public IQueryable<Patient> GetAsQueryable()
        {
            throw new NotImplementedException();
        }
    }
}
