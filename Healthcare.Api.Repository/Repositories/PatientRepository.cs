using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        private readonly HealthcareDbContext _context;

        public PatientRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Patient> GetAsQueryable()
        {
            return _context.Patient.AsQueryable();
        }

        public async Task<Patient> GetPatientByUserIdAsync(int userId)
        {
            return await _context.Patient.Where(x => x.UserId == userId)
                .Include(x => x.User)
                .ThenInclude(x => x.Address)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.State)
                .ThenInclude(x => x.Country)
                .Include(x => x.HealthPlans)
                .ThenInclude(x => x.HealthInsurance)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _context.Patient.Where(x => x.Id == id)
                .Include(x => x.User)
                .ThenInclude(x => x.Address)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.State)
                .ThenInclude(x => x.Country)
                .Include(x => x.HealthPlans)
                .ThenInclude(x => x.HealthInsurance)
                .FirstOrDefaultAsync();
        }
    }
}
