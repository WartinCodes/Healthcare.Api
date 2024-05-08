using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IPatientHealthPlanRepository : IRepository<PatientHealthPlan>
    {
        Task<IEnumerable<PatientHealthPlan>> GetHealthPlansByPatientIdAsync(int id);
    }
}