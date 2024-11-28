using Biblioteca.Domain.Models.Biblioteca;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Entities.Biblioteca;

namespace Biblioteca.Infrastructure.DataBase.EntityFramework.Extensions.Biblioteca
{
    public static class LibroExtension
    {
        // Método para convertir de entidad a modelo
        public static LibroModel ToModel(this LibroEntity entity)
        {
            return new LibroModel
            (
                entity.LibroId,   // ID
                entity.Codigo,    // Código
                entity.Titulo,    // Título
                entity.Autor,     // Autor
                entity.UrlImagen  // URL de la imagen (puede ser nulo)
            );
        }

        // Método para convertir de modelo a entidad
        public static LibroEntity ToEntity(this LibroModel model)
        {
            return new LibroEntity
            {
                LibroId = model.Id,   // ID
                Codigo = model.Codigo, // Código
                Titulo = model.Titulo, // Título
                Autor = model.Autor,   // Autor
                UrlImagen = model.UrlImagen // URL de la imagen (puede ser nulo)
            };
        }
    }
}