using Microsoft.AspNetCore.Mvc.Rendering;

namespace Proyecto_Blog.BE.Models.ViewModel
{
    public class ArticleRegisterViewModel
    {
        public ArticleViewModel Article { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
