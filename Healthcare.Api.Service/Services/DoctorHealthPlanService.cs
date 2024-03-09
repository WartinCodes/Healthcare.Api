using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class DoctorHealthPlanService : IDoctorHealthPlanService
    {
        private readonly IDoctorHealthPlanRepository _doctorHealthPlanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorHealthPlanService(IDoctorHealthPlanRepository doctorHealthPlanRepository, IUnitOfWork unitOfWork)
        {
            _doctorHealthPlanRepository = doctorHealthPlanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DoctorHealthPlan> Add(DoctorHealthPlan entity)
        {
            var record = await _unitOfWork.DoctorHealthPlanRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public void Edit(DoctorHealthPlan entity)
        {
            _unitOfWork.DoctorHealthPlanRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public IQueryable<DoctorHealthPlan> GetAsQueryable()
        {
            return _doctorHealthPlanRepository.GetAsQueryable();
        }

        public Task<IEnumerable<DoctorHealthPlan>> GetAsync()
        {
            return _doctorHealthPlanRepository.GetAsync();
        }

        public void Remove(DoctorHealthPlan entity)
        {
            _unitOfWork.DoctorHealthPlanRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}
