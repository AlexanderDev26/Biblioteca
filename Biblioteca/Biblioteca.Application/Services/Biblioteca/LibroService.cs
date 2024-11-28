using Biblioteca.Application.Utils;
using Biblioteca.Domain.Models.Biblioteca;
using Biblioteca.Domain.Repositories.Common;
using Biblioteca.Domain.Response;
using FluentValidation;

namespace Biblioteca.Application.Services.Biblioteca
{
    public class LibroService
    {
        private readonly IValidator<LibroModel> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public LibroService(
            IValidator<LibroModel> validator,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        // Obtener todos los libros ordenados por un campo especificado
        public async Task<Result<IEnumerable<LibroModel>>> GetAllAsync(
            string sortField = "Titulo",
            string algorithm = "quick",
            string? keysearch = null,
            string? search = null)
        {
            // Obtener todos los libros
            var libros = (await _unitOfWork.ServiceRepositoriesModule.LibroRepository.GetAllAsync()).ToList();
    
            // Verifica cuántos libros se han obtenido
            Console.WriteLine($"Libros obtenidos: {libros.Count}");

            IEnumerable<LibroModel> resultados;

            // Si hay búsqueda, aplicamos el filtro
            if (!string.IsNullOrWhiteSpace(keysearch) && !string.IsNullOrWhiteSpace(search))
            {
                resultados = SearchHelper.LinearSearch(libros, keysearch, search);
                Console.WriteLine($"Libros después del filtro: {resultados.Count()}");
            }
            else
            {
                resultados = libros;
            }

            // Aplicar el algoritmo de ordenamiento
            resultados = algorithm.ToLower() switch
            {
                "quick" => QuickSortHelper.QuickSort(resultados, sortField),
                "bubble" => BubbleSortHelper.BubbleSort(resultados, sortField),
                "merge" => MergeSortHelper.MergeSort(resultados, sortField),
                _ => QuickSortHelper.QuickSort(resultados, sortField) // Valor por defecto
            };

            return Result<IEnumerable<LibroModel>>.Success(resultados, "Libros obtenidos exitosamente.");
        }


        // Buscar un libro por un campo específico y su valor
        public async Task<Result<LibroModel>> GetByFieldAsync(string key, string value)
        {
            // Obtener todos los libros del repositorio
            var libros = (await _unitOfWork.ServiceRepositoriesModule
                .LibroRepository.GetAllAsync()).ToList();

            // Buscar el libro según el campo (key) y el valor (value)
            var libro = libros
                .Where(l =>
                {
                    var property = l.GetType().GetProperty(key); // Obtenemos la propiedad por el nombre
                    if (property == null) return false; // Si la propiedad no existe, retornamos false

                    var propertyValue = property.GetValue(l)?.ToString(); // Obtenemos el valor de la propiedad
                    return propertyValue != null && propertyValue.Contains(value, StringComparison.OrdinalIgnoreCase);
                })
                .FirstOrDefault(); // Solo devolvemos el primer libro que cumpla la condición

            if (libro == null)
            {
                return Result<LibroModel>.Failure(new List<string> { "Libro no encontrado." },
                    "No se pudo encontrar el libro.");
            }

            return Result<LibroModel>.Success(libro, "Libro encontrado exitosamente.");
        }



        // Crear un libro
        public async Task<Result<bool>> CreateAsync(LibroModel libro)
        {
            var validationResult = await _validator.ValidateAsync(libro);

            if (validationResult.IsValid)
            {
                var isCreated = await _unitOfWork.ServiceRepositoriesModule.LibroRepository.CreateAsync(libro) != null;

                await _unitOfWork.CompletedAsync();
                return Result<bool>.Success(isCreated, "Libro creado exitosamente.");
            }

            // Se pasa una lista de errores en lugar de un solo mensaje
            return Result<bool>.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                "Error de validación.");
        }

// Actualizar un libro
        public async Task<Result<bool>> UpdateAsync(int id, LibroModel libro)
        {
            var validationResult = await _validator.ValidateAsync(libro);

            if (!validationResult.IsValid)
            {
                // Se pasa una lista de errores en lugar de un solo mensaje
                return Result<bool>.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    "Error de validación.");
            }

            var existingLibro = await _unitOfWork.ServiceRepositoriesModule
                .LibroRepository.GetByIdAsync(id);

            if (existingLibro == null)
            {
                // Se pasa una lista con un error específico
                return Result<bool>.Failure(new List<string> { "Libro no encontrado." },
                    "No se pudo encontrar el libro.");
            }

            var isUpdated = await _unitOfWork.ServiceRepositoriesModule
                .LibroRepository.UpdateAsync(id, libro) != null;

            await _unitOfWork.CompletedAsync();
            return Result<bool>.Success(isUpdated, "Libro actualizado exitosamente.");
        }

// Obtener libro por ID
        public async Task<Result<LibroModel>> GetByIdAsync(int id)
        {
            var libro = await _unitOfWork.ServiceRepositoriesModule
                .LibroRepository.GetByIdAsync(id);

            if (libro == null)
            {
                // Se pasa una lista con un error específico
                return Result<LibroModel>.Failure(new List<string> { "Libro no encontrado." },
                    "No se pudo encontrar el libro.");
            }

            return Result<LibroModel>.Success(libro, "Libro encontrado exitosamente.");
        }

// Eliminar un libro
        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var libro = await _unitOfWork.ServiceRepositoriesModule
                .LibroRepository.GetByIdAsync(id);

            if (libro == null)
            {
                // Se pasa una lista con un error específico
                return Result<bool>.Failure(new List<string> { "Libro no encontrado." },
                    "No se pudo encontrar el libro.");
            }

            var isDeleted = await _unitOfWork.ServiceRepositoriesModule
                .LibroRepository.DeleteHardAsync(id);

            await _unitOfWork.CompletedAsync();
            return Result<bool>.Success(isDeleted, "Libro eliminado exitosamente.");
        }

// Contar todos los libros
        public async Task<Result<int>> CountAsync()
        {
            var total = await _unitOfWork.ServiceRepositoriesModule
                .LibroRepository.CountAsync();

            return Result<int>.Success(total, "Total de libros calculado exitosamente.");
        }
    }
}
