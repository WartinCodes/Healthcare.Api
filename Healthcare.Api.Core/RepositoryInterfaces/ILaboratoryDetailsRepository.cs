using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface ILaboratoryDetailsRepository : IRepository<LaboratoryDetail>
    {
        Task<IEnumerable<LaboratoryDetail>> GetLaboratoriesByUserId(int userId);
        Task<LaboratoryDetail> GetLaboratoriesByStudyId(int studyId);
    }
}
