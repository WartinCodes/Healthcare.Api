using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class SpecialityService : ISpecialityService
    {
        private readonly ISpecialityRepository _specialityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SpecialityService(ISpecialityRepository specialityRepository, IUnitOfWork unitOfWork)
        {
            _specialityRepository = specialityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Speciality> Add(Speciality entity)
        {
            var record = await _unitOfWork.SpecialityRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public void Edit(Speciality entity)
        {
            _unitOfWork.SpecialityRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public IQueryable<Speciality> GetAsQueryable()
        {
            return _specialityRepository.GetAsQueryable();
        }

        public Task<IEnumerable<Speciality>> GetAsync()
        {
            return _specialityRepository.GetAsync();
        }

        public Task<Speciality> GetSpecialityByIdAsync(int id)
        {
            return _specialityRepository.GetSpecialityByIdAsync(id);
        }

        public void Remove(Speciality entity)
        {
            _unitOfWork.SpecialityRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}
