using Godel.AutoPartsStore.Common.Models;
using Godel.AutoPartsStore.DAL;
using Godel.AutoPartsStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Godel.AutoPartsStore.Web.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void TestAllMethods_In_UserServise()
        {
            //create inmemory db
            var options = CreateInMemoryOptions();
            var users = GetTestUsers();
            //Seed test data
            //CreateTestData(options);

            // Arrange

            var newUser = new User();
            var userByEmail = new User();
            var userByEmailAndpassword = new User();

            // Act
            using (var context = new PartsStoreDbContext(options))
            {
                var mockUserService = new Mock<IUnitOfWork>(context);
                var user = new  User();
                mockUserService.Setup(rep=>rep.Users.CreateUser(user)).Returns(GetTestUsers());
                mockUserService.Setup(rep => rep.Users.GetUserByEmail("a.pashkun@godeltech.com"));
                mockUserService.Setup(rep => rep.Users.GetUserByEmailAndPassword("a.pashkun@godeltech.com", "12345678"));
            }

            // Assert
            Assert.Equal(0, newUser.Id);
            Assert.Equal("a.pashkun@godeltech.com", userByEmail.Email);
            Assert.Equal("a.pashkun@godeltech.com", userByEmailAndpassword.Email);
            Assert.Equal("12345678", userByEmailAndpassword.Password);
        }

        private DbContextOptions<PartsStoreDbContext> CreateInMemoryOptions()
        {
            DbContextOptionsBuilder<PartsStoreDbContext> builder = new DbContextOptionsBuilder<PartsStoreDbContext>();
            builder.UseInMemoryDatabase("UsersTestData");
            return builder.Options;
        }

        public Task <User> GetTestUsers() 
        {
            var users = new List<User>
            {
                 new User {Email ="a.pashkun@godeltech.com",Password = "12345678"},
            };
            return users;
        }
       
    }
}
