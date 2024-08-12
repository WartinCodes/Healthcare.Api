using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class DoctorSpecialityService : IDoctorSpecialityService
    {
        private readonly IDoctorSpecialityRepository _doctorSpecialityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorSpecialityService(IDoctorSpecialityRepository DoctorSpecialityRepository, IUnitOfWork unitOfWork)
        {
            _doctorSpecialityRepository = DoctorSpecialityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DoctorSpeciality> Add(DoctorSpeciality entity)
        {
            var record = await _unitOfWork.DoctorSpecialityRepository.AddAsync(entity);
            return record;
        }

        public void Edit(DoctorSpeciality entity)
        {
            _unitOfWork.DoctorSpecialityRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public IQueryable<DoctorSpeciality> GetAsQueryable()
        {
            return _doctorSpecialityRepository.GetAsQueryable();
        }

        public Task<IEnumerable<DoctorSpeciality>> GetAsync()
        {
            return _doctorSpecialityRepository.GetAsync();
        }

        public async Task<IEnumerable<DoctorSpeciality>> GetSpecialitiesByDoctor(int doctorId)
        {
            return await _doctorSpecialityRepository.GetSpecialitiesByDoctorIdAsync(doctorId);
        }

        public void Remove(DoctorSpeciality entity)
        {
            _unitOfWork.DoctorSpecialityRepository.Remove(entity);
        }
    }
}
