
namespace Proyecto_Blog.BE.Models.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<SliderViewModel> Sliders { get; set; }
        public IEnumerable<ArticleViewModel> Articles { get; set; }
    }
}
