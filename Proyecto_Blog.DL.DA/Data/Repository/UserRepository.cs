using Proyecto_Blog.BE.Models;
using Proyecto_Blog.DL.DA.Data.Repository.IRepository;

namespace Proyecto_Blog.DL.DA.Data.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void BloquearUsuario(string userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.LockoutEnd = DateTimeOffset.MaxValue;
                _context.SaveChanges();
            }
        }

        public void DesbloquearUsuario(string userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.LockoutEnd = DateTime.Now;
                _context.SaveChanges();
            }
        }

    }
}
