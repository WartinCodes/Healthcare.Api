using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IPatientHealthPlanService
    {
        IQueryable<PatientHealthPlan> GetAsQueryable();
        Task<IEnumerable<PatientHealthPlan>> GetAsync();
        Task<IEnumerable<PatientHealthPlan>> GetHealthPlansByPatient(int patientId);
        Task<PatientHealthPlan> Add(PatientHealthPlan entity);
        Task Remove(PatientHealthPlan entity);
        Task Edit(PatientHealthPlan entity);
    }
}
