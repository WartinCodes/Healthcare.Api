using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        private readonly HealthcareDbContext _context;

        public PatientRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Patient> GetAsQueryable()
        {
            return _context.Patient.AsQueryable();
        }

        public async Task<IEnumerable<Patient>> GetAsync()
        {
            return await base.GetAsync().ConfigureAwait(false);
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _context.Patient.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public void Remove(Patient entity)
        {
            base.Delete(entity.Id);
        }

        public void Edit(Patient entity)
        {
            _context.Patient.Update(entity);
        }

        public async Task<Patient> AddAsync(Patient entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }
    }
}
