using Biblioteca.Domain.Repositories;
using Biblioteca.Domain.Repositories.Common;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infrastructure.DataBase.EntityFramework.Repositories.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        public BibliotecaDBcontext _context { get; }

        public IBibliotecaRepositoriesModule ServiceRepositoriesModule { get; }
        private bool _disposed = false;
        private IUnitOfWork _unitOfWorkImplementation;

        public UnitOfWork(
            BibliotecaDBcontext dbcontext,
            IBibliotecaRepositoriesModule serviceRepositoriesModule)
        {
            _context = dbcontext;
            ServiceRepositoriesModule = serviceRepositoriesModule;
        }


        public async Task<int> CompletedAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing) _context.Dispose();
                _disposed = true;
            }
        }
    }
}
