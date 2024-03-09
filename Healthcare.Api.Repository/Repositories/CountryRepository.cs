using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        private readonly HealthcareDbContext _context;

        public CountryRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<Country> AddAsync(Country entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Country entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Country> GetAsQueryable()
        {
            return _context.Country.AsQueryable();
        }

        public async Task<IEnumerable<Country>> GetAsync()
        {
            return await base.GetAsync().ConfigureAwait(false);
        }

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            return await _context.Country.Where(x => x.Id == id).AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public void Remove(Country entity)
        {
            throw new NotImplementedException();
        }
    }
}
