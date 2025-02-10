using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface INutritionDataService
    {
        Task<NutritionData?> GetByIdAsync(int id);
        Task<NutritionData> Add(NutritionData entity);
        void Edit(NutritionData entity);
        void Remove(NutritionData entity);
    }
}
