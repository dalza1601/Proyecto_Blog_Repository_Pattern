using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Blog.BE.Models.ViewModel;
using Proyecto_Blog.DL.DA.Data.Repository.IRepository;
using Proyecto_Blog.UI.ASPNET.MVC.Utils;
using System.Diagnostics;

namespace Proyecto_Blog.UI.ASPNET.MVC.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork unitOfWork, ILogger<BaseController> logger, IMapper mapper) : base(unitOfWork, logger, mapper)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            HomeViewModel homeVM = new HomeViewModel()
            {
                Sliders = _unitOfWork.Slider.GetAll().Select(slider => _mapper.Map<SliderViewModel>(slider)),
                Articles = _unitOfWork.Article.GetAll(includeProperties: "Category")
                .Select(article => _mapper.Map<ArticleViewModel>(article))
            };

            ViewBag.IsHome = true;

            return View(homeVM);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var article = _unitOfWork.Article.Get(id);
            var articleVM = _mapper.Map<ArticleViewModel>(article);
            return View(articleVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
