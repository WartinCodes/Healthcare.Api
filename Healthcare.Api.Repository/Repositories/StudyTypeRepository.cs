using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class StudyTypeRepository : BaseRepository<StudyType>, IStudyTypeRepository
    {
        private readonly HealthcareDbContext _context;

        public StudyTypeRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<StudyType> AddAsync(StudyType entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }

        public void Edit(StudyType entity)
        {
            _context.StudyType.Update(entity);
        }

        public IQueryable<StudyType> GetAsQueryable()
        {
            return _context.StudyType.AsQueryable();
        }

        public async Task<IEnumerable<StudyType>> GetAsync()
        {
            return await base.GetAsync().ConfigureAwait(false);
        }

        public async Task<StudyType> GetStudyTypeByIdAsync(int id)
        {
            return await _context.StudyType.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(StudyType entity)
        {
            base.Delete(entity.Id);
        }
    }
}