using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly HealthcareDbContext _context;

        public UserRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindUserByEmailOrDni(string email, string dni)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email || x.NationalIdentityDocument == dni);
        }

        public IQueryable<User> GetAsQueryable()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public void Remove(User entity)
        {
            throw new NotImplementedException();
        }

        public Task ValidateUser(string user, string password)
        {
            throw new NotImplementedException();
        }
    }
}
