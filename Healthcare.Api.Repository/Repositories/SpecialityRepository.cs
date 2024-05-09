using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Repositories
{
    public class SpecialityRepository : BaseRepository<Speciality>, ISpecialityRepository
    {
        private readonly HealthcareDbContext _context;

        public SpecialityRepository(HealthcareDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Speciality> GetAsQueryable()
        {
            return _context.Speciality.AsQueryable();
        }

        public async Task<IEnumerable<Speciality>> GetAsync()
        {
            return await base.GetAsync().ConfigureAwait(false);
        }

        public async Task<Speciality> GetSpecialityByIdAsync(int id)
        {
            return await _context.Speciality.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public void Remove(Speciality entity)
        {
            base.Delete(entity.Id);
        }

        public void Edit(Speciality entity)
        {
            _context.Speciality.Update(entity);
        }

        public async Task<Speciality> AddAsync(Speciality entity)
        {
            return await base.InsertAsync(entity).ConfigureAwait(false);
        }

        public async Task<Speciality> GetSpecialityByNameAsync(string name)
        {
            return await _context.Speciality.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
