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
    public class SliderController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SliderController(IUnitOfWork unitOfWork, ILogger<BaseController> logger, IMapper mapper, IWebHostEnvironment hostEnvironment) : base(unitOfWork, logger, mapper)
        {
            _webHostEnvironment = hostEnvironment;
        }

        // GET: SliderController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SliderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SliderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SliderViewModel sliderViewModel)
        {
            if (ModelState.IsValid) {
                try
                {
                    //agregar imagen
                    var imgUrl = FileUploader.updloadFile(_webHostEnvironment, HttpContext.Request.Form.Files, Constants.FOLDER_SLIDER);
                    var objSlider = _mapper.Map<BE.Models.Slider>(sliderViewModel);

                    if (!imgUrl.Equals(Constants.NO_HAVE_FILE))
                    {
                        objSlider.Url = imgUrl;
                        _unitOfWork.Slider.Add(objSlider);
                        _unitOfWork.Save();

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("Imagen", Constants.REQUIRED_IMAGE);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message, e.StackTrace, "Error al subir la imagen");
                    return View(sliderViewModel);
                }

            }

            return View(sliderViewModel);
        }

        // GET: SliderController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id > 0)
            {
                var objSlider = _unitOfWork.Slider.Get(id);
                if (objSlider != null)
                {
                    return View(_mapper.Map<SliderViewModel>(objSlider));
                }
            }
            return NotFound();
        }

        // POST: SliderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SliderViewModel sliderViewModel)
        {

            if (ModelState.IsValid) {
                var oSlider = _unitOfWork.Slider.Get(sliderViewModel.Id);
                if (oSlider != null)
                {
                    oSlider.Name = sliderViewModel.Name;
                    oSlider.IsActive = sliderViewModel.IsActive;
                    var imgUrl = FileUploader.updloadFile(_webHostEnvironment, HttpContext.Request.Form.Files, Constants.FOLDER_ARTICLES);

                    if (!imgUrl.Equals(Constants.NO_HAVE_FILE))
                        oSlider.Url = imgUrl;

                    _unitOfWork.Slider.Update(oSlider);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(sliderViewModel);
        }

        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Slider.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            String Message = string.Empty;//""
            var objSlider = _unitOfWork.Slider.Get(id);

            if (objSlider == null)
            {
                return Json(new { success = false, message = "Error borrando el slider" });
            }

            _unitOfWork.Slider.Remove(objSlider);
            trySave(out Message);

            Message = string.IsNullOrWhiteSpace(Message) ? "El slider fue borrado correctamente" : Message;
            return Json(new { success = true, message = Message });

        }
        #endregion
    }
}
