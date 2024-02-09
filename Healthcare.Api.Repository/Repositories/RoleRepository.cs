using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;

namespace Healthcare.Api.Repository.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private readonly HealthcareDbContext _context;

        public RoleRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<Role> AddAsync(Role entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Role entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Role> GetAsQueryable()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public void Remove(Role entity)
        {
            throw new NotImplementedException();
        }
    }
}