using Godel.AutoPartsStore.Common.Models;
using Godel.AutoPartsStore.DAL;
using Godel.AutoPartsStore.DAL.Impl;
using Godel.AutoPartsStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Godel.AutoPartsStore.Web.Tests
{
    public class UserRepositoryTests
    {
        [Fact]
        public void TestAllMethods_In_UserRepository()
        {

            //create inmemory db
            var options = CreateInMemoryOptions();
           
            //Seed test data
            CreateTestData(options);

            // Arrange
           
            Task newUser = null;
            User userByEmail = null;
            User userByEmailAndpassword = null;

            // Act
            using (var context = new PartsStoreDbContext(options))
            {
                IUserRepository rep = new UserRepository(context);
                var users = new User();
                newUser = rep.CreateUser(users);
                userByEmail = rep.GetUserByEmail("a.pashkun@godeltech.com");
                userByEmailAndpassword = rep.GetUserByEmailAndPassword("a.pashkun@godeltech.com", "12345678");
            }

            // Assert
            Assert.Equal(1, newUser.Id);
            Assert.Equal("a.pashkun@godeltech.com", userByEmail.Email);
            Assert.Equal("a.pashkun@godeltech.com", userByEmailAndpassword.Email);
            Assert.Equal("12345678",userByEmailAndpassword.Password);
        }

        private DbContextOptions<PartsStoreDbContext> CreateInMemoryOptions()
        {
            DbContextOptionsBuilder<PartsStoreDbContext> builder = new DbContextOptionsBuilder<PartsStoreDbContext>();
            builder.UseInMemoryDatabase("UsersTestData");
            return builder.Options;
        }

        private void CreateTestData(DbContextOptions<PartsStoreDbContext> options)
        {
            using (var context = new PartsStoreDbContext(options))
            {
                var users = new List<User>
            {
                 new User { Email ="a.pashkun@godeltech.com",Password = "12345678"},
            };

                foreach (User u in users)
                {
                    context.Users.Add(u);
                }
                context.SaveChanges();
            }
        }
    }
}
