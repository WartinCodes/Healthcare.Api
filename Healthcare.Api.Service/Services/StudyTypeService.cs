using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class StudyTypeService : IStudyTypeService
    {
        private readonly IStudyTypeRepository _studyTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudyTypeService(IStudyTypeRepository studyTypeRepository, IUnitOfWork unitOfWork)
        {
            _studyTypeRepository = studyTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<StudyType> Add(StudyType entity)
        {
            var record = await _unitOfWork.StudyTypeRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public void Edit(StudyType entity)
        {
            _unitOfWork.StudyTypeRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public IQueryable<StudyType> GetAsQueryable()
        {
            return _studyTypeRepository.GetAsQueryable();
        }

        public Task<IEnumerable<StudyType>> GetAsync()
        {
            return _studyTypeRepository.GetAsync();
        }

        public async Task<StudyType> GetStudyTypeByIdAsync(int id)
        {
            return await _studyTypeRepository.GetStudyTypeByIdAsync(id);
        }

        public void Remove(StudyType entity)
        {
            _unitOfWork.StudyTypeRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}