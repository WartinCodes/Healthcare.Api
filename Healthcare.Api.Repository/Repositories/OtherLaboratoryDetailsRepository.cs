using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;

namespace Healthcare.Api.Repository.Repositories
{
    public class OtherLaboratoryDetailsRepository : BaseRepository<OtherLaboratoryDetail>, IOtherLaboratoryDetailsRepository
    {
        private readonly HealthcareDbContext _context;

        public OtherLaboratoryDetailsRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<OtherLaboratoryDetail> GetAsQueryable()
        {
            return _context.OtherLaboratoryDetail.AsQueryable();
        }

        public async Task<IEnumerable<OtherLaboratoryDetail>> GetAsync()
        {
            return await base.GetAsync().ConfigureAwait(false);
        }

        public void Remove(OtherLaboratoryDetail entity)
        {
            base.Delete(entity.Id);
        }

        public void Edit(OtherLaboratoryDetail entity)
        {
            _context.OtherLaboratoryDetail.Update(entity);
            _context.SaveChanges();
        }

        public async Task<OtherLaboratoryDetail> AddAsync(OtherLaboratoryDetail entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }
    }
}