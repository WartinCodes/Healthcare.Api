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
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public void Edit(User entity)
        {
            _context.Users.Update(entity);
        }

        public async Task<User> FindUserByEmailOrDni(string email, string dni)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email || x.NationalIdentityDocument == dni);
        }

        public IQueryable<User> GetAsQueryable()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await base.GetAsync().ConfigureAwait(false);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await base.GetByIdAsync(id);
            return user;
        }

        public void Remove(User entity)
        {
            base.Delete(entity.Id);
        }

        public async Task<Boolean> ValidateUserCredentials(string user, string password)
        {
            var validCredentials = await _context.Users.FirstOrDefaultAsync(x => x.NationalIdentityDocument == user && x.PasswordHash == password);
            if (validCredentials != null) { return true; }
            return false;
        }
    }
}
