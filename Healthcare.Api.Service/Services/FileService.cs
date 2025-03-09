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
        private readonly string _studiesFolder = "studies";

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

        public async Task<HttpStatusCode> DeletePhotoAsync(string fileName)
        {
            try
            {
                string key = _photosFolder + "/" + fileName;

                var result = await _awsS3Client.DeleteObjectAsync(new DeleteObjectRequest
                {
                    BucketName = _s3Configuration.BucketName,
                    Key = key
                });

                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HttpStatusCode> InsertFileStudyAsync(Stream file, string dni, string fileName)
        {
            try
            {
                string key = _studiesFolder + "/" + dni + "/" + fileName;

                TransferUtilityUploadRequest transferUtilityUploadRequest = new TransferUtilityUploadRequest()
                {
                    CannedACL = S3CannedACL.Private,
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

        public async Task<HttpStatusCode> InsertDoctorFileAsync(Stream file, string subFolder, string fileName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No se ha enviado ningún archivo o el archivo está vacío.");

            if (!IsValidImageExtension(fileName))
                throw new ArgumentException("Formato de imagen no válido. Solo se permiten archivos .png, .jpg o .jpeg.");

            try
            {
                string key = $"{_photosFolder}/{subFolder}/{fileName}";

                TransferUtilityUploadRequest transferUtilityUploadRequest = new TransferUtilityUploadRequest()
                {
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

        public async Task<HttpStatusCode> DeleteStudyAsync(string fileName)
        {
            try
            {
                string key = _studiesFolder + "/" + fileName;

                var result = await _awsS3Client.DeleteObjectAsync(new DeleteObjectRequest
                {
                    BucketName = _s3Configuration.BucketName,
                    Key = key
                });

                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetSignedUrl(string rootFolder, string userNameFolder, string fileName, double expiryHours = 1)
        {
            string key = $"{rootFolder}/{userNameFolder}/{fileName}";

            var request = new GetPreSignedUrlRequest
            {
                BucketName = _s3Configuration.BucketName,
                Key = key,
                Expires = DateTime.UtcNow.AddHours(expiryHours),
                Verb = HttpVerb.GET
            };

            return _awsS3Client.GetPreSignedURL(request);
        }

        private bool IsValidImageExtension(string fileName)
        {
            var permittedExtensions = new[] { ".png", ".jpg", ".jpeg" };
            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            return !string.IsNullOrEmpty(extension) && permittedExtensions.Contains(extension);
        }
    }
}