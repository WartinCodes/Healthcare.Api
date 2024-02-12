﻿using Healthcare.Api.Core.RepositoryInterfaces;

namespace Healthcare.Api.Core.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        void Save();
        Task SaveAsync();
    }
}