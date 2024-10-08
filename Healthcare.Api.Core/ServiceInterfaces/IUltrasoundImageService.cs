﻿using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IUltrasoundImageService
    {
        Task<IEnumerable<UltrasoundImage>> GetUltrasoundImagesByUserIdAsync(int userId);
        Task<IEnumerable<UltrasoundImage>> GetUltrasoundImagesByStudyIdAsync(int studyId);
        Task<UltrasoundImage> Add(UltrasoundImage entity);
        void Remove(UltrasoundImage entity);
    }
}
