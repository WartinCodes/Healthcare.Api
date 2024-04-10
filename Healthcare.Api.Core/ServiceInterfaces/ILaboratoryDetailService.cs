using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface ILaboratoryDetailService
    {
        Task<IEnumerable<LaboratoryDetail>> GetAsync();
        Task<LaboratoryDetail> GetLaboratoryDetailsByIdAsync(int id);
        Task<LaboratoryDetail> Add(LaboratoryDetail entity);
        void Remove(LaboratoryDetail entity);
        void Edit(LaboratoryDetail entity);
    }
}
