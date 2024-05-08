using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class DoctorSpecialityRepository : BaseRepository<DoctorSpeciality>, IDoctorSpecialityRepository
    {
        private readonly HealthcareDbContext _context;

        public DoctorSpecialityRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<DoctorSpeciality> GetAsQueryable()
        {
            return _context.DoctorSpeciality.AsQueryable();
        }

        public async Task<IEnumerable<DoctorSpeciality>> GetAsync()
        {
            return await base.GetAsync();
        }

        public async Task<DoctorSpeciality> GetDoctorSpecialityByIdAsync(int id)
        {
            return await _context.DoctorSpeciality.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(DoctorSpeciality entity)
        {
            base.Delete(entity.Id);
        }

        public void Edit(DoctorSpeciality entity)
        {
            _context.DoctorSpeciality.Update(entity);
        }

        public async Task<DoctorSpeciality> AddAsync(DoctorSpeciality entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }

        public async Task<IEnumerable<DoctorSpeciality>> GetSpecialitiesByDoctorIdAsync(int doctorId)
        {
            return await _context.DoctorSpeciality.Where(x => x.DoctorId == doctorId)
                .ToListAsync();
        }
    }
}
