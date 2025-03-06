using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;
using static Amazon.S3.Util.S3EventNotification;

namespace Healthcare.Api.Service.Services
{
    public class NutritionDataService : INutritionDataService
    {
        private readonly INutritionDataRepository _nutritionDataRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NutritionDataService(INutritionDataRepository nutritionDataRepository, IUnitOfWork unitOfWork)
        {
            _nutritionDataRepository = nutritionDataRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<NutritionData> Add(NutritionData entity)
        {
            var record = await _unitOfWork.NutritionDataRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public async Task AddRange(List<NutritionData> entities)
        {
            await _unitOfWork.NutritionDataRepository.InsertRangeAsync(entities);
            await _unitOfWork.SaveAsync();
        }

        public void Edit(NutritionData entity)
        {
            _unitOfWork.NutritionDataRepository.Update(entity);
            _unitOfWork.Save();
        }

        public async Task<NutritionData?> GetByIdAsync(int id)
        {
            return await _nutritionDataRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<NutritionData>> GetNutritionDatasByPatient(int patientId)
        {
            return await _nutritionDataRepository.GetNutritionDatasByPatient(patientId);
        }

        public void Remove(NutritionData entity)
        {
            _unitOfWork.NutritionDataRepository.Delete(entity);
            _unitOfWork.Save();
        }
    }
}
