using Biblioteca.Domain.Models.Biblioteca;
using Biblioteca.Domain.Repositories.Common;

namespace Biblioteca.Domain.Repositories.Biblioteca;

public interface ILibroRepository: IGenericRepository<LibroModel>
{
    Task<IEnumerable<LibroModel>> GetAllAsync();
    
    Task<LibroModel?> GetByFieldAsync(string key, string value);
    Task<int> CountAsync();
    Task<LibroModel?> UpdateAsync(int id, LibroModel libro);

}