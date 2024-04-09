using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<Doctor> GetDoctorByUserIdAsync(int userId);
        Task<Doctor> GetDoctorByIdAsync(int id);
    }
}
