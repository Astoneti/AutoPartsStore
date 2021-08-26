using Godel.AutoPartsStore.Common.Models;

namespace Godel.AutoPartsStore.DAL.Interfaces
{
    public interface IUserRepository 
    {
        public User GetUserByEmailAndPassword(string email,string password);
        public User GetUserByEmail(string email);
        public void CreateUser(User users);
    }
}

