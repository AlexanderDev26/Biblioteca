using Biblioteca.Domain.Models.Biblioteca;

namespace Biblioteca.Application.Utils;

public static class SearchHelper
{
    public static IEnumerable<LibroModel> LinearSearch(IEnumerable<LibroModel> libros, string keysearch, string search)
    {
        return libros.Where(libro =>
            libro.GetType().GetProperty(keysearch)?.GetValue(libro)?.ToString()?.Contains(search, StringComparison.OrdinalIgnoreCase) == true);
    }

}