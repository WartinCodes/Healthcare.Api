using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

namespace Healthcare.Api.Service.Services
{
    public class BloodTestService : IBloodTestService
    {
        private readonly IBloodTestRepository _bloodTestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BloodTestService(IBloodTestRepository bloodTestRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _bloodTestRepository = bloodTestRepository;
        }

        public async Task<BloodTest> Add(BloodTest entity)
        {
            var record = await _unitOfWork.BloodTestRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public async Task Edit(BloodTest entity)
        {
            _unitOfWork.BloodTestRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public async Task<BloodTest> GetBloodTestByIdAsync(int id)
        {
            return await _bloodTestRepository.GetBloodTestByIdAsync(id);
        }

        public async Task<BloodTest?> GetBloodTestByNameAsync(string name)
        {
            return await _bloodTestRepository.GetBloodTestByNameAsync(name);
        }

        public void Remove(BloodTest entity)
        {
            _unitOfWork.BloodTestRepository.Remove(entity);
            _unitOfWork.Save();
        }

        public IQueryable<BloodTest> GetAsQueryable()
        {
            return _bloodTestRepository.GetAsQueryable();
        }
    }
}
