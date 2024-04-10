using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IDoctorService
    {
        IQueryable<Doctor> GetAsQueryable();
        Task<IEnumerable<Doctor>> GetAsync();
        Task<Doctor> GetDoctorByUserIdAsync(int userId);
        Task<Doctor> GetDoctorByIdAsync(int id);
        Task<Doctor> Add(Doctor entity);
        void Remove(Doctor entity);
        void Edit(Doctor entity);
    }
}