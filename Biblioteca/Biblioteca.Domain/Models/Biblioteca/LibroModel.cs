using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Models.Biblioteca
{
    public class LibroModel : BaseModel
    {
        public string Codigo { get; private set; }
        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public string? UrlImagen { get; private set; }

        public LibroModel
            (
            int id,
            string codigo,
            string titulo,
            string autor,
            string? urlimagen
            ) : base(id)
        {
            Codigo = codigo;
            Titulo = titulo;
            Autor = autor;
            UrlImagen = urlimagen;
        }
    }
}
