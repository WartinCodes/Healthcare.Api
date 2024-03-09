using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IPatientHealthPlanService
    {
        IQueryable<PatientHealthPlan> GetAsQueryable();
        Task<IEnumerable<PatientHealthPlan>> GetAsync();
        Task<PatientHealthPlan> Add(PatientHealthPlan entity);
        void Remove(PatientHealthPlan entity);
        void Edit(PatientHealthPlan entity);
    }
}
