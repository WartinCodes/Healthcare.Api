using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface ILaboratoryDetailService
    {
        Task<IEnumerable<LaboratoryDetail>> GetAsync();
        Task<IEnumerable<LaboratoryDetail>> GetLaboratoriesDetailsByUserIdAsync(int userId);
        Task<LaboratoryDetail> GetLaboratoriesDetailsByStudyIdAsync(int studyId);
        Task<LaboratoryDetail> Add(LaboratoryDetail entity);
        void Remove(LaboratoryDetail entity);
        void Edit(LaboratoryDetail entity);
    }
}
