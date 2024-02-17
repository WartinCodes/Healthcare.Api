using Healthcare.Api.Core.RepositoryInterfaces;

namespace Healthcare.Api.Core.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IPatientRepository PatientRepository { get; }
        IDoctorRepository DoctorRepository { get; }
        ISpecialityRepository SpecialityRepository { get; }

        void Save();
        Task SaveAsync();
    }
}