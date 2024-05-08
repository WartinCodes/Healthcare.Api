using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class DoctorHealthInsuranceRepository : BaseRepository<DoctorHealthInsurance>, IDoctorHealthInsuranceRepository
    {
        private readonly HealthcareDbContext _context;

        public DoctorHealthInsuranceRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<DoctorHealthInsurance> GetAsQueryable()
        {
            return _context.DoctorHealthInsurance.AsQueryable();
        }

        public async Task<IEnumerable<DoctorHealthInsurance>> GetAsync()
        {
            return await base.GetAsync();
        }

        public async Task<DoctorHealthInsurance> GetDoctorHealthPlanByIdAsync(int id)
        {
            return await _context.DoctorHealthInsurance.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(DoctorHealthInsurance entity)
        {
            base.Delete(entity.Id);
        }

        public void Edit(DoctorHealthInsurance entity)
        {
            _context.DoctorHealthInsurance.Update(entity);
        }

        public async Task<DoctorHealthInsurance> AddAsync(DoctorHealthInsurance entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }

        public async Task<IEnumerable<DoctorHealthInsurance>> GetHealthPlansByDoctorIdAsync(int doctorId)
        {
            return await _context.DoctorHealthInsurance.Where(x => x.DoctorId == doctorId)
                .ToListAsync();
        }
    }
}