using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IHealthInsuranceRepository : IRepository<HealthInsurance>
    {
        Task<HealthInsurance> GetHealthInsuranceByIdAsync(int id);
    }
}