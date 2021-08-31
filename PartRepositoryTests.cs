using Godel.AutoPartsStore.Common.Models;
using Godel.AutoPartsStore.DAL;
using Godel.AutoPartsStore.DAL.Interfaces;
using Godel.AutoPartsStore.DAL.Models;
using Godel.AutoPartsStore.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Godel.AutoPartsStore.Web.Tests
{
    public class PartRepositoryTests
    {
        [Fact]
        public void GetFilteredListFromPartRepository()
        {

            //create inmemory db
            var options = CreateInMemoryOptions();
            
            //Seed test data
            CreateTestData(options);

            // Arrange
            PaginatedList<Part> searchResult = null;
            PaginatedList<Part> categoryResult = null;
            PaginatedList<Part> pagingResult = null;
            Part getAllResult = null;
            Part getResult = null;
            Task deleteResult = null;
            Task createResult = null;
            Task updateResult = null;

            // Act
            using (var context = new PartsStoreDbContext(options))
            {
                var obj = new Part();
                IPartRepository rep = new PartRepository(context);
                searchResult = rep.GetFilteredList(null, "Spark", "Name",10,1).Result;
                categoryResult = rep.GetFilteredList(3, null, "Name",10,1).Result;
                pagingResult = rep.GetFilteredList(null, null, "Name", 12, 1).Result;
                getAllResult = rep.GetAll().FirstOrDefault();
                getResult = rep.Get(5).Result;
                deleteResult = rep.Delete(1);
                createResult = rep.Create(obj);
                updateResult = rep.Update(obj);
            }
              
            // Assert
            Assert.Equal(4, searchResult.Count);
            Assert.Equal(7, categoryResult.Count);
            Assert.Equal(12, pagingResult.Count);
            Assert.Equal(4, getAllResult.Id);
            Assert.Equal(5, getResult.Id);
            Assert.Equal(1, deleteResult.Id);
            Assert.Equal(1, createResult.Id);
            Assert.Equal(1, updateResult.Id);
        }

        private DbContextOptions<PartsStoreDbContext> CreateInMemoryOptions()
        {
            DbContextOptionsBuilder<PartsStoreDbContext> builder = new DbContextOptionsBuilder<PartsStoreDbContext>();
            builder.UseInMemoryDatabase("PartsTestData");
            return builder.Options;
        }

        private void CreateTestData(DbContextOptions<PartsStoreDbContext> options)
        {
            using (var context = new PartsStoreDbContext(options))
            {
                var categories = new Category[]
             {
                 new Category { Name="Motor oils"},
                 new Category { Name="Breaking fluids"},
                 new Category { Name="Engine and accessories"},
                 new Category { Name="Brake system"},
             };

                foreach (Category c in categories)
                {
                    context.Categories.Add(c);
                }
                context.SaveChanges();

                var parts = new Part[]
                {
                new Part {CategoryId=1,Name="Motor oil Shell Ultra-5W30",PartNumber=1,Price=13,Quantity=13,IsActive=true},
                new Part {CategoryId=2,Name="Breaking fluid Shell-Dot 4",PartNumber=2,Price=5,Quantity=2,IsActive=true},
                new Part {CategoryId=3,Name="Spark plugs-NGK Quick Range",PartNumber=3,Price=7,Quantity=3,IsActive=false},
                new Part {CategoryId=3,Name="Spark plugs-Bosch Super",PartNumber=4,Price=8,Quantity=2,IsActive=true},
                new Part {CategoryId=3,Name="Spark plugs-Bosch Super Plus",PartNumber=5,Price=9,Quantity=4,IsActive=false},
                new Part {CategoryId=3,Name="Spark plugs-Valeo-Silver",PartNumber=6,Price=5,Quantity=1,IsActive=true},
                new Part {CategoryId=3,Name="Belt drive-Bosch Universal",PartNumber=7,Price=10,Quantity=16,IsActive=false},
                new Part {CategoryId=3,Name="Belt drive-NSK Premium",PartNumber=8,Price=13,Quantity=11,IsActive=true},
                new Part {CategoryId=3,Name="Tension roller-SKF",PartNumber=9,Price=14,Quantity=10,IsActive=true},
                new Part {CategoryId=4,Name="Brake pads-Bosch Premium",PartNumber=10,Price=25,Quantity=8,IsActive=false},
                new Part {CategoryId=4,Name="Brake pads-Brembo Ultra Sport",PartNumber=11,Price=28,Quantity=6,IsActive=true},
                new Part {CategoryId=4,Name="Brake discs-Brembo Sport",PartNumber=12,Price=130,Quantity=30,IsActive=true},
                };

                foreach (Part p in parts)
                {
                    context.Parts.Add(p);
                }
                context.SaveChanges();
            }
        }
    }
}
