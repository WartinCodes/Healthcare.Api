using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IBloodTestDataRepository : IBaseRepository<BloodTestData>
    {
        Task<IEnumerable<BloodTestData>> GetByStudyIdAsync(int studyId);
        Task<IEnumerable<BloodTestData>> GetBloodTestDatasByStudyIdsAsync(int[] studiesIds);
    }
}
