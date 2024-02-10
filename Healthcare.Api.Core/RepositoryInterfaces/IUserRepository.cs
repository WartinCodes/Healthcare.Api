﻿using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.RepositoryInterfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindUserByEmailOrDni(string email, string dni);
        Task ValidateUser(string user, string password);
    }
}
