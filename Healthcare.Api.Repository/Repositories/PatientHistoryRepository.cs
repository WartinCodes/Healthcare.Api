using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class PatientHistoryRepository : BaseRepository<PatientHistory>, IPatientHistoryRepository
    {
        private readonly HealthcareDbContext _context;

        public PatientHistoryRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<PatientHistory> GetAsQueryable()
        {
            return _context.PatientHistory.AsQueryable();
        }

        public async Task<IEnumerable<PatientHistory>> GetAsync()
        {
            return await base.GetAsync();
        }

        public async Task<IEnumerable<PatientHistory>> GetPatientHistoryByUserIdAsync(int userId)
        {
            return await _context.PatientHistory.Where(x => x.Patient.UserId == userId).ToListAsync();
        }

        public void Remove(PatientHistory entity)
        {
            base.Delete(entity.Id);
        }

        public void Edit(PatientHistory entity)
        {
            _context.PatientHistory.Update(entity);
        }

        public async Task<PatientHistory> AddAsync(PatientHistory entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }
    }
}