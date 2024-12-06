using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UnitService(IUnitRepository unitRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unitRepository = unitRepository;
        }

        public async Task<Unit> Add(Unit entity)
        {
            var record = await _unitOfWork.UnitRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public async Task Edit(Unit entity)
        {
            _unitOfWork.UnitRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public Task<Unit> GetUnitByIdAsync(int id)
        {
            return _unitRepository.GetUnitByIdAsync(id);
        }

        public void Remove(Unit entity)
        {
            _unitOfWork.UnitRepository.Remove(entity);
            _unitOfWork.Save();
        }

        public IQueryable<Unit> GetAsQueryable()
        {
            return _unitRepository.GetAsQueryable();
        }
    }
}
