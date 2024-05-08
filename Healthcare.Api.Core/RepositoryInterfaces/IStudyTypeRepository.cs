using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IStudyTypeRepository : IRepository<StudyType>
    {
        Task<StudyType> GetStudyTypeByIdAsync(int id);
    }
}
