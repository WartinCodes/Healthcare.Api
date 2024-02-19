using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface ISpecialityRepository : IRepository<Speciality>
    {
        Task<Speciality> GetSpecialityByIdAsync(int id);
    }
}
