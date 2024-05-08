using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IDoctorHealthInsuranceRepository : IRepository<DoctorHealthInsurance>
    {
        Task<IEnumerable<DoctorHealthInsurance>> GetHealthPlansByDoctorIdAsync(int doctorId);
    }
}