using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;

namespace Healthcare.Api.Core.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IDoctorRepository DoctorRepository { get; }
        IDoctorHealthInsuranceRepository DoctorHealthInsuranceRepository { get; }
        IDoctorSpecialityRepository DoctorSpecialityRepository { get; }
        ISpecialityRepository SpecialityRepository { get; }
        IHealthInsuranceRepository HealthInsuranceRepository { get; }
        IHealthPlanRepository HealthPlanRepository { get; }
        IAddressRepository AddressRepository { get; }
        IHemogramaRepository HemogramaRepository { get; }
        ICityRepository CityRepository { get; }
        ICountryRepository CountryRepository { get; }
        IStateRepository StateRepository { get; }
        IPatientRepository PatientRepository { get; }
        IPatientHealthPlanRepository PatientHealthPlanRepository { get; }

        void Save();
        Task SaveAsync();
    }
}