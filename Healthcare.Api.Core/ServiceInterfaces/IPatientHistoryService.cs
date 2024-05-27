using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IPatientHistoryService
    {
        Task<IEnumerable<PatientHistory>> GetPatientHistoryByUserIdAsync(int userId);
    }
}
