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
            return await _context.Doctor
                .Include(x => x.User)
                .ThenInclude(x => x.Address)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.State)
                .ThenInclude(x => x.Country)
                .Include(x => x.Specialities)
                .Include(x => x.HealthInsurances)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctor.Where(x => x.Id == id)
                .Include(x => x.User)
                .ThenInclude(x => x.Address)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.State)
                .ThenInclude(x => x.Country)
                .Include(x => x.Specialities)
                .Include(x => x.HealthInsurances)
                .FirstOrDefaultAsync();
        }

        public void Remove(Doctor entity)
        {
            _context.Entry(entity.User).State = EntityState.Detached;
            _context.Entry(entity.User.Address).State = EntityState.Detached;
            _context.Remove(entity);
        }

        public void Edit(Doctor entity)
        {
            _context.Entry(entity.User).State = EntityState.Detached;
            _context.Entry(entity.User.Address.City).State = EntityState.Detached;
            _context.Entry(entity.User.Address.City.State).State = EntityState.Detached;
            _context.Entry(entity.User.Address.City.State.Country).State = EntityState.Detached;
            _context.Doctor.Update(entity);
        }

        public async Task<Doctor> AddAsync(Doctor entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }

        public async Task<Doctor> GetDoctorByUserIdAsync(int userId)
        {
            return await _context.Doctor.Where(x => x.UserId == userId)
                .Include(x => x.User)
                .ThenInclude(x => x.Address)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.State)
                .ThenInclude(x => x.Country)
                .Include(x => x.Specialities)
                .Include(x => x.HealthInsurances)
                .FirstOrDefaultAsync();
        }
    }
}
