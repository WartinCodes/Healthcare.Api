using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;

namespace Healthcare.Api.Repository.Repositories
{
    public class LaboratoryDetailRepository : BaseRepository<LaboratoryDetail>, ILaboratoryDetailsRepository
    {
        private readonly HealthcareDbContext _context;

        public LaboratoryDetailRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<LaboratoryDetail> GetAsQueryable()
        {
            return _context.Hemograma.AsQueryable();
        }

        public async Task<IEnumerable<LaboratoryDetail>> GetAsync()
        {
            return await base.GetAsync().ConfigureAwait(false);
        }

        public void Remove(LaboratoryDetail entity)
        {
            base.Delete(entity.Id);
        }

        public void Edit(LaboratoryDetail entity)
        {
            _context.Hemograma.Update(entity);
        }

        public async Task<LaboratoryDetail> AddAsync(LaboratoryDetail entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }
    }
}
