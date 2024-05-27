using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IPatientHistoryRepository : IRepository<PatientHistory>
    {
        Task<IEnumerable<PatientHistory>> GetPatientHistoryByUserIdAsync(int userId);
    }
}