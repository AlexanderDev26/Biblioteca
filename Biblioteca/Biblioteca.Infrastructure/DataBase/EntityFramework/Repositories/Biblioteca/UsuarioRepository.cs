using Biblioteca.Domain.Models.Usuario;
using Biblioteca.Domain.Repositories.Biblioteca;
using Biblioteca.Domain.Repositories.Common;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Context;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Entities.Biblioteca;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Extensions.Biblioteca;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.DataBase.EntityFramework.Repositories.Biblioteca
{
    public class UsuarioRepository : GenericRepository<UsuarioEntity>, IUsuarioRepository
    {
        private readonly BibliotecaDBcontext _dbContext;

        public UsuarioRepository(BibliotecaDBcontext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // Crear un usuario
        public async Task<UsuarioModel> CreateAsync(UsuarioModel model)
        {
            var entity = model.ToEntity();
            var createdEntity = await _dbContext.Usuarios.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return createdEntity.Entity.ToModel();
        }

        // Verificar si un correo está duplicado
        public async Task<bool> IsEmailDuplicadoAsync(string email)
        {
            return await _dbContext.Usuarios.AnyAsync(u => u.Email == email);
        }

        // Verificar si un nombre está duplicado
        public async Task<bool> IsNombreDuplicadoAsync(string nombre)
        {
            return await _dbContext.Usuarios.AnyAsync(u => u.Nombre == nombre);
        }

        // Login de usuario
        public async Task<UsuarioModel?> LoginAsync(string email, string password)
        {
            var entity = await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            return entity?.ToModel();
        }

        // Logout de usuario
        public Task LogoutAsync(string token)
        {
            // Suponiendo que manejas un sistema de tokens, podrías implementar la invalidación aquí.
            // Por ejemplo, eliminar el token de una base de datos o de un sistema de caché.
            throw new NotImplementedException("Implementar lógica de logout si es necesario.");
        }

        // Obtener un usuario por ID
        async Task<UsuarioModel?> IGenericRepository<UsuarioModel>.GetByIdAsync(int id)
        {
            var entity = await _dbContext.Usuarios.FindAsync(id);
            return entity?.ToModel();
        }
    }
}