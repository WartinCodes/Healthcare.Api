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

        public IQueryable<PatientHealthPlan> GetAsQueryable()
        {
            return _context.PatientHealthPlan.AsQueryable();
        }

        public async Task<IEnumerable<PatientHealthPlan>> GetAsync()
        {
            return await base.GetAsync();
        }

        public async Task<PatientHealthPlan> GetPatientHealthPlanByIdAsync(int id)
        {
            return await _context.PatientHealthPlan.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(PatientHealthPlan entity)
        {
            base.Delete(entity.Id);
        }

        public void Edit(PatientHealthPlan entity)
        {
            _context.PatientHealthPlan.Update(entity);
        }

        public async Task<PatientHealthPlan> AddAsync(PatientHealthPlan entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }
    }
}
