using System.ComponentModel.DataAnnotations;

namespace Proyecto_Blog.BE.Models
{
    public class Category
    {
        [Key] 
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre para la categoria")]
        public string Name { get; set; }

        [Range(1,100, ErrorMessage = "El valor debe estar entre 1 y 100")]
        public int? Orden { get; set; }
    }
}
