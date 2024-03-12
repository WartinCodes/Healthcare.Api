using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Context
{
    public interface IHealthcareDbContext
    {
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
        DbSet<Hemograma> LaboratoryDetail { get; set; }
        DbSet<DoctorHealthInsurance> DoctorHealthInsurance { get; set; }
        DbSet<PatientHealthPlan> PatientHealthPlan { get; set; }
    }
}