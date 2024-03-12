using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IDoctorHealthInsuranceService
    {
        IQueryable<DoctorHealthInsurance> GetAsQueryable();
        Task<IEnumerable<DoctorHealthInsurance>> GetAsync();
        Task<DoctorHealthInsurance> Add(DoctorHealthInsurance entity);
        void Remove(DoctorHealthInsurance entity);
        void Edit(DoctorHealthInsurance entity);
    }
}
