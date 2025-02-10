using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface INutritionDataRepository : IRepository<NutritionData>
    {
        Task<NutritionData?> GetByIdAsync(int id);
    }
}
