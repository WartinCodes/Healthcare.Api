using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IDoctorSpecialityService
    {
        IQueryable<DoctorSpeciality> GetAsQueryable();
        Task<IEnumerable<DoctorSpeciality>> GetAsync();
        Task<DoctorSpeciality> Add(DoctorSpeciality entity);
        void Remove(DoctorSpeciality entity);
        void Edit(DoctorSpeciality entity);
    }
}
