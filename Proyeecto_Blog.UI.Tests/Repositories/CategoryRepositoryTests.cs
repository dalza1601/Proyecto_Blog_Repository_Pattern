using Microsoft.EntityFrameworkCore;
using Moq;
using Proyecto_Blog.BE.Models;
using Proyecto_Blog.DL.DA.Data;
using Proyecto_Blog.DL.DA.Data.Repository;
using Proyecto_Blog.DL.DA.Data.Repository.IRepository;

namespace Proyeecto_Blog.UI.Tests.Repositories
{
    public class CategoryRepositoryTests
    {
        [Fact]
        public void GetAllCategories_ShouldReturnListOfCategories()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BlogCrud")
                .Options;
            using var context = new ApplicationDbContext(options);
            var repository = new CategoryRepository(context);


            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category 1" , Orden = 1},
                new Category { Id = 2, Name = "Category 2" , Orden = 2}
            };  

            context.Categories.AddRange(categories);
            context.SaveChanges();

            var categories_expected = new List<Category>
            {
                new Category { Id = 2, Name = "Category 2" , Orden = 2}
            };

            //Act
            var result = repository.GetAll(c => c.Name.Contains("2"));

            //Assert

            //Assert.NotNull(result);
            //Assert.Equal(2, result.Count());
            Assert.Equal(categories_expected.Select(c => new { c.Id, c.Name, c.Orden }),
             result.Select(c => new { c.Id, c.Name, c.Orden }));
            //Assert.Equal(categories_expected.ElementAt(0).Name, result.ElementAt(0).Name);
        }

        [Fact]
        public void GetAll_shouldFail_ReturnException()
        {
            //Arrange
            var mockRepository = new Mock<ICategoryRepository>();
            mockRepository.Setup(x => x.GetAll())
                .Throws(new Exception(Constants.ERROR_GET_ALL_CATEGORIES));

            //Act
            var exception = Assert.Throws<Exception>(() => mockRepository.Object.GetAll());

            //Assert
            Assert.Equal(Constants.ERROR_GET_ALL_CATEGORIES, exception.Message);
        }

        [Fact]
        public void GetAll_ShouldFail_ReturnEmptyList()
        {
            //Arrange
            var mockRepository = new Mock<ICategoryRepository>();

            mockRepository.Setup(x => x.GetAll())
                .Throws(new Exception());

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BlogCrud")
                .Options;
            using var context = new ApplicationDbContext(options);

            var unitOfWork = new UnitOfWork(context);

            //Act
            var result = unitOfWork.Category.GetAll();

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Add_ShouldAddCategory_ReturnCategory()
        {
            //Arrange
            var mockRepository = new Mock<ICategoryRepository>();
            var category = new Category { Id = 1, Name = "Category 1", Orden = 1 };

            //Act
            mockRepository.Object.Add(category);

            //Assert
            mockRepository.Verify(x => x.Add(It.IsAny<Category>()),
                Times.Once);

        }

        [Fact]
        public void Remove_ShouldRemoveCategory_ById()
        {
            // Arrange
            var mockRepository = new Mock<ICategoryRepository>();
            var categoryId = 1;

            // Act
            mockRepository.Object.Remove(categoryId);

            // Assert
            mockRepository.Verify(x => x.Remove(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Remove_ShouldCallRemove_ByEntity()
        {
            // Arrange
            var mockRepository = new Mock<ICategoryRepository>();
            var category = new Category { Id = 1, Name = "Test Category" };

            // Act
            mockRepository.Object.Remove(category);

            // Assert
            mockRepository.Verify(x => x.Remove(It.IsAny<Category>()), Times.Once);
        }

    }
}
