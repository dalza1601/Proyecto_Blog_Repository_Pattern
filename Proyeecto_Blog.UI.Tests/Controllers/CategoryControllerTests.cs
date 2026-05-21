using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Proyecto_Blog.BE.Models;
using Proyecto_Blog.DL.DA.Data.Repository.IRepository;
using Proyecto_Blog.UI.ASPNET.MVC.Areas.Admin.Controllers;


namespace Proyeecto_Blog.UI.Tests.Controllers
{
    public class CategoryControllerTests
    {

        [Fact]
        public void Index() 
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var logger = new Mock<ILogger<CategoryController>>();
            var mapper = new Mock<AutoMapper.IMapper>();

            var controller = new CategoryController(mockUnitOfWork.Object, logger.Object, mapper.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public void GetAll_ReturnsViewResult_WithListOfCategories()
        {

            //Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" }
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var logger   = new Mock<ILogger<CategoryController>>();
            var mapper = new Mock<AutoMapper.IMapper>();
            
            mockUnitOfWork.Setup(repo => repo.Category.GetAll()).Returns(categories);

            var controller = new CategoryController(mockUnitOfWork.Object, logger.Object, mapper.Object);

            //Act
            var result = controller.GetAll();

            //Assert
            var viewResult = Assert.IsType<JsonResult>(result);

        }

        [Fact]
        public void GetAll_WhenUnitOfWorkFail_ShouldReturnException()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var logger = new Mock<ILogger<CategoryController>>();
            var mapper = new Mock<AutoMapper.IMapper>();
            var controller = new CategoryController(mockUnitOfWork.Object, logger.Object, mapper.Object);

            mockUnitOfWork.Setup(repo => repo.Category.GetAll()).Throws(new Exception("Database error"));

            //Act
            var exception = Assert.Throws<Exception>(() => controller.GetAll());

            //Assert
            Assert.Equal("Database error", exception.Message);

        }
    }
}
