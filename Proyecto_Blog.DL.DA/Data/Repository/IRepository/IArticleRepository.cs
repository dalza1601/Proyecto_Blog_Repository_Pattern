using Proyecto_Blog.BE.Models;


namespace Proyecto_Blog.DL.DA.Data.Repository.IRepository
{
    public interface IArticleRepository : IRepository<Article>
    {
        void Update(Article article);
    }
}
