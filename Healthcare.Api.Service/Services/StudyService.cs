using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Extensions;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;
using Healthcare.Api.Core.Utilities;

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

        public async Task<IEnumerable<Study>> GetStudiesByUserId(int userId)
        {
            return await _studyRepository.GetStudiesByUserId(userId);
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

        public string GenerateFileName(FileNameParameters parameters)
        {
            var fileDate = parameters.Date.ToString().Replace("/", "").Trim();
            if (parameters.Number.HasValue && !string.IsNullOrEmpty(parameters.Note) && !string.IsNullOrEmpty(parameters.Extension))
            {
                return $"{parameters.Number.Value}-{parameters.User.LastName}{parameters.User.FirstName}{parameters.StudyType.Name}-{parameters.Note}-{fileDate}{parameters.Extension}";
            }
            else
            {
                return $"{parameters.User.LastName}{parameters.User.FirstName}{parameters.StudyType.Name}-{fileDate}{parameters.Extension}";
            }
        }
    }
}