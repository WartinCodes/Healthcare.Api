﻿using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Context
{
    public interface IHealthcareDbContext
    {
        DbSet<BloodTest> BloodTest { get; set; }
        DbSet<BloodTestData> BloodTestData { get; set; }
        DbSet<Patient> Patient { get; set; }
        DbSet<Doctor> Doctor { get; set; }
        DbSet<Speciality> Speciality { get; set; }
        DbSet<HealthInsurance> HealthInsurance { get; set; }
        DbSet<HealthPlan> HealthPlan { get; set; }
        DbSet<DoctorSpeciality> DoctorSpeciality { get; set; }
        DbSet<Address> Address { get; set; }
        DbSet<City> City { get; set; }
        DbSet<State> State { get; set; }
        DbSet<Country> Country { get; set; }
        DbSet<DoctorHealthInsurance> DoctorHealthInsurance { get; set; }
        DbSet<PatientHealthPlan> PatientHealthPlan { get; set; }
        DbSet<Study> Study { get; set; }
        DbSet<StudyType> StudyType { get; set; }
        DbSet<Support> Support { get; set; }
        DbSet<PatientHistory> PatientHistory { get; set; }
        DbSet<UltrasoundImage> UltrasoundImage { get; set; }
        DbSet<NutritionData> NutritionData { get; set; }
    }
}