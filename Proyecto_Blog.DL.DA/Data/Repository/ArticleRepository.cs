using Proyecto_Blog.BE.Models;
using Proyecto_Blog.DL.DA.Data.Repository.IRepository;

namespace Proyecto_Blog.DL.DA.Data.Repository
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Article article)
        {
            var objFromDb = _context.Articles
                .FirstOrDefault(s => s.Id == article.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = article.Name;
                objFromDb.Description = article.Description;
                objFromDb.CategoryId = article.CategoryId;
                objFromDb.ImageUrl = article.ImageUrl;
                _context.SaveChanges();
            }
        }
    }
}
