using Biblioteca.Domain.Repositories;
using Biblioteca.Domain.Repositories.Biblioteca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Repositories.Biblioteca;

namespace Biblioteca.Infrastructure.DataBase.EntityFramework.Repositories
{
    public class BibliotecaRepositoriesModule: IBibliotecaRepositoriesModule
    {
        public IUsuarioRepository UsuarioRepository { get; }
        public ILibroRepository LibroRepository { get; }
        
        public BibliotecaRepositoriesModule(
            IUsuarioRepository usuarioRepository,
            ILibroRepository libroRepository)
        {
            UsuarioRepository = usuarioRepository;
            LibroRepository = libroRepository;
        }
    }
}
