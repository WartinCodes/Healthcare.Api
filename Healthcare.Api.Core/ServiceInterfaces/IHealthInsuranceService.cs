using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IHealthInsuranceService
    {
        IQueryable<HealthInsurance> GetAsQueryable();
        Task<IEnumerable<HealthInsurance>> GetAsync();
        Task<HealthInsurance> GetHealthInsuranceByIdAsync(int id);
        Task<HealthInsurance> Add(HealthInsurance entity);
        void Remove(HealthInsurance entity);
        void Edit(HealthInsurance entity);
    }
}
