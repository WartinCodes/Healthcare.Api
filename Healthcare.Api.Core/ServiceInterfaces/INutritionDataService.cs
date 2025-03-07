using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface INutritionDataService
    {
        Task<NutritionData?> GetByIdAsync(int id);
        Task<IEnumerable<NutritionData>> GetNutritionDatasByPatient(int patientId);
        Task<NutritionData> Add(NutritionData entity);
        Task AddRange(List<NutritionData> entities);
        void Edit(NutritionData entity);
        void Remove(NutritionData entity);
    }
}
