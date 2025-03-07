using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        private readonly HealthcareDbContext _context;

        public AddressRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Address> GetAsQueryable()
        {
            return _context.Address.AsQueryable();
        }

        public async Task<IEnumerable<Address>> GetAsync()
        {
            return await _context.Address
                .Include(x => x.City)
                .ThenInclude(x => x.State)
                .ThenInclude(x => x.Country)
                .ToListAsync();
        }

        public void Remove(Address entity)
        {
            _context.Attach(entity.City);
            _context.Attach(entity.City.State);
            _context.Attach(entity.City.State.Country);
            base.Delete(entity.Id);
        }

        public void Edit(Address entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
            _context.Entry(entity.City).State = EntityState.Detached;
            _context.Entry(entity.City.State).State = EntityState.Detached;
            _context.Entry(entity.City.State.Country).State = EntityState.Detached;

            _context.Address.Update(entity);
        }

        public async Task<Address> AddAsync(Address entity)
        {
            _context.Attach(entity.City);
            _context.Attach(entity.City.State);
            _context.Attach(entity.City.State.Country);
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }

        public async Task<Address?> GetByIdAsync(int id)
        {
            return await _context.Address
                .Include(x => x.City)
                .ThenInclude(x => x.State)
                .ThenInclude(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}