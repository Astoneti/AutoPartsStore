using Godel.AutoPartsStore.Common.Models;
using Godel.AutoPartsStore.DAL;
using Godel.AutoPartsStore.DAL.Impl;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;

namespace Godel.AutoPartsStore.Web.Tests
{
    public class RoleRepositoryTests
    {
        [Fact]
        public void TestMethod_GetRoleByName_FromRoleRepository()
        {

            //create inmemory db
            var options = CreateInMemoryOptions();

            //Seed test data
            CreateTestData(options);

            // Arrange
            Role adminRole =null;
            Role userRole = null;

            // Act
            using (var context = new PartsStoreDbContext(options))
            {
                IRoleRepository rep = new RoleRepository(context);
                adminRole = rep.GetRoleByName("admin");
                userRole = rep.GetRoleByName("user");
            }

            // Assert
            
            Assert.Equal("admin", adminRole.Name="admin");
            Assert.Equal("user", userRole.Name="user");
        }

        private DbContextOptions<PartsStoreDbContext> CreateInMemoryOptions()
        {
            DbContextOptionsBuilder<PartsStoreDbContext> builder = new DbContextOptionsBuilder<PartsStoreDbContext>();
            builder.UseInMemoryDatabase("RolesTestData");
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

                var roles = new List<Role>

                {
                 new Role {Name="admin",Users=users},
                 new Role {Name="user",Users=null},
                };
                foreach (Role r in roles)
                {
                    context.Roles.Add(r);
                }
                context.SaveChanges();
            }
        }
    }
}
