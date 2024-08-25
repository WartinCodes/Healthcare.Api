using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IUltrasoundImageService
    {
        Task<UltrasoundImage> Add(UltrasoundImage entity);
        void Remove(UltrasoundImage entity);
    }
}
