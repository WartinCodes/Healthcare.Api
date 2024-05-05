using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;

namespace Healthcare.Api.Repository.Repositories
{
    public class SupportRepository : BaseRepository<Support>, ISupportRepository
    {
        private readonly HealthcareDbContext _context;

        public SupportRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Support> AddAsync(Support entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }

        public void Edit(Support entity)
        {
            _context.Support.Update(entity);
        }

        public IQueryable<Support> GetAsQueryable()
        {
            return _context.Support.AsQueryable();
        }

        public async Task<IEnumerable<Support>> GetAsync()
        {
            return await base.GetAsync().ConfigureAwait(false);
        }

        public void Remove(Support entity)
        {
            base.Delete(entity.Id);
        }
    }
}
