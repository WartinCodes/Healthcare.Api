using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IHealthPlanService
    {
        IQueryable<HealthPlan> GetAsQueryable();
        Task<IEnumerable<HealthPlan>> GetAsync();
        Task<HealthPlan> GetHealthPlanByIdAsync(int id);
        Task<HealthPlan> Add(HealthPlan entity);
        void Remove(HealthPlan entity);
        void Edit(HealthPlan entity);
    }
}
