using Godel.AutoPartsStore.Common.Models;

namespace Godel.AutoPartsStore.DAL.Impl
{
    public interface IRoleRepository
    {
        public Role GetRoleByName(string name);
    }
}