using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IBloodTestRepository : IRepository<BloodTest>
    {
        Task<BloodTest> GetBloodTestByIdAsync(int id);
        Task<BloodTest?> GetBloodTestByNameAsync(string name);
    }
}
