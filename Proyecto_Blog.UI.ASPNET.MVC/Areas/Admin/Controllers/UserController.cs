using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Blog.DL.DA.Data.Repository.IRepository;
using Proyecto_Blog.FL.Utilities;
using Proyecto_Blog.UI.ASPNET.MVC.Utils;

namespace Proyecto_Blog.UI.ASPNET.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{Roles.Admin}")]
    public class UserController : BaseController
    {
        public UserController(IUnitOfWork unitOfWork, ILogger<BaseController> logger, IMapper mapper) : base(unitOfWork, logger, mapper)
        {
        }

        public IActionResult Index()
        {
            return View(_unitOfWork.User.GetAll());
        }

        [HttpGet]
        public IActionResult Block(string userId) {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }
            _unitOfWork.User.BloquearUsuario(userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult UnBlock(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }
            _unitOfWork.User.DesbloquearUsuario(userId);
            return RedirectToAction(nameof(Index));
        }

    }
}
