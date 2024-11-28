using Biblioteca.Application.Services.Biblioteca;
using Biblioteca.Domain.Models.Usuario;
using Biblioteca.Domain.Response;

namespace Biblioteca.Api.EndPoints
{
    internal static class UsuarioEndPoint
    {
        internal static void MapUsuarioEndpoints(this WebApplication webApp)
        {
            webApp.MapGroup("/usuario").MapGroupEndPoint();
        }

        internal static void MapGroupEndPoint(this RouteGroupBuilder groupBuilder)
        {
            // Endpoint para crear un usuario
            groupBuilder.MapPost("/", async (UsuarioModel usuario, UsuarioService usuarioService) =>
            {
                var result = await usuarioService.CreateAsync(usuario);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            })
            .WithName("CrearUsuario")
            .Produces<Result<bool>>(StatusCodes.Status200OK)
            .Produces<Result<bool>>(StatusCodes.Status400BadRequest);

            // Endpoint para obtener un usuario por ID
            groupBuilder.MapGet("/{id}", async (int id, UsuarioService usuarioService) =>
            {
                var result = await usuarioService.GetByIdAsync(id);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.NotFound(result);
            })
            .WithName("ObtenerUsuario")
            .Produces<Result<UsuarioModel>>(StatusCodes.Status200OK)
            .Produces<Result<UsuarioModel>>(StatusCodes.Status404NotFound);

            // Endpoint para eliminar un usuario de forma permanente
            groupBuilder.MapDelete("/{id}", async (int id, UsuarioService usuarioService) =>
            {
                var result = await usuarioService.DeleteHardAsync(id);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            })
            .WithName("EliminarUsuario")
            .Produces<Result<bool>>(StatusCodes.Status200OK)
            .Produces<Result<bool>>(StatusCodes.Status400BadRequest);

            // Endpoint para login de usuario
            groupBuilder.MapPost("/login", async (LoginRequest loginRequest, UsuarioService usuarioService) =>
            {
                var result = await usuarioService.LoginAsync(loginRequest.Email, loginRequest.Password);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            })
            .WithName("LoginUsuario")
            .Produces<Result<string>>(StatusCodes.Status200OK)
            .Produces<Result<string>>(StatusCodes.Status400BadRequest);

            // Endpoint para logout de usuario
            groupBuilder.MapPost("/logout", async (string token, UsuarioService usuarioService) =>
            {
                var result = await usuarioService.LogoutAsync(token);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            })
            .WithName("LogoutUsuario")
            .Produces<Result<bool>>(StatusCodes.Status200OK)
            .Produces<Result<bool>>(StatusCodes.Status400BadRequest);
        }
    }

    // Request model para Login
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
