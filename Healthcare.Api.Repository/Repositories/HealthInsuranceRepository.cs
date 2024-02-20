using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class HealthInsuranceRepository : BaseRepository<HealthInsurance>, IHealthInsuranceRepository
    {
        private readonly HealthcareDbContext _context;

        public HealthInsuranceRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<HealthInsurance> GetAsQueryable()
        {
            return _context.HealthInsurance.AsQueryable();
        }

        public async Task<IEnumerable<HealthInsurance>> GetAsync()
        {
            return await base.GetAsync().ConfigureAwait(false);
        }

        public async Task<HealthInsurance> GetHealthInsuranceByIdAsync(int id)
        {
            return await _context.HealthInsurance.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(HealthInsurance entity)
        {
            base.Delete(entity.Id);
        }

        public void Edit(HealthInsurance entity)
        {
            _context.HealthInsurance.Update(entity);
        }

        public async Task<HealthInsurance> AddAsync(HealthInsurance entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }
    }
}
