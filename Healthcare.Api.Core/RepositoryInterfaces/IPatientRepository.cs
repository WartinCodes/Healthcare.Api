using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Utilities;
using System.Linq.Expressions;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<Patient> GetPatientByUserIdAsync(int id);
        Task<Patient> GetPatientByIdAsync(int id);
        IQueryable<Patient> GetAsQueryable();
        Task<PagedResult<Patient>> GetPagedAsync(Expression<Func<Patient, bool>> filter, PaginationParams paginationParams, params Expression<Func<Patient, object>>[] includes);
    }
}
