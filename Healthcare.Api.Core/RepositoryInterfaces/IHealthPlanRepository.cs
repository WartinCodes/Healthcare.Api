using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IHealthPlanRepository : IRepository<HealthPlan>
    {
        Task<HealthPlan> GetHealthPlanByIdAsync(int id);
        Task<IEnumerable<HealthPlan>> GetHealthPlansByHealthInsuranceId(int id);
    }
}