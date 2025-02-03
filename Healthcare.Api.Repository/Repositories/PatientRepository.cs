using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.Utilities;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            return _context.Patient.Include(p => p.User).Include(p => p.HealthPlans).AsQueryable();
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

        public async Task<PagedResult<Patient>> GetPagedAsync(
            Expression<Func<Patient, bool>> filter,
            PaginationParams paginationParams,
            params Expression<Func<Patient, object>>[] includes)
        {
            var query = _context.Patient.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var totalRecords = await query.CountAsync();

            var data = await query
                .Skip((paginationParams.Page - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return new PagedResult<Patient>(data, totalRecords, paginationParams.Page, paginationParams.PageSize);
        }
    }
}