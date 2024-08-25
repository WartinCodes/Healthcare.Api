using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class UltrasoundImageRepository : BaseRepository<UltrasoundImage>, IUltrasoundImageRepository
    {
        private readonly HealthcareDbContext _context;

        public UltrasoundImageRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UltrasoundImage>> GetUltrasoundImagesByUserId(int userId)
        {
            return await _context.UltrasoundImage
                .Include(x => x.Study)
                .ThenInclude(x => x.User)
                .Where(x => x.Study.User.Id == userId)
                .ToListAsync();
        }    
    }
}