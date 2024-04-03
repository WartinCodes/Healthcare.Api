using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IStudyService
    {
        IQueryable<Study> GetAsQueryable();
        Task<IEnumerable<Study>> GetAsync();
        Task<Study> GetStudyByIdAsync(int id);
        Task<Study> Add(Study entity);
        void Remove(Study entity);
        void Edit(Study entity);
    }
}
