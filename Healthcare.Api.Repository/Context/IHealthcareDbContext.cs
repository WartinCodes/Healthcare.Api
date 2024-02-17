using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Repository.Context
{
    public interface IHealthcareDbContext
    {
        DbSet<Patient> Patient { get; set; }
        DbSet<Doctor> Doctor { get; set; }
        DbSet<Speciality> Speciality { get; set; }
    }
}