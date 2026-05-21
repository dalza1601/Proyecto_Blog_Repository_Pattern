using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proyecto_Blog.BE.Models.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nombre de la categoria")]
        [Required(ErrorMessage = "Ingrese un nombre para la categoria")]
        public string Name { get; set; }
        
        [Display(Name = "N° de Orden")]
        [Range(1, 100, ErrorMessage = "El valor debe estar entre 1 y 100")]
        public int? Orden { get; set; }
    }
}
