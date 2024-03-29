﻿using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.UnitOfWorks;
using Healthcare.Api.Repository.Context;

namespace Healthcare.Api.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HealthcareDbContext _context;

        public UnitOfWork(
            HealthcareDbContext context,
            IPatientRepository patientRepository,
            IDoctorRepository doctorRepository,
            ISpecialityRepository specialityRepository,
            IHealthInsuranceRepository healthInsuranceRepository,
            IHealthPlanRepository healthPlanRepository,
            IDoctorSpecialityRepository doctorSpecialityRepository,
            IAddressRepository addressRepository)
        {
            _context = context;
            PatientRepository = patientRepository;
            DoctorRepository = doctorRepository;
            SpecialityRepository = specialityRepository;
            HealthInsuranceRepository = healthInsuranceRepository;
            HealthPlanRepository = healthPlanRepository;
            DoctorSpecialityRepository = doctorSpecialityRepository;
            AddressRepository = addressRepository;
        }

        public ISpecialityRepository SpecialityRepository { get; }
        public IPatientRepository PatientRepository { get; }
        public IDoctorRepository DoctorRepository { get; }
        public IHealthInsuranceRepository HealthInsuranceRepository { get; }
        public IHealthPlanRepository HealthPlanRepository { get; }
        public IDoctorSpecialityRepository DoctorSpecialityRepository { get; }
        public IAddressRepository AddressRepository { get; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}