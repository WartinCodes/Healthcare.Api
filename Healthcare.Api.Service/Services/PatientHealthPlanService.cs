using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class PatientHealthPlanService : IPatientHealthPlanService
    {
        private readonly IPatientHealthPlanRepository _patientHealthPlanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PatientHealthPlanService(IPatientHealthPlanRepository patientHealthPlanRepository, IUnitOfWork unitOfWork)
        {
            _patientHealthPlanRepository = patientHealthPlanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PatientHealthPlan> Add(PatientHealthPlan entity)
        {
            var record = await _unitOfWork.PatientHealthPlanRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public void Edit(PatientHealthPlan entity)
        {
            _unitOfWork.PatientHealthPlanRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public IQueryable<PatientHealthPlan> GetAsQueryable()
        {
            return _patientHealthPlanRepository.GetAsQueryable();
        }

        public Task<IEnumerable<PatientHealthPlan>> GetAsync()
        {
            return _patientHealthPlanRepository.GetAsync();
        }

        public void Remove(PatientHealthPlan entity)
        {
            _unitOfWork.PatientHealthPlanRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}
