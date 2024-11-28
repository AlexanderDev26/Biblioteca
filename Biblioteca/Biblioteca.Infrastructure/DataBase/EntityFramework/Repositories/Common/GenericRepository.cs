using Biblioteca.Domain.Repositories.Common;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Context;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infrastructure.DataBase.EntityFramework.Repositories.Common
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly BibliotecaDBcontext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(BibliotecaDBcontext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
        // cambiar el nombre CreateAsync 
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var entityEntry = await _dbSet.AddAsync(entity);
            return entityEntry.Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var entityEntry = _dbSet.Update(entity);
            return entityEntry.Entity;
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> DeleteHardAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            return true;
        }
    }
}
