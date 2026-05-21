using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proyecto_Blog.BE.Models.ViewModel
{
    public class SliderViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre")]
        public String Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [ValidateNever]
        [DataType(DataType.ImageUrl)]
        public String Url { get; set; }

    }
}
