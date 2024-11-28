using Biblioteca.Domain.Models.Usuario;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Entities.Biblioteca;


namespace Biblioteca.Infrastructure.DataBase.EntityFramework.Extensions.Biblioteca
{
    public static class UsuarioExtension
    {
        // Método para convertir de entidad a modelo
        public static UsuarioModel ToModel(this UsuarioEntity entity)
        {
            return new UsuarioModel
            (
                entity.Id,
                entity.Nombre,
                entity.Email,
                entity.Password,
                entity.Rol
            );
        }

        // Método para convertir de modelo a entidad
        public static UsuarioEntity ToEntity(this UsuarioModel model)
        {
            return new UsuarioEntity
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Email = model.Email,
                Password = model.Password,
                Rol = model.Rol
            };
        }
    }
}
