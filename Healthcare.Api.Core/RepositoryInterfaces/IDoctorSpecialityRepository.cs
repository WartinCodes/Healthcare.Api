using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IDoctorSpecialityRepository : IRepository<DoctorSpeciality>
    {
        Task<IEnumerable<DoctorSpeciality>> GetSpecialitiesByDoctorIdAsync(int doctorId);
    }
}
