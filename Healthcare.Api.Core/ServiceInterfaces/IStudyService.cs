﻿using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Utilities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IStudyService
    {
        IQueryable<Study> GetAsQueryable();
        Task<IEnumerable<Study>> GetAsync();
        Task<Study> GetStudyByIdAsync(int id);
        Task<IEnumerable<Study>> GetStudiesByUserId(int userId);
        Task<Study> Add(Study entity);
        void Remove(Study entity);
        void Edit(Study entity);
        string GenerateFileName(FileNameParameters parameters);
    }
}
