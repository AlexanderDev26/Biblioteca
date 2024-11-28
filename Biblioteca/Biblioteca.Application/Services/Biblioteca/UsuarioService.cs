using Biblioteca.Application.Token;
using Biblioteca.Domain.Models.Usuario;
using Biblioteca.Domain.Repositories.Common;
using Biblioteca.Domain.Response;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Services.Biblioteca
{
    public class UsuarioService
    {
        private readonly IValidator<UsuarioModel> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(
            IValidator<UsuarioModel> validator,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        // Crear un usuario
        public async Task<Result<bool>> CreateAsync(UsuarioModel usuario)
        {
            var validationResult = await _validator.ValidateAsync(usuario);

            if (validationResult.IsValid)
            {
                usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);

                var isCreated = await _unitOfWork.ServiceRepositoriesModule
                    .UsuarioRepository.CreateAsync(usuario) != null;

                await _unitOfWork.CompletedAsync();
                return Result<bool>.Success(isCreated, "Usuario creado exitosamente.");
            }

            return Result<bool>.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList(), "Error al crear usuario.");
        }

        // Obtener un usuario por ID
        public async Task<Result<UsuarioModel>> GetByIdAsync(int id)
        {
            var usuario = await _unitOfWork.ServiceRepositoriesModule.UsuarioRepository.GetByIdAsync(id);

            if (usuario != null)
                return Result<UsuarioModel>.Success(usuario, "Usuario encontrado.");

            return Result<UsuarioModel>.Failure(new List<string> { "Usuario no encontrado." }, "Error.");
        }

        // Eliminar un usuario de forma permanente
        public async Task<Result<bool>> DeleteHardAsync(int id)
        {
            var isDeleted = await _unitOfWork.ServiceRepositoriesModule.UsuarioRepository.DeleteHardAsync(id);

            if (isDeleted)
            {
                await _unitOfWork.CompletedAsync();
                return Result<bool>.Success(true, "Usuario eliminado exitosamente.");
            }

            return Result<bool>.Failure(new List<string> { "Error al eliminar usuario." }, "Error.");
        }

        // Login de usuario
        public async Task<Result<string>> LoginAsync(string email, string password)
        {
            var usuario = await _unitOfWork.ServiceRepositoriesModule
                .UsuarioRepository.LoginAsync(email, password);

            if (usuario != null)
            {
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, usuario.Password);
                if (isPasswordValid)
                {
                    // Aquí deberías generar un token JWT o similar
                    string token = JwtTokenHelper.GenerateToken(
                        usuario.Email,
                        usuario.Rol.ToString(),
                        usuario.Id); 
                    return Result<string>.Success(token, "Login exitoso.");
                }
                else
                {
                    return Result<string>.Failure(new List<string> { "Credenciales incorrectas." }, "Error.");
                }
            }

            return Result<string>.Failure(new List<string> { "Credenciales incorrectas." }, "Error.");
        }

        // Logout de usuario (invalida el token)
        public async Task<Result<bool>> LogoutAsync(string token)
        {
            return Result<bool>.Success(true, "Logout exitoso.");
        }
    }
}
