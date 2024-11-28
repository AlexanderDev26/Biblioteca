using Biblioteca.Domain.Dtos.Biblioteca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Biblioteca.Domain.Dtos.Biblioteca.RolEnum;

namespace Biblioteca.Domain.Models.Usuario
{
    public class UsuarioModel : BaseModel
    {
        public string Nombre { get;  set; }
        public string Email { get;  set; }
        public string Password { get;  set; }
        public RolEnum Rol { get; private set; }
        public UsuarioModel
            (
            int id,
            string nombre,
            string email,
            string password,
            RolEnum rol

            ) : base(id)
        {
            Nombre = nombre;
            Email = email;
            Password = password;
            Rol = rol;
        }
    }
}
