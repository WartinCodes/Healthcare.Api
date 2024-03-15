using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Text;

namespace Healthcare.Api.Service.Services
{
    public class RestClientHelper : IRestClientHelper
    {
        private readonly string pathName = "photos";
        private readonly IConfiguration _configuration;
        private readonly S3Configuration _s3Configuration;

        public RestClientHelper(IConfiguration configuration, IOptions<S3Configuration> s3Configuration)
        {
            _configuration = configuration;
            _s3Configuration = s3Configuration?.Value ?? throw new ArgumentNullException(nameof(s3Configuration));
        }

        public async Task<string> GetAsync(string requestUri, string token)
        {
            try
            {
                var req = new RestRequest(requestUri);
                var options = new RestClientOptions();
                options.Authenticator = new JwtAuthenticator($"{token}");

                var client = new RestClient(options);
                var response = client.ExecuteAsync(req);

                return response.Result.Content;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public string GetUrl(string fileName)
        {
            return $"{_configuration.GetValue<string>("AmazonS3Service")}?folderName={pathName}&fileName={fileName}&bucketName={_s3Configuration.BucketName}";
        }

        public async Task<int> InsertAsync(Stream file, string fileName, string contentType)
        {
            var requestUri = $"{_configuration.GetValue<string>("AmazonS3Service")}?folderName={pathName}&fileName={fileName}&bucketName={_s3Configuration.BucketName}";

            string content;
            using (MemoryStream ms = new MemoryStream())
            {
                await file.CopyToAsync(ms).ConfigureAwait(false);
                content = Encoding.ASCII.GetString(ms.ToArray());
            }
            var request = new RestRequest(requestUri, Method.Post);
            var restClient = new RestClient();

            long pos = file.CanSeek ? file.Position : 0L;
            if (pos != 0L)
                file.Seek(0, SeekOrigin.Begin);
            byte[] result = new byte[file.Length];
            file.Read(result, 0, result.Length);
            if (file.CanSeek)
                file.Seek(pos, SeekOrigin.Begin);
            request.AddFile("file", result, fileName, contentType);

            var response = await restClient.ExecuteAsync(request);
            HttpStatusCode statusCode = response.StatusCode;

            return (int)statusCode;
        }
    }
}