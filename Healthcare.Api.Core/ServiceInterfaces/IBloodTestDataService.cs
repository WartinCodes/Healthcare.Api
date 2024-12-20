using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IBloodTestDataService
    {
        Task<IEnumerable<BloodTestData>> GetBloodTestDataAsync();
        Task<BloodTestData> GetBloodTestDataByIdAsync(int id);
        Task<BloodTestData> Add(BloodTestData entity);
        Task AddRangeAsync(List<BloodTestData> entities);
        void Remove(BloodTestData entity);
        Task Edit(BloodTestData entity);
    }
}
 