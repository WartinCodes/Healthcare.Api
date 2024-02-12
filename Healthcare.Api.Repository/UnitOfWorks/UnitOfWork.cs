using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.UnitOfWorks;
using Healthcare.Api.Repository.Context;

namespace Healthcare.Api.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HealthcareDbContext _context;

        public UnitOfWork(HealthcareDbContext context,
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _context = context;
            UserRepository = userRepository;
            RoleRepository = roleRepository;
        }

        public IUserRepository UserRepository { get; }

        public IRoleRepository RoleRepository { get; }


        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}