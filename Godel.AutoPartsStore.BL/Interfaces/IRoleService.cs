using Godel.AutoPartsStore.Common.Models;


namespace Godel.AutoPartsStore.BL.Interfaces
{
    public interface IRoleService
    {
        public Role GetRoleByName(string name);
    }
}
