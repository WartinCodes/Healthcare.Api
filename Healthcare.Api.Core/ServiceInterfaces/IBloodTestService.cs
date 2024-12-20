using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IBloodTestService
    {
        Task<IEnumerable<BloodTest>> GetBloodTestsAsync();
        Task<BloodTest> GetBloodTestByIdAsync(int id);
        Task<BloodTest?> GetBloodTestByNameAsync(string name);
        IEnumerable<string> GetAllBloodTestName();
        Task<BloodTest> Add(BloodTest entity);
        void Remove(BloodTest entity);
        Task Edit(BloodTest entity);
    }
}
