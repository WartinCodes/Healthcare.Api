using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class PatientHealthPlanRepository : BaseRepository<PatientHealthPlan>, IPatientHealthPlanRepository
    {
        private readonly HealthcareDbContext _context;

        public PatientHealthPlanRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PatientHealthPlan>> GetHealthPlansByPatientIdAsync(int id)
        {
            return await _context.PatientHealthPlan.Where(x => x.PatientId == id)
                .ToListAsync();
        }

    }
}