using Healthcare.Api.Core.RepositoryInterfaces;

namespace Healthcare.Api.Core.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IBloodTestRepository BloodTestRepository { get; }
        IBloodTestDataRepository BloodTestDataRepository { get; }
        IDoctorRepository DoctorRepository { get; }
        IDoctorHealthInsuranceRepository DoctorHealthInsuranceRepository { get; }
        IDoctorSpecialityRepository DoctorSpecialityRepository { get; }
        ISpecialityRepository SpecialityRepository { get; }
        IHealthInsuranceRepository HealthInsuranceRepository { get; }
        IHealthPlanRepository HealthPlanRepository { get; }
        IAddressRepository AddressRepository { get; }
        ICityRepository CityRepository { get; }
        ICountryRepository CountryRepository { get; }
        IStateRepository StateRepository { get; }
        IPatientRepository PatientRepository { get; }
        IPatientHealthPlanRepository PatientHealthPlanRepository { get; }
        IPatientHistoryRepository PatientHistoryRepository { get; }
        IStudyRepository StudyRepository { get; }
        IStudyTypeRepository StudyTypeRepository { get; }
        ISupportRepository SupportRepository { get; }
        IUltrasoundImageRepository UltrasoundImageRepository { get; }
        IUnitRepository UnitRepository { get; }

        void Save();
        Task SaveAsync();
    }
}