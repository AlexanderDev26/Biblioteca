using Biblioteca.Domain.Models.Biblioteca;
using Biblioteca.Domain.Repositories.Biblioteca;
using Biblioteca.Domain.Repositories.Common;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Context;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Entities.Biblioteca;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Extensions.Biblioteca;
using Biblioteca.Infrastructure.DataBase.EntityFramework.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.DataBase.EntityFramework.Repositories.Biblioteca
{
    public class LibroRepository : IGenericRepository<LibroModel>, ILibroRepository
    {
        private readonly BibliotecaDBcontext _dbContext;

        public LibroRepository(BibliotecaDBcontext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obtener todos los libros
        public async Task<IEnumerable<LibroModel>> GetAllAsync()
        {
            var libros = await _dbContext.Libros
                .AsNoTracking()
                .ToListAsync();

            return libros.Select(libro => libro.ToModel());
        }

        // Obtener un libro por un campo específico y su valor
        public async Task<LibroModel?> GetByFieldAsync(string key, string value)
        {
            // Obtener todos los libros de la base de datos
            var libros = await _dbContext.Libros.AsNoTracking().ToListAsync();

            // Filtrar los libros en memoria
            var libro = libros
                .Where(l =>
                {
                    var property = l.GetType().GetProperty(key);
                    if (property == null) return false;

                    var propertyValue = property.GetValue(l)?.ToString();
                    return propertyValue != null && propertyValue.Contains(value, StringComparison.OrdinalIgnoreCase);
                })
                .FirstOrDefault();

            return libro?.ToModel();
        }

        // Crear un libro
        public async Task<LibroModel?> CreateAsync(LibroModel model)
        {
            var entity = model.ToEntity();
            var createdEntity = await _dbContext.Libros.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return createdEntity.Entity.ToModel();
        }

        // Actualizar un libro
        public async Task<LibroModel?> UpdateAsync(int id, LibroModel libro)
        {
            var existingLibro = await _dbContext.Libros
                .FirstOrDefaultAsync(l => l.LibroId == id);

            if (existingLibro == null) return null;

            existingLibro.Codigo = libro.Codigo;
            existingLibro.Titulo = libro.Titulo;
            existingLibro.Autor = libro.Autor;
            existingLibro.UrlImagen = libro.UrlImagen;

            _dbContext.Libros.Update(existingLibro);
            await _dbContext.SaveChangesAsync();

            return existingLibro.ToModel();
        }

        // Contar todos los libros
        public async Task<int> CountAsync()
        {
            return await _dbContext.Libros.CountAsync();
        }
        // Obtener un libro por ID
        public async Task<LibroModel?> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Libros
                .FirstOrDefaultAsync(l => l.LibroId == id);

            return entity?.ToModel();
        }

        // Eliminar un libro (borrado lógico)
        public async Task<bool> DeleteHardAsync(int id)
        {
            var libro = await _dbContext.Libros.FirstOrDefaultAsync(l => l.LibroId == id);
            if (libro == null) return false;

            _dbContext.Libros.Remove(libro);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}