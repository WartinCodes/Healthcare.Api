using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;

namespace Healthcare.Api.Repository.Repositories
{
    public class SupportRepository : BaseRepository<Patient>, ISupportRepository
    {
        private readonly HealthcareDbContext _context;

        public SupportRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Support> GetAsQueryable()
        {
            return _context.Support.AsQueryable();
        }
    }
}
