using Godel.AutoPartsStore.Common.Models;

namespace Godel.AutoPartsStore.BL.Interfaces
{
    public interface IUserService
    {
        public User GetUserByEmailAndPassword(string email, string password);
        public User GetUserByEmail(string email);
        public void CreateUser(User user); 
    }
}
