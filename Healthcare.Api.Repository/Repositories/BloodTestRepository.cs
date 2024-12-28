using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class BloodTestRepository : BaseRepository<BloodTest>, IBloodTestRepository
    {
        private readonly HealthcareDbContext _context;

        public BloodTestRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<BloodTest> GetAsQueryable()
        {
            return _context.BloodTest.AsQueryable();
        }

        public async Task<IEnumerable<BloodTest>> GetAsync()
        {
            return await _context.BloodTest.Include(x => x.Unit).ToListAsync();
        }

        public async Task<BloodTest> GetBloodTestByIdAsync(int id)
        {
            return await _context.BloodTest.Include(x => x.Unit).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BloodTest?> GetBloodTestByNameAsync(string name)
        {
            return await _context.BloodTest.Include(x => x.Unit).FirstOrDefaultAsync(x => x.ParsedName == name);
        }

        public async Task<BloodTest> AddAsync(BloodTest entity)
        {
            _context.Attach(entity.Unit);
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }

        public void Remove(BloodTest entity)
        {
            base.Delete(entity.Id);
        }

        public void Edit(BloodTest entity)
        {
            _context.BloodTest.Update(entity);
        }

        public async Task<BloodTest> AddRangeAsync(BloodTest entity)
        {
            throw new NotImplementedException();
        }
    }
}