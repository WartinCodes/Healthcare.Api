using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class DoctorHealthPlanRepository : BaseRepository<DoctorHealthPlan>, IDoctorHealthPlanRepository
    {
        private readonly HealthcareDbContext _context;

        public DoctorHealthPlanRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<DoctorHealthPlan> GetAsQueryable()
        {
            return _context.DoctorHealthPlan.AsQueryable();
        }

        public async Task<IEnumerable<DoctorHealthPlan>> GetAsync()
        {
            return await base.GetAsync();
        }

        public async Task<DoctorHealthPlan> GetDoctorHealthPlanByIdAsync(int id)
        {
            return await _context.DoctorHealthPlan.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(DoctorHealthPlan entity)
        {
            base.Delete(entity.Id);
        }

        public void Edit(DoctorHealthPlan entity)
        {
            _context.DoctorHealthPlan.Update(entity);
        }

        public async Task<DoctorHealthPlan> AddAsync(DoctorHealthPlan entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }
    }
}