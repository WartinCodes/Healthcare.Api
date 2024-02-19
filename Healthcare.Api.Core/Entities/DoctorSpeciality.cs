namespace Healthcare.Api.Core.Entities
{
    public class DoctorSpeciality
    {
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public int SpecialityId { get; set; }
        public virtual Speciality Speciality { get; set; }
    }
}
