using Moq;
using Proyecto_Blog.BE.Models;
using Proyecto_Blog.DL.DA.Data.Repository.IRepository;

namespace Proyeecto_Blog.UI.Tests.Repositories
{
    public class ArticleRepositoryTests
    {
        [Fact]
        public void GetAllArticles_ShouldReturnListOfArticles()
        {
            // Arrange
           var mockArticle = new Mock<IArticleRepository>();
            var articles = new List<Article>
            {
                new Article { Id = 1, Name = "Article 1", Description = "Content 1" },
                new Article { Id = 2, Name = "Article 2", Description = "Content 2" }
            };
            mockArticle.Setup(repo => repo.GetAll()).Returns(articles);
            // Act
            var result = mockArticle.Object.GetAll();
            // Assert
            Assert.Equal(articles.Select(a => new { a.Id, a.Name, a.Description }),
                         result.Select(a => new { a.Id, a.Name, a.Description }));
        }

        [Fact]
        public void Add_ShouldReturnArticle() { 
        
            var  mockRepository = new Mock<IArticleRepository>();
            var article = new Article { Id = 1, Name = "Article 1", Description = "Content 1", CategoryId = 1 };

            mockRepository.Setup(repo => repo.Add(It.IsAny<Article>()));

            mockRepository.Object.Add(article);

            mockRepository.Verify(x => x.Add(It.IsAny<Article>()),
                Times.Once);

        }

        [Fact]
        public void Update_ShouldReturnArticle() { 
        
            //Arrange
            var mockRepository = new Mock<IArticleRepository>();
            var article = new Article { Id = 1, Name = "Article 1", Description = "Content 1", CategoryId = 1, DateCreate = DateTime.Now, ImageUrl = "test" };
            mockRepository.Setup(repo => repo.Update(article));

            //Act
            mockRepository.Object.Update(article);

            //Assert
            mockRepository.Verify(x => x.Update(It.Is<Article>( article =>
                    article.Id ==1 &&
                 article.Name == "Article 1"
            )),
                Times.Once);

        }

        [Fact]
        public void Update_ShouldReturnException()
        {
            //Arrange
            var mockRepository = new Mock<IArticleRepository>();

            var article = new Article { Id = 1, Name = "Article 1", Description = "Content 1", CategoryId = 1, DateCreate = DateTime.Now, ImageUrl = "test" };
            
            mockRepository.Setup(repo => repo.Update(article))
                .Throws(new Exception("Error updating article"));

            //Act
            var exception = Assert.Throws<Exception>(() => mockRepository.Object.Update(article));
            
            //Assert
            Assert.Equal("Error updating article", exception.Message);

        }
    }
}
