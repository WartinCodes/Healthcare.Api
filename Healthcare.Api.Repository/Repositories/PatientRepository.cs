﻿using Healthcare.Api.Core.Entities;
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
            return await _context.Patient
                .Include(x => x.User)
                .Include(x => x.HealthPlans)
                .ThenInclude(x => x.HealthInsurance)
                .Include(x => x.Address)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.State)
                .ThenInclude(x => x.Country)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _context.Patient.Where(x => x.Id == id)
                .Include(x => x.User)
                .Include(x => x.HealthPlans)
                .ThenInclude(x => x.HealthInsurance)
                .Include(x => x.Address)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.State)
                .ThenInclude(x => x.Country)
                .AsNoTracking()
                .FirstOrDefaultAsync();
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
