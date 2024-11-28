using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Repositories.Common
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<TEntity> CreateAsync(TEntity entity);
        public Task<TEntity> GetByIdAsync(int Id);
        public Task<bool> DeleteHardAsync(int Id);
    }
}
