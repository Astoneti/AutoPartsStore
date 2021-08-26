using System;
using Microsoft.EntityFrameworkCore;
using Godel.AutoPartsStore.DAL.Interfaces;
using Godel.AutoPartsStore.DAL.Repositories;
using Godel.AutoPartsStore.DAL.Models;
using Godel.AutoPartsStore.Common.Models;
using Godel.AutoPartsStore.DAL.Impl;

namespace Godel.AutoPartsStore.DAL.Empl.UnitOfWork
{
    public class UnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        private PartsStoreDbContext db;
        private PartRepository partRepository;
        private CategoryRepository categoryRepository;
        private UserRepository userRepository;
        private RoleRepository roleRepository;
        public UnitOfWork(string connectionString)
        {
            DbContextOptionsBuilder<PartsStoreDbContext> builder = new DbContextOptionsBuilder<PartsStoreDbContext>();
            builder.UseSqlServer(connectionString);
            db = new PartsStoreDbContext(builder.Options);
        }
        public IPartRepository Parts
        {
            get
            {
                if (partRepository == null)
                    partRepository = new PartRepository(db);
                return partRepository;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(db);
                }
                return categoryRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }
                return userRepository;
            }
        }

        public IRoleRepository Roles
        {
            get
            {
                if (roleRepository == null)
                {
                    roleRepository = new RoleRepository(db);
                }
                return roleRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
