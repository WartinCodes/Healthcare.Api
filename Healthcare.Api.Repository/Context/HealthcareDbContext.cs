﻿using Healthcare.Api.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Healthcare.Api.Repository.Context
{
    public class HealthcareDbContext : IdentityDbContext<User, Role, int>, IHealthcareDbContext
    {
        private readonly IConfiguration _configuration;
        public const string _Schema = "Healthcare";

        public DbSet<Address> Address { get; set; }
        public DbSet<BloodTest> BloodTest { get; set; }
        public DbSet<BloodTestData> BloodTestData { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<PatientHealthPlan> PatientHealthPlan { get; set; }
        public DbSet<PatientHistory> PatientHistory { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<DoctorSpeciality> DoctorSpeciality { get; set; }
        public DbSet<DoctorHealthInsurance> DoctorHealthInsurance { get; set; }
        public DbSet<HealthInsurance> HealthInsurance { get; set; }
        public DbSet<HealthPlan> HealthPlan { get ; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Speciality> Speciality { get; set; }
        public DbSet<Study> Study { get; set; }
        public DbSet<StudyType> StudyType { get; set; }
        public DbSet<Support> Support { get; set; }
        public DbSet<UltrasoundImage> UltrasoundImage { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<NutritionData> NutritionData { get; set; }

        public HealthcareDbContext(IConfiguration configuration) : base()
        {
            this._configuration = configuration;
        }

        public HealthcareDbContext(IConfiguration configuration, DbContextOptions<HealthcareDbContext> options) : base(options)
        {
            this._configuration = configuration;
        }

        public HealthcareDbContext(DbContextOptions<HealthcareDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(_Schema);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
