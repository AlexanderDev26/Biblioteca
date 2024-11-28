using Biblioteca.Application.Services.Biblioteca;
using Biblioteca.Domain.Models.Biblioteca;

namespace Biblioteca.Api.EndPoints
{
    internal static class LibroEndPoint
    {
        internal static void MapLibroEndpoints(this WebApplication webApp)
        {
            webApp.MapGroup("/libro").MapGroupEndPoint2();
        }

        internal static void MapGroupEndPoint2(this RouteGroupBuilder groupBuilder)
        {
            // Endpoint para crear un libro
            groupBuilder.MapPost("/", async (LibroModel libro, LibroService libroService) =>
            {
                var result = await libroService.CreateAsync(libro);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            });

            // Endpoint para obtener todos los libros
            groupBuilder.MapGet("/", async (string? sortField, string? algorithm, string? keysearch, string? search,
                LibroService libroService) =>
            {
                var result = await libroService.GetAllAsync(sortField, algorithm, keysearch, search);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            });


            // Endpoint para obtener un libro por ID
            groupBuilder.MapGet("/{id:int}", async (int id, LibroService libroService) =>
            {
                var result = await libroService.GetByIdAsync(id);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            });

            // Endpoint para actualizar un libro
            groupBuilder.MapPut("/{id:int}", async (int id, LibroModel libro, LibroService libroService) =>
            {
                var result = await libroService.UpdateAsync(id, libro);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            });

            // Endpoint para eliminar un libro
            groupBuilder.MapDelete("/{id:int}", async (int id, LibroService libroService) =>
            {
                var result = await libroService.DeleteAsync(id);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            });

            // Endpoint para contar todos los libros
            groupBuilder.MapGet("/count", async (LibroService libroService) =>
            {
                var result = await libroService.CountAsync();
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            });

            // Endpoint para buscar un libro por un campo específico
            groupBuilder.MapGet("/search", async (string key, string value, LibroService libroService) =>
            {
                var result = await libroService.GetByFieldAsync(key, value);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            });
        }
    }
}