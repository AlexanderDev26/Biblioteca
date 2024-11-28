using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Infrastructure.DataBase.EntityFramework.Entities.Biblioteca
{
    [Table("Libros", Schema = "Biblioteca")]
    public class LibroEntity: BaseEntity
    {
        [Key]
        public int LibroId { get; set; }
        
        [Required]
        public string Codigo { get; set; }
        
        [Required]
        public string Titulo { get; set; }
        
        [Required]
        public string Autor { get; set; }
        
        public string? UrlImagen { get; set; }
    }
}