using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class DoctorSpecialityService : IDoctorSpecialityService
    {
        private readonly IDoctorSpecialityRepository _DoctorSpecialityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorSpecialityService(IDoctorSpecialityRepository DoctorSpecialityRepository, IUnitOfWork unitOfWork)
        {
            _DoctorSpecialityRepository = DoctorSpecialityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DoctorSpeciality> Add(DoctorSpeciality entity)
        {
            var record = await _unitOfWork.DoctorSpecialityRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public void Edit(DoctorSpeciality entity)
        {
            _unitOfWork.DoctorSpecialityRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public IQueryable<DoctorSpeciality> GetAsQueryable()
        {
            return _DoctorSpecialityRepository.GetAsQueryable();
        }

        public Task<IEnumerable<DoctorSpeciality>> GetAsync()
        {
            return _DoctorSpecialityRepository.GetAsync();
        }

        public void Remove(DoctorSpeciality entity)
        {
            _unitOfWork.DoctorSpecialityRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}
