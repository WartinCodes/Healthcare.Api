using Healthcare.Api.Core.Entities;

public class Doctor
{
    public Doctor()
    {
        Specialities = new HashSet<Speciality>();
        HealthInsurances = new HashSet<HealthInsurance>();
        SignedStudies = new HashSet<Study>();
    }

    public Doctor(int userId, ICollection<Speciality> doctorSpecialities, ICollection<HealthInsurance> healthInsurances)
    {
        UserId = userId;
        Specialities = doctorSpecialities ?? new HashSet<Speciality>();
        HealthInsurances = healthInsurances ?? new HashSet<HealthInsurance>();
        SignedStudies = new HashSet<Study>();
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public string Matricula { get; set; }
    public string? Firma { get; set; }
    public string? Sello { get; set; }
    public ICollection<Speciality> Specialities { get; set; }
    public ICollection<HealthInsurance> HealthInsurances { get; set; }
    public ICollection<Study> SignedStudies { get; set; }
}
