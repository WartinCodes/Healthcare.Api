using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;
using System.Xml.Linq;

namespace Healthcare.Api.Service.Services
{
    public class BloodTestDataService : IBloodTestDataService
    {
        private readonly IBloodTestDataRepository _bloodTestDataRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BloodTestDataService(IBloodTestDataRepository bloodTestDataRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _bloodTestDataRepository = bloodTestDataRepository;
        }

        public async Task<BloodTestData> Add(BloodTestData entity)
        {
            var record = await _unitOfWork.BloodTestDataRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public async Task AddRangeAsync(List<BloodTestData> entities)
        {
            await _unitOfWork.BloodTestDataRepository.InsertRangeAsync(entities);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddRangeAsync(int studyId, List<BloodTestData> entities)
        {
            foreach (var item in entities)
            {
                item.IdStudy = studyId;
            }
            await _unitOfWork.BloodTestDataRepository.InsertRangeAsync(entities);
            await _unitOfWork.SaveAsync();
        }

        public async Task Edit(BloodTestData entity)
        {
            _unitOfWork.BloodTestDataRepository.Update(entity);
            _unitOfWork.Save();
        }

        public Task<IEnumerable<BloodTestData>> GetBloodTestDataAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BloodTestData> GetBloodTestDataByIdAsync(int id)
        {
            return await _bloodTestDataRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<BloodTestData>> GetBloodTestDatasByStudyIdAsync(int studyId)
        {
            return await _bloodTestDataRepository.GetByStudyIdAsync(studyId);
        }

        public async Task<IEnumerable<BloodTestData>> GetBloodTestDatasByStudyIdsAsync(int[] studiesIds)
        {
            return await _bloodTestDataRepository.GetBloodTestDatasByStudyIdsAsync(studiesIds);
        }

        public void Remove(BloodTestData entity)
        {
            _unitOfWork.BloodTestDataRepository.Delete(entity);
            _unitOfWork.Save();
        }
    }
}
