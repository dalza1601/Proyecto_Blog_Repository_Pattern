using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Blog.BE.Models;
using Proyecto_Blog.BE.Models.ViewModel;
using Proyecto_Blog.DL.DA.Data.Repository.IRepository;
using Proyecto_Blog.FL.Utilities;
using Proyecto_Blog.UI.ASPNET.MVC.Utils;

namespace Proyecto_Blog.UI.ASPNET.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = $"{Roles.Admin},{Roles.User}")]
    [Authorize(Roles = $"{Roles.Admin}")]
    public class ArticleController :  BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ArticleController(IUnitOfWork unitOfWork, ILogger<BaseController> logger, IMapper mapper, IWebHostEnvironment hostEnvironment) : base(unitOfWork, logger, mapper)
        {
            _webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticleRegisterViewModel article = new ArticleRegisterViewModel()
            {
                Article = new ArticleViewModel(),
                Categories = _unitOfWork.Category.GetSelectListCategories()
            };
            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleViewModel article)
        {
            if (ModelState.IsValid)
            {
                //agregar imagen
                var imgUrl = FileUploader.updloadFile(_webHostEnvironment, HttpContext.Request.Form.Files, Constants.FOLDER_ARTICLES);

                var objArticulo = new Article()
                {
                    Name = article.Name,
                    Description = article.Description,
                    CategoryId = article.CategoryId,
                    DateCreate = DateTime.Now,
                };

                if (!imgUrl.Equals(Constants.NO_HAVE_FILE))
                {
                    objArticulo.ImageUrl = imgUrl;

                    _unitOfWork.Article.Add(objArticulo);
                    _unitOfWork.Save();
                    
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Imagen", Constants.REQUIRED_IMAGE);
                }

            }
            ArticleRegisterViewModel articleRegister = new ArticleRegisterViewModel()
            {
                Article = article,
                Categories = _unitOfWork.Category.GetSelectListCategories()
            };
            return View(articleRegister);
        }

        public ActionResult Edit(int id) {

            ArticleRegisterViewModel article = new ArticleRegisterViewModel()
            {
                Article = new ArticleViewModel(),
                Categories = _unitOfWork.Category.GetSelectListCategories()
            };

            if(id > 0)
            {
                article.Article = _mapper.Map<ArticleViewModel>(_unitOfWork.Article.Get(id));
            }

            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleViewModel article)
        {
            if (ModelState.IsValid)
            {
                var objArticulo = _unitOfWork.Article.Get(article.Id);
                if (objArticulo != null)
                {
                    objArticulo.Name = article.Name;
                    objArticulo.Description = article.Description;
                    objArticulo.CategoryId = article.CategoryId;
                    var imgUrl = FileUploader.updloadFile(_webHostEnvironment, HttpContext.Request.Form.Files, Constants.FOLDER_ARTICLES);

                    if (!imgUrl.Equals(Constants.NO_HAVE_FILE))
                        objArticulo.ImageUrl = imgUrl;

                    _unitOfWork.Article.Update(objArticulo);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));

                }
            }
            ArticleRegisterViewModel articleRegister = new ArticleRegisterViewModel()
            {
                Article = article,
                Categories = _unitOfWork.Category.GetSelectListCategories()
            };
            return View(articleRegister);
        }

        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Article.GetAll(includeProperties: "Category") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            String Message = string.Empty;//""
            var objArticulo = _unitOfWork.Article.Get(id);
            //Agregar logica para eliminar la imagen

            if (objArticulo == null)
            {
                return Json(new { success = false, message = "Error borrando la categoría" });
            }

            _unitOfWork.Article.Remove(objArticulo);
            trySave(out Message);

            Message = string.IsNullOrWhiteSpace(Message) ? "La categoria fue borrada correctamente" : Message;
            return Json(new { success = true, message = Message });

        }

        #endregion
    }
}
