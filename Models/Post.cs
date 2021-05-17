using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace alkemy_blog_challenge.Models
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El titulo es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El {0} debe ser al menos {2} y maximo {1} caracteres.")]
        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "No se puede subir un post vacio.")]
        [MaxLength(100, ErrorMessage = "El {0} debe ser al menos {2} y maximo {1} caracteres.")]
        [Display(Name = "Contenido")]
        public string Contenido { get; set; }

        [Display(Name = "Imagen")]
        public byte[] Imagen { get; set; }

        [Required(ErrorMessage = "Debes definir una categoria")]
        [MaxLength(50, ErrorMessage = "El {0} debe ser al menos {2} y maximo {1} caracteres.")]
        [Display(Name = "Categoria")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de creacion.")]
        public DateTime FechaCreacion { get; set; }
    }
}
