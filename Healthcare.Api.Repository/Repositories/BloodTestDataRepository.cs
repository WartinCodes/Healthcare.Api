using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class BloodTestDataRepository : BaseRepository<BloodTestData>, IBloodTestDataRepository
    {
        private readonly HealthcareDbContext _context;

        public BloodTestDataRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BloodTestData> AddAsync(BloodTestData entity)
        {
            _context.Attach(entity.BloodTest);
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }


        public async Task AddRangeAsync(List<BloodTestData> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                throw new ArgumentException("Entities list cannot be null or empty.");
            }

            await base.InsertRangeAsync(entities);
        }

        public void Remove(BloodTestData entity)
        {
            base.Delete(entity.Id);
        }

        public void Edit(BloodTestData entity)
        {
            _context.BloodTestData.Update(entity);
        }

        public IQueryable<BloodTestData> GetAsQueryable()
        {
            return _context.BloodTestData.AsQueryable();
        }

        public async Task<IEnumerable<BloodTestData>> GetAsync()
        {
            return await _context.BloodTestData.Include(x => x.BloodTest).ToListAsync();
        }

        public async Task<IEnumerable<BloodTestData>> GetByStudyIdAsync(int studyId)
        {
            return await _context.BloodTestData
                .Include(x => x.Study)
                .Include(x => x.BloodTest)
                .ThenInclude(x => x.Unit)
                .Where(x => x.IdStudy == studyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<BloodTestData>> GetBloodTestDatasByStudyIdsAsync(int[] studiesIds)
        {
            return await _context.BloodTestData
                .Include(x => x.Study)
                .Include(x => x.BloodTest)
                .ThenInclude(x => x.Unit)
                .Where(b => studiesIds.Contains(b.IdStudy))
                .ToListAsync();
        }
    }
}