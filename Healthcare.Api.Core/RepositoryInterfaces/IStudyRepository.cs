using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IStudyRepository : IRepository<Study>
    {
        Task<Study> GetStudyByIdAsync(int id);
        Task<IEnumerable<Study>> GetStudiesByPatientId(int id);
    }
}
