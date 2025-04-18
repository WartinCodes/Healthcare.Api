﻿using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address?> GetByIdAsync(int id);
    }
}
