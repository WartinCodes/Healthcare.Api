using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;

namespace Healthcare.Api.Repository.Repositories
{
    public class UserRoleRepository : BaseRepository<Role>, IUserRoleRepository
    {
        private readonly HealthcareDbContext _context;

        public UserRoleRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<UserRole> AddAsync(UserRole entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(UserRole entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserRole> GetAsQueryable()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserRole>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public void Remove(UserRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
