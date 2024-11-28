using Biblioteca.Domain.Models.Usuario;
using Biblioteca.Domain.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Repositories.Biblioteca
{
    public interface IUsuarioRepository: IGenericRepository<UsuarioModel>
    {
        // Método para verificar si el email ya existe
        Task<bool> IsEmailDuplicadoAsync(string email);

        // Método para verificar si el nombre de usuario ya existe
        Task<bool> IsNombreDuplicadoAsync(string nombre);

        // Login con email y password, retornando el modelo de usuario
        Task<UsuarioModel> LoginAsync(string email, string password);

        // Logout que invalidará el token
        Task LogoutAsync(string token);
    }
}
