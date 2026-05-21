using Proyecto_Blog.BE.Models;
using Proyecto_Blog.DL.DA.Data.Repository.IRepository;


namespace Proyecto_Blog.DL.DA.Data.Repository
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _context;
        public SliderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Slider slider)
        {
            var objFromDb = _context.Sliders
                .FirstOrDefault(s => s.Id == slider.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = slider.Name;
                objFromDb.IsActive = slider.IsActive;
                objFromDb.Url = slider.Url;
                _context.SaveChanges();
            }
        }
    }
}
