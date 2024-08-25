using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IUltrasoundImageRepository : IBaseRepository<UltrasoundImage>
    {
        Task<IEnumerable<UltrasoundImage>> GetUltrasoundImagesByUserId(int userId);
    }
}
