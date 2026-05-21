using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Blog.BE.Models;
using Proyecto_Blog.DL.DA.Data.Repository.IRepository;


namespace Proyecto_Blog.DL.DA.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext cntx;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            cntx = context;
        }

        public IEnumerable<SelectListItem> GetSelectListCategories()
        {
            return cntx.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }

        public void Update(Category category)
        {
            var oCategory = cntx.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (oCategory != null) { 
                oCategory.Name = category.Name;
                oCategory.Orden = category.Orden;
                cntx.SaveChanges();
            }
        }
    }
}
