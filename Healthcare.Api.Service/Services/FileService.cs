using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.Extensions.Options;
using System.Net;

namespace Healthcare.Api.Service.Services
{
    public class FileService : IFileService
    {
        private static IAmazonS3 _awsS3Client;
        private readonly S3Configuration _s3Configuration;
        private readonly ITransferUtility _awsS3TransferUtility;
        private readonly string _photosFolder = "photos";

        public FileService(IAmazonS3 client, IOptions<S3Configuration> s3Configuration, ITransferUtility transferUtility)
        {
            _awsS3Client = client;
            _s3Configuration = s3Configuration?.Value ?? throw new ArgumentNullException(nameof(s3Configuration));
            _awsS3TransferUtility = transferUtility;
        }

        public async Task<HttpStatusCode> InsertPhotoAsync(Stream file, string fileName, string contentType)
        {
            try
            {
                string key = _photosFolder + "/" + fileName;

                TransferUtilityUploadRequest transferUtilityUploadRequest = new TransferUtilityUploadRequest()
                {
                    CannedACL = S3CannedACL.PublicRead,
                    BucketName = _s3Configuration.BucketName,
                    Key = key,
                    InputStream = file
                };

                await _awsS3TransferUtility.UploadAsync(transferUtilityUploadRequest);

                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
    }
}