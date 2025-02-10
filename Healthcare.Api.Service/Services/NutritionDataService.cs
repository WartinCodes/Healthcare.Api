using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.UnitOfWorks;

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
            var record = await _unitOfWork.NutritionDataRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return record;
        }

        public void Edit(NutritionData entity)
        {
            _unitOfWork.NutritionDataRepository.Edit(entity);
            _unitOfWork.Save();
        }

        public async Task<NutritionData?> GetByIdAsync(int id)
        {
            return await _nutritionDataRepository.GetByIdAsync(id);
        }

        public void Remove(NutritionData entity)
        {
            _unitOfWork.NutritionDataRepository.Remove(entity);
            _unitOfWork.Save();
        }
    }
}
