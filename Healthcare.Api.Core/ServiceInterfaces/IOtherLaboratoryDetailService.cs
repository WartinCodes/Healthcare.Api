using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IOtherLaboratoryDetailService
    {
        Task<IEnumerable<OtherLaboratoryDetail>> GetAsync();
        Task AddRange(List<OtherLaboratoryDetail> entities, int idLaboratoryDetail);
        void Remove(OtherLaboratoryDetail entity);
        void Edit(OtherLaboratoryDetail entity);
    }
}
