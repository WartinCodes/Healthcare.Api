using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        private readonly HealthcareDbContext _context;

        public DoctorRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Doctor> GetAsQueryable()
        {
            return _context.Doctor.AsQueryable();
        }

        public async Task<IEnumerable<Doctor>> GetAsync()
        {
            return await base.GetAsync().ConfigureAwait(false);
        } 

        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctor.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public void Remove(Doctor entity)
        {
            base.Delete(entity.Id);
        }

        public void Edit(Doctor entity)
        {
            _context.Doctor.Update(entity);
        }

        public async Task<Doctor> AddAsync(Doctor entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }
    }
}
