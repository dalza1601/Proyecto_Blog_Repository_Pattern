using AutoMapper;
using Proyecto_Blog.BE.Models;
using Proyecto_Blog.BE.Models.ViewModel;

namespace Proyecto_Blog.UI.ASPNET.MVC.Helppers
{
    public class MappingHelpper : Profile
    {

        public MappingHelpper()
        {
            CreateMap<Article, ArticleViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name)).ReverseMap();
            CreateMap<Slider, SliderViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();

        }
    }
}
