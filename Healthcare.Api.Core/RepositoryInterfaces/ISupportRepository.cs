using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface ISupportRepository : IRepository<Support>
    {
        IQueryable<Support> GetAsQueryable();
    }
}
