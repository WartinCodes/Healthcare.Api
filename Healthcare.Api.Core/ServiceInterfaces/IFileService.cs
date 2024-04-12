﻿using System.Net;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IFileService
    {
        Task<HttpStatusCode> InsertPhotoAsync(Stream file, string fileName, string contentType);
        Task<HttpStatusCode> DeletePhotoAsync(string fileName);
        Task<HttpStatusCode> InsertStudyAsync(Stream file, string fileName, string contentType);
        Task<HttpStatusCode> DeleteStudyAsync(string fileName);
    }
}