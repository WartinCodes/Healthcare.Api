using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class StudyService : IStudyService
    {
        private readonly IStudyRepository _studyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudyService(IStudyRepository studyRepository, IUnitOfWork unitOfWork)
        {
            _studyRepository = studyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Study> Add(Study entity)
        {
            var record = await _unitOfWork.StudyRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public void Edit(Study entity)
        {
            _unitOfWork.StudyRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public IQueryable<Study> GetAsQueryable()
        {
            return _studyRepository.GetAsQueryable();
        }

        public async Task<IEnumerable<Study>> GetAsync()
        {
            return await _studyRepository.GetAsync();
        }

        public async Task<IEnumerable<Study>> GetStudiesByPatientId(int id)
        {
            return await _studyRepository.GetStudiesByPatientId(id);
        }

        public async Task<Study> GetStudyByIdAsync(int id)
        {
            return await _studyRepository.GetStudyByIdAsync(id);
        }

        public void Remove(Study entity)
        {
            _unitOfWork.StudyRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}