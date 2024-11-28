using Biblioteca.Domain.Repositories.Biblioteca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Repositories
{
    public interface IBibliotecaRepositoriesModule
    {
        public IUsuarioRepository UsuarioRepository { get; }
        public ILibroRepository LibroRepository { get; }
    }
}
