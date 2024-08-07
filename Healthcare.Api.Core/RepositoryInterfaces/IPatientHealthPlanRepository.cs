using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IPatientHealthPlanRepository : IBaseRepository<PatientHealthPlan>
    {
        Task<IEnumerable<PatientHealthPlan>> GetHealthPlansByPatientIdAsync(int id);
    }
}