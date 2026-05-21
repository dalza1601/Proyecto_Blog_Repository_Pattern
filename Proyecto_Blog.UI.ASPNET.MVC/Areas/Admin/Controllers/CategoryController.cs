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
    [Authorize(Roles = Roles.Admin)]
    public class CategoryController : BaseController
    {

        private String errorMessage = string.Empty;
        public CategoryController(IUnitOfWork unitOfWork, ILogger<BaseController> logger, IMapper mapper) : base(unitOfWork, logger, mapper)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create() { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                var oCategory = _mapper.Map<Category>(categoryVM);
                _unitOfWork.Category.Add(oCategory);
                trySave(out errorMessage);
                if (string.IsNullOrEmpty(errorMessage))
                    return RedirectToAction(nameof(Index));
            }

            return View(categoryVM);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var oCategory = _unitOfWork.Category.Get(id);

            if (oCategory is null) return NotFound();

            return View(_mapper.Map<CategoryViewModel>(oCategory));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(_mapper.Map<Category>(categoryVM));
                trySave(out errorMessage);
                if (string.IsNullOrEmpty(errorMessage))
                    return RedirectToAction(nameof(Index));
            }

            return View(categoryVM);
        }

        #region LLamado a APIs

        [HttpGet]
        public IActionResult GetAll() { 
        
            return Json(new {data = _unitOfWork.Category.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            String msg = string.Empty;
            bool isDeleted = false;

            var oCategory = _unitOfWork.Category
                .GetFirstOrDefault(c => c.Id == id);

            if (oCategory is null)
            {
                msg = "No se encontró la categoría.";
            }
            else
            {
                _unitOfWork.Category.Remove(oCategory);
                msg = "Categoría eliminada correctamente.";
                isDeleted = true;
            }

            return Json(new { success = isDeleted, message = msg });

        }
        #endregion

    }
}
