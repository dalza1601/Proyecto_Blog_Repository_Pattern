using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Blog.BE.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required (ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        public string Address { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        public string City { get; set; }

        [Required(ErrorMessage = "El país es obligatorio")]
        public string Country { get; set; }

    }
}
