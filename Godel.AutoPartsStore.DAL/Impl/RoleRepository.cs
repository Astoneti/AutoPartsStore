using Godel.AutoPartsStore.Common.Models;
using System;
using System.Linq;

namespace Godel.AutoPartsStore.DAL.Impl
{
    public class RoleRepository : IRoleRepository
    {
        private readonly PartsStoreDbContext context;

        public RoleRepository(PartsStoreDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Role GetRoleByName(string name) 
        { 
           return context.Roles.FirstOrDefault(r => r.Name == name);
        }
    }
}
