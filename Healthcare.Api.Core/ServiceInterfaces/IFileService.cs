using System.Net;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IFileService
    {
        Task<HttpStatusCode> InsertPhotoAsync(Stream file, string fileName, string contentType);
        Task<HttpStatusCode> DeletePhotoAsync(string fileName);
        Task<HttpStatusCode> InsertFileStudyAsync(Stream file, string dni, string fileName);
        Task<HttpStatusCode> DeleteStudyAsync(string fileName);
        string GetUrl(string userNameFolder, string fileName);
    }
}
