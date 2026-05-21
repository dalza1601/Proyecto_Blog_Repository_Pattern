using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Blog.BE.Models;


namespace Proyecto_Blog.DL.DA.Data.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
        IEnumerable<SelectListItem> GetSelectListCategories();
    }
}
