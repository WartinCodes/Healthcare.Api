using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IDoctorHealthPlanService
    {
        IQueryable<DoctorHealthPlan> GetAsQueryable();
        Task<IEnumerable<DoctorHealthPlan>> GetAsync();
        Task<DoctorHealthPlan> Add(DoctorHealthPlan entity);
        void Remove(DoctorHealthPlan entity);
        void Edit(DoctorHealthPlan entity);
    }
}
