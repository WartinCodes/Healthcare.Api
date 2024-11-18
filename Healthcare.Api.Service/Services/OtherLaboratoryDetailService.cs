using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class OtherLaboratoryDetailService : IOtherLaboratoryDetailService
    {
        private readonly IOtherLaboratoryDetailsRepository _otherLaboratoryDetailsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OtherLaboratoryDetailService(IOtherLaboratoryDetailsRepository otherLaboratoryDetailsRepository, IUnitOfWork unitOfWork)
        {
            _otherLaboratoryDetailsRepository = otherLaboratoryDetailsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddRange(List<OtherLaboratoryDetail> entities, int idLaboratoryDetail)
        {
            foreach (var entity in entities)
            {
                entity.IdLaboratoryDetail = idLaboratoryDetail;
                var record = await _unitOfWork.OtherLaboratoryDetailsRepository.AddAsync(entity);
                await _unitOfWork.SaveAsync();
            }
        }

        public void Edit(OtherLaboratoryDetail entity)
        {
            _otherLaboratoryDetailsRepository.Edit(entity);
        }

        public async Task<IEnumerable<OtherLaboratoryDetail>> GetAsync()
        {
            return await _otherLaboratoryDetailsRepository.GetAsync();
        }

        public void Remove(OtherLaboratoryDetail entity)
        {
            _unitOfWork.OtherLaboratoryDetailsRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}
