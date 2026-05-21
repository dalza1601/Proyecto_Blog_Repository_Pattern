using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Blog.DL.DA.Data.Repository.IRepository;

namespace Proyecto_Blog.UI.ASPNET.MVC.Utils
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ILogger<BaseController> _logger;
        protected readonly IMapper _mapper;
        //hacemos la inyeccion de depencias para obtener la instancia
        public BaseController(IUnitOfWork unitOfWork, ILogger<BaseController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        protected bool trySave(out string? errorMessage) { 
        
            errorMessage = null;
            try
            {
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar los cambios en la base de datos.");
                errorMessage = "Ocurrio un error al guardar los cambios. Porfavor, intentelo mas tarde.";
                return false;
            }
        }

        protected void AddModelErrors(IEnumerable<string> errors) {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }
    }
}
