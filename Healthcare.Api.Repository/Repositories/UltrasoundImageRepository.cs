using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;

namespace Healthcare.Api.Repository.Repositories
{
    public class UltrasoundImageRepository : BaseRepository<UltrasoundImage>, IUltrasoundImageRepository
    {
        private readonly HealthcareDbContext _context;

        public UltrasoundImageRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }
    }
}