using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface INutritionDataRepository : IBaseRepository<NutritionData>
    {
        Task<NutritionData?> GetByIdAsync(int id);
        Task<IEnumerable<NutritionData>> GetNutritionDatasByUser(int userId);
    }
}
