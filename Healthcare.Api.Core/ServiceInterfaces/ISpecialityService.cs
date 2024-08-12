using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface ISpecialityService
    {
        IQueryable<Speciality> GetAsQueryable();
        Task<IEnumerable<Speciality>> GetAsync();
        Task<Speciality> GetSpecialityByNameAsync(string name);
        Task<Speciality> GetSpecialityByIdAsync(int id);
        Task<Speciality> Add(Speciality entity);
        void Remove(Speciality entity);
        void Edit(Speciality entity);
    }
}
