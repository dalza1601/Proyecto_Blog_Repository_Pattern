using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Blog.BE.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es obligatorio")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "La descripcion es obligatorio")]
        public string Description { get; set; }

        public DateTime DateCreate { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        //Relaciones   
        [Required(ErrorMessage = "La categoria es obligatoria")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

    }
}
