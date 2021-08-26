using Godel.AutoPartsStore.Common.Models;
using Godel.AutoPartsStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace Godel.AutoPartsStore.DAL.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly PartsStoreDbContext context;

        public UserRepository(PartsStoreDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            return context.Users.Include(x => x.Role).FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void CreateUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
