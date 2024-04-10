using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IStudyTypeService
    {
        IQueryable<StudyType> GetAsQueryable();
        Task<IEnumerable<StudyType>> GetAsync();
        Task<StudyType> GetStudyTypeByIdAsync(int id);
        Task<StudyType> Add(StudyType entity);
        void Remove(StudyType entity);
        void Edit(StudyType entity);
    }
}