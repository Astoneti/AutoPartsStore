using Godel.AutoPartsStore.BL.Interfaces;
using Godel.AutoPartsStore.Common.Models;
using Godel.AutoPartsStore.DAL.Interfaces;

namespace Godel.AutoPartsStore.BL.Servises
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public User GetUserByEmailAndPassword(string email,string password )
        {
            return _unitOfWork.Users.GetUserByEmailAndPassword(email,password);
        }

        public User GetUserByEmail(string email)
        {
            return _unitOfWork.Users.GetUserByEmail(email);
        }

        public void CreateUser(User user)
        {
            _unitOfWork.Users.CreateUser(user);
        }
    }
}
