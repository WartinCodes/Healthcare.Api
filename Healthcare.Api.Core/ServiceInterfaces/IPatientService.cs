using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Utilities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IPatientService
    {
        IQueryable<Patient> GetAsQueryable();
        Task<IEnumerable<Patient>> GetAsync();
        Task<Patient> GetPatientByUserIdAsync(int userId);
        Task<Patient> GetPatientByIdAsync(int id);
        Task<Patient> Add(Patient entity);
        void Remove(Patient entity);
        Task Edit(Patient entity);
        Task<PagedResult<Patient>> GetPagedPatientsAsync(PaginationParams paginationParams);
    }
}