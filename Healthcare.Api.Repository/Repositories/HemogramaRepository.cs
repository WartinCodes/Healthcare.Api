using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;

namespace Healthcare.Api.Repository.Repositories
{
    public class HemogramaRepository : BaseRepository<Hemograma>, IHemogramaRepository
    {
        private readonly HealthcareDbContext _context;

        public HemogramaRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Hemograma> GetAsQueryable()
        {
            return _context.Hemograma.AsQueryable();
        }

        public async Task<IEnumerable<Hemograma>> GetAsync()
        {
            return await base.GetAsync().ConfigureAwait(false);
        }

        public void Remove(Hemograma entity)
        {
            base.Delete(entity.Id);
        }

        public void Edit(Hemograma entity)
        {
            _context.Hemograma.Update(entity);
        }

        public async Task<Hemograma> AddAsync(Hemograma entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }
    }
}
