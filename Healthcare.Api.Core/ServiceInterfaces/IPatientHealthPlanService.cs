using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IPatientHealthPlanService
    {
        IQueryable<PatientHealthPlan> GetAsQueryable();
        Task<IEnumerable<PatientHealthPlan>> GetAsync();
        Task<IEnumerable<PatientHealthPlan>> GetHealthPlansByPatient(int patientId);
        Task<PatientHealthPlan> Add(PatientHealthPlan entity);
        void Remove(PatientHealthPlan entity);
        Task Update(Patient entity);
    }
}
