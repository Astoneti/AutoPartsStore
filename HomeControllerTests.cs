using Godel.AutoPartsStore.BusinessLogicLayer.Interfaces;
using Godel.AutoPartsStore.Common.Models;
using Godel.AutoPartsStore.DAL.Models;
using Godel.AutoPartsStore.PartsStore.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Godel.AutoPartsStore.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexReturnsViewResult_WithListOf_Categories()
        {
            // Arrange
            var categories = GetTestCategories();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockPartService = new Mock<IPartService>();
            mockCategoryService.Setup(repo => repo.GetAll()).Returns(categories);
            var controller = new HomeController(mockLogger.Object, mockCategoryService.Object, mockPartService.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Category>>(viewResult.Model);
            Assert.Equal(categories[1], model[1]);
            Assert.Equal(categories.Count, model.Count);
        }

        public List<Category> GetTestCategories()
        {
            var categories = new List<Category>
            {
                 new Category { Name="Motor oils"},
                 new Category { Name="Breaking fluids"},
                 new Category { Name="Engine and accessories"},
                 new Category { Name="Brake system"},
            };
            return categories;
        }

        [Fact]
        public void PartsReturnsViewResult_Of_FilteredList_OfParts()
        {
            // Arrange
            var parts = GetFilteredListOfParts().Result;
            var mockCategoryService = new Mock<ICategoryService>();
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockPartService = new Mock<IPartService>();
            mockPartService.Setup(repo => repo.GetFilteredList(null, string.Empty, string.Empty, 1, 5)).Returns(GetFilteredListOfParts());
            var controller = new HomeController(mockLogger.Object, mockCategoryService.Object, mockPartService.Object);

            // Act
            var result = controller.Parts().Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PaginatedList<Part>>(viewResult.Model);
            Assert.Equal<Part>(parts, model, new PartComparer());
            Assert.Equal(parts.Count, model.Count);
        }

        public async Task<PaginatedList<Part>> GetFilteredListOfParts()
        {
            var parts = new List<Part>
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
            return new PaginatedList<Part>( parts, parts.Count, 1, 5);
        }

        [Fact]
        public void DetailsReturnsViewResultOfDetailsListOfParts()
        {
            // Arrange
            var part = GetDetailsFromParts().Result;
            var mockCategoryService = new Mock<ICategoryService>();
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockPartService = new Mock<IPartService>();
            mockPartService.Setup(repo => repo.Get(5)).Returns(GetDetailsFromParts());
            var controller = new HomeController(mockLogger.Object, mockCategoryService.Object, mockPartService.Object);

            // Act
            var result = controller.Details(5).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Part>(viewResult.Model);
            Assert.Equal(part.Name, model.Name);
           
        }

        public async Task<Part> GetDetailsFromParts()
        {
            return new Part { CategoryId = 3, Name = "Spark plugs-Valeo-Silver", PartNumber = 6, Price = 5, Quantity = 1, IsActive = true };
        }
    }
}
