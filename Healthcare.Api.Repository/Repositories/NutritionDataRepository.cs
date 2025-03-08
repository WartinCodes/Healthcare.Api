using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class NutritionDataRepository : BaseRepository<NutritionData>, INutritionDataRepository
    {
        private readonly HealthcareDbContext _context;

        public NutritionDataRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<NutritionData> AddAsync(NutritionData entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }

        public void Edit(NutritionData entity)
        {
            _context.NutritionData.Update(entity);
        }

        public IQueryable<NutritionData> GetAsQueryable()
        {
            return _context.NutritionData.AsQueryable();
        }

        public async Task<IEnumerable<NutritionData>> GetAsync()
        {
            return await _context.NutritionData.ToListAsync().ConfigureAwait(false);
        }

        public async Task<NutritionData?> GetByIdAsync(int id)
        {
            return await _context.NutritionData.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<NutritionData>> GetNutritionDatasByPatient(int patientId)
        {
            return await _context.NutritionData
                .Where(x => x.PatientId == patientId)
                .OrderBy(x => x.Date)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public void Remove(NutritionData entity)
        {
            base.Delete(entity.Id);
        }
    }
}
