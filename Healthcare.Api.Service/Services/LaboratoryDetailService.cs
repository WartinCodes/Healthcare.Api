using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class LaboratoryDetailService : ILaboratoryDetailService
    {
        private readonly ILaboratoryDetailsRepository _laboratoryDetailsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LaboratoryDetailService(ILaboratoryDetailsRepository hemogramaRepository, IUnitOfWork unitOfWork)
        {
            _laboratoryDetailsRepository = hemogramaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LaboratoryDetail> Add(LaboratoryDetail entity)
        {
            var record = await _unitOfWork.LaboratoryDetailsRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public void Edit(LaboratoryDetail entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LaboratoryDetail>> GetAsync()
        {
            return _laboratoryDetailsRepository.GetAsync();
        }

        public async Task<IEnumerable<LaboratoryDetail>> GetLaboratoriesDetailsByUserIdAsync(int userId)
        {
            return await _laboratoryDetailsRepository.GetLaboratoriesByUserId(userId);
        }

        public void Remove(LaboratoryDetail entity)
        {
            _unitOfWork.LaboratoryDetailsRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}
