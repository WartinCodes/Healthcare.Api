using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class UnitRepository : BaseRepository<Unit>, IUnitRepository
    {
        private readonly HealthcareDbContext _context;

        public UnitRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Unit> AddAsync(Unit entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }

        public IQueryable<Unit> GetAsQueryable()
        {
            return _context.Unit.AsQueryable();
        }

        public async Task<IEnumerable<Unit>> GetAsync()
        {
            return _context.Unit.AsQueryable();
        }

        public void Remove(Unit entity)
        {
            base.Delete(entity.Id);
        }

        public void Edit(Unit entity)
        {
            _context.Unit.Update(entity);
        }

        public async Task<Unit> GetUnitByIdAsync(int id)
        {
            return await _context.Unit.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Unit?> GetUnitByNameOrShortNameAsync(string name, string shortName)
        {
            return await _context.Unit.FirstOrDefaultAsync(x => x.Name == name || x.ShortName == shortName);
        }
    }
}
