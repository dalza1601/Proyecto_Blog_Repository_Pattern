using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proyecto_Blog.BE.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Ingrese un nombre")]
        public String Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [DataType(DataType.ImageUrl)] 
        public String Url { get; set; }
    }
}
