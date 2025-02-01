using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;
using Healthcare.Api.Core.Utilities;
using System.Linq.Expressions;

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

        public async Task<PagedResult<Patient>> GetPagedPatientsAsync(PaginationParams paginationParams)
        {
            return await _patientRepository.GetPagedAsync(
                filter: p => string.IsNullOrEmpty(paginationParams.Search) ||
                p.User.FirstName.Contains(paginationParams.Search) ||
                p.User.LastName.Contains(paginationParams.Search) ||
                p.User.UserName.Contains(paginationParams.Search),
                paginationParams: paginationParams,
                includes: new Expression<Func<Patient, object>>[]
                {
                    p => p.User,
                    p => p.HealthPlans     
                });
        }
    }
}
