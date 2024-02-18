using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<Patient> GetPatientByIdAsync(int id);
    }
}
