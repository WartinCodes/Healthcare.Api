using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

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
            return _context.LaboratoryDetail.AsQueryable();
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
            _context.LaboratoryDetail.Update(entity);
            _context.SaveChanges();
        }

        public async Task<LaboratoryDetail> AddAsync(LaboratoryDetail entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }

        public async Task<IEnumerable<LaboratoryDetail>> GetLaboratoriesByUserId(int userId)
        {
            return await _context.LaboratoryDetail
                .Include(x => x.Study)
                .ThenInclude(x => x.User)
                .Where(x => x.Study.User.Id == userId)
                .OrderBy(x => x.Study.Date)
                .ToListAsync();
        }

        public async Task<LaboratoryDetail> GetLaboratoriesByStudyId(int studyId)
        {
            var laboratoryDetail = await _context.LaboratoryDetail.SingleOrDefaultAsync(x => x.IdStudy == studyId);

            if (laboratoryDetail == null)
            {
                return new LaboratoryDetail();
            }

            return laboratoryDetail;
        }
    }
}
