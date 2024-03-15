using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class DoctorHealthInsuranceService : IDoctorHealthInsuranceService
    {
        private readonly IDoctorHealthInsuranceRepository _doctorHealthPlanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorHealthInsuranceService(IDoctorHealthInsuranceRepository doctorHealthPlanRepository, IUnitOfWork unitOfWork)
        {
            _doctorHealthPlanRepository = doctorHealthPlanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DoctorHealthInsurance> Add(DoctorHealthInsurance entity)
        {
            var record = await _unitOfWork.DoctorHealthInsuranceRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public void Edit(DoctorHealthInsurance entity)
        {
            _unitOfWork.DoctorHealthInsuranceRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public IQueryable<DoctorHealthInsurance> GetAsQueryable()
        {
            return _doctorHealthPlanRepository.GetAsQueryable();
        }

        public Task<IEnumerable<DoctorHealthInsurance>> GetAsync()
        {
            return _doctorHealthPlanRepository.GetAsync();
        }

        public async Task<IEnumerable<DoctorHealthInsurance>> GetHealthPlansByDoctor(int doctorId)
        {
            return await _doctorHealthPlanRepository.GetHealthPlansByDoctorIdAsync(doctorId);
        }

        public void Remove(DoctorHealthInsurance entity)
        {
            _unitOfWork.DoctorHealthInsuranceRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}
