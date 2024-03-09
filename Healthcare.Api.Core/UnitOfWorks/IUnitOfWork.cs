using Healthcare.Api.Core.RepositoryInterfaces;

namespace Healthcare.Api.Core.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IPatientRepository PatientRepository { get; }
        IDoctorRepository DoctorRepository { get; }
        ISpecialityRepository SpecialityRepository { get; }
        IHealthInsuranceRepository HealthInsuranceRepository { get; }
        IHealthPlanRepository HealthPlanRepository { get; }
        IDoctorSpecialityRepository DoctorSpecialityRepository { get; }
        IAddressRepository AddressRepository { get; }
        ILaboratoryDetailRepository LaboratoryDetailRepository { get; }
        ICityRepository CityRepository { get; }
        ICountryRepository CountryRepository { get; }
        IStateRepository StateRepository { get; }

        void Save();
        Task SaveAsync();
    }
}