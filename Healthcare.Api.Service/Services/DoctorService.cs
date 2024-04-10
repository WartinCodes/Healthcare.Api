using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork)
        {
            _doctorRepository = doctorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Doctor> Add(Doctor entity)
        {
            var record = await _unitOfWork.DoctorRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public void Edit(Doctor entity)
        {
            _unitOfWork.DoctorRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public IQueryable<Doctor> GetAsQueryable()
        {
            return _doctorRepository.GetAsQueryable();
        }

        public Task<IEnumerable<Doctor>> GetAsync()
        {
            return _doctorRepository.GetAsync();
        }

        public Task<Doctor> GetDoctorByUserIdAsync(int userId)
        {
            return _doctorRepository.GetDoctorByUserIdAsync(userId);
        }

        public Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return _doctorRepository.GetDoctorByIdAsync(id);
        }

        public void Remove(Doctor entity)
        {
            _unitOfWork.DoctorRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}
