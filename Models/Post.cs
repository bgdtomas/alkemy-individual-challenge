using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alkemy_blog_challenge.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string Imagen { get; set; }
        public string Categoria { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
