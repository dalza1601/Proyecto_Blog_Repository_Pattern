using Proyecto_Blog.BE.Models;

namespace Proyecto_Blog.DL.DA.Data.Repository.IRepository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        void BloquearUsuario(string userId);
        void DesbloquearUsuario(string userId);
    }
}
