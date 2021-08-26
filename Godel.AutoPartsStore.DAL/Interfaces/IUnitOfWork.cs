using System;
using Godel.AutoPartsStore.DAL.Impl;
using Godel.AutoPartsStore.DAL.Models;

namespace Godel.AutoPartsStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPartRepository Parts { get; }
        IRepository<Category> Categories { get; }
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        void Save();
    }
}
