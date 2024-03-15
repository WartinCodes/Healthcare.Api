namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IRestClientHelper
    {
        Task<string> GetAsync(string request, string token);
        Task<int> InsertAsync(Stream file, string fileName, string contentType);
        string GetUrl(string fileName);
    }
}
