using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class HealthPlanRepository : BaseRepository<HealthPlan>, IHealthPlanRepository
    {
        private readonly HealthcareDbContext _context;

        public HealthPlanRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<HealthPlan> GetAsQueryable()
        {
            return _context.HealthPlan.AsQueryable();
        }

        public async Task<IEnumerable<HealthPlan>> GetAsync()
        {
            return await _context.HealthPlan
                .Include(x => x.HealthInsurance)
                .ToListAsync();
        }

        public async Task<HealthPlan> GetHealthPlanByIdAsync(int id)
        {
            return await _context.HealthPlan.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(HealthPlan entity)
        {
            base.Delete(entity.Id);
        }

        public void Edit(HealthPlan entity)
        {
            _context.HealthPlan.Update(entity);
        }

        public async Task<HealthPlan> AddAsync(HealthPlan entity)
        {
            _context.Attach(entity.HealthInsurance);
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }
    }
}
