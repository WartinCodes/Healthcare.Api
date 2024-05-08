using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        private readonly HealthcareDbContext _context;

        public CityRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<City> GetAsQueryable()
        {
            return _context.City.AsQueryable();
        }

        public async Task<IEnumerable<City>> GetAsync()
        {
            return await base.GetAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _context.City.AsQueryable()
                .Include(x => x.State)
                .ThenInclude(x => x.Country)
                .ToListAsync();
        }

        public async Task<City> GetCityByIdAsync(int id)
        {
            return await _context.City.Where(x => x.Id == id).AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public Task<City> AddAsync(City entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(City entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(City entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<City>> GetCitiesByStateId(int stateId)
        {
            return await _context.City
                .Where(x => x.IdState == stateId)
                .ToListAsync();
        }
    }
}