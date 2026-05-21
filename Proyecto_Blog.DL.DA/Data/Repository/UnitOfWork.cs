using Proyecto_Blog.DL.DA.Data.Repository.IRepository;

namespace Proyecto_Blog.DL.DA.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _cntx;
        
        //Agregar los repositorios
        public ICategoryRepository Category { get; private set; }

        public IArticleRepository Article { get; private set; }

        public ISliderRepository Slider { get; private set; }
        
        public IUserRepository User { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {   
            _cntx = context;
            Category = new CategoryRepository(_cntx);
            Article = new ArticleRepository(_cntx);
            Slider = new SliderRepository(_cntx);
            User = new UserRepository(_cntx);
        }

        public void Dispose()
        {
            _cntx.Dispose();
        }

        public void Save()
        {
            _cntx.SaveChanges();
        }
    }
}
