using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class SupportService : ISupportService
    {
        private readonly ISupportRepository _supportRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SupportService(ISupportRepository supportRepository, IUnitOfWork unitOfWork)
        {
            _supportRepository = supportRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Support> Add(Support entity)
        {
            var support = await _unitOfWork.SupportRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return support;
        }

        public void Edit(Support entity)
        {
            _unitOfWork.SupportRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public IQueryable<Support> GetAsQueryable()
        {
            return _supportRepository.GetAsQueryable();
        }

        public Task<IEnumerable<Support>> GetAsync()
        {
            return _supportRepository.GetAsync();
        }

        public void Remove(Support entity)
        {
            _unitOfWork.SupportRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}
