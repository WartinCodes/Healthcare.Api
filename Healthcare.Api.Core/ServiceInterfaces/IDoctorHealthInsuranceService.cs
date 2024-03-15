using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IDoctorHealthInsuranceService
    {
        IQueryable<DoctorHealthInsurance> GetAsQueryable();
        Task<IEnumerable<DoctorHealthInsurance>> GetAsync();
        Task<IEnumerable<DoctorHealthInsurance>> GetHealthPlansByDoctor(int doctorId);
        Task<DoctorHealthInsurance> Add(DoctorHealthInsurance entity);
        void Remove(DoctorHealthInsurance entity);
        void Edit(DoctorHealthInsurance entity);
    }
}
