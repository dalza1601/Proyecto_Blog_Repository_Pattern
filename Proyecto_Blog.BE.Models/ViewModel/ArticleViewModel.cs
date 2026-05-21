using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Blog.BE.Models.ViewModel
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }

        [ValidateNever]
        public string Category { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage ="El campo descripcion solo acepta alfanumericos.")]
        [Required(ErrorMessage = "La descripcion es obligatorio")]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreate { get; set; }

        [ValidateNever]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [Display(Name= "Categoria")]
        public int CategoryId { get; set; }

    }
}
