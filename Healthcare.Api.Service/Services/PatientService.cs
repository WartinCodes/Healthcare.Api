using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;
using Healthcare.Api.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Amazon.S3.Util.S3EventNotification;

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
            return _patientRepository.GetAsQueryable();
        }

        public async Task<PagedResult<Patient>> GetPagedPatientsAsync(PaginationParams paginationParams)
        {
            var searchTerms = paginationParams.Search?
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList() ?? new List<string>();

            var query = GetAsQueryable();

            if (searchTerms.Any())
            {
                foreach (var term in searchTerms)
                {
                    var tempTerm = term;
                    query = query.Where(p =>
                        EF.Functions.Like(p.User.FirstName, $"%{tempTerm}%") ||
                        EF.Functions.Like(p.User.LastName, $"%{tempTerm}%") ||
                        EF.Functions.Like(p.User.UserName, $"%{tempTerm}%"));
                }
            }

            int totalRecords = await query.CountAsync();

            var data = await query
                .Skip((paginationParams.Page - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return new PagedResult<Patient>(data, totalRecords, paginationParams.Page, paginationParams.PageSize);
        }
    }
}
