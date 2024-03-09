using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        private readonly HealthcareDbContext _context;

        public StateRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<State> GetAsQueryable()
        {
            return _context.State.AsQueryable();
        }

        public async Task<IEnumerable<State>> GetAsync()
        {
            return await base.GetAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<State>> GetAllStateAsync()
        {
            return await _context.State.AsQueryable()
                .Include(x => x.Country)
                .ToListAsync();
        }

        public async Task<IEnumerable<State>> GetStatesByCountryId(int countryId)
        {
            return await _context.State
                .Where(x => x.IdCountry == countryId)
                .ToListAsync();
        }

        public Task<State> AddAsync(State entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(State entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(State entity)
        {
            throw new NotImplementedException();
        }
    }
}