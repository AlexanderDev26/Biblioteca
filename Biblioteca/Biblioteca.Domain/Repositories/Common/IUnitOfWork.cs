using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Repositories.Common
{
    public interface IUnitOfWork : IDisposable
    {
        public IBibliotecaRepositoriesModule ServiceRepositoriesModule { get; }
        public Task<int> CompletedAsync();

        Task CommitAsync();

    }
}
