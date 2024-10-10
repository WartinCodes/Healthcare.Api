using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class StudyRepository : BaseRepository<Study>, IStudyRepository
    {
        private readonly HealthcareDbContext _context;

        public StudyRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Study> AddAsync(Study entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }

        public void Edit(Study entity)
        {
            _context.Study.Update(entity);
        }

        public IQueryable<Study> GetAsQueryable()
        {
            return _context.Study.AsQueryable();
        }

        public async Task<IEnumerable<Study>> GetAsync()
        {
            return await base.GetAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Study>> GetStudiesByUserId(int userId)
        {
            return await _context.Study
                .Include(x => x.StudyType)
                .Include(x => x.User)
                .Where(x => x.User.Id == userId)
                .OrderBy(x => x.Date)
                .ToListAsync();
        }

        public async Task<Study> GetStudyByIdAsync(int id)
        {
            return await _context.Study
                .Include(x => x.StudyType)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(Study entity)
        {
            base.Delete(entity.Id);
        }
    }
}