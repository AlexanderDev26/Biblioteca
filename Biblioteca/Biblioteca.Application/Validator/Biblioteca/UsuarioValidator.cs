using Biblioteca.Domain.Models.Usuario;
using Biblioteca.Domain.Repositories.Biblioteca;
using FluentValidation;


namespace Biblioteca.Application.Validator.Biblioteca
{
    public class UsuarioValidator : AbstractValidator<UsuarioModel>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioValidator(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;

            // Validación de nombre de usuario (mínimo 3 caracteres, sin números)
            RuleFor(u => u.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("El nombre solo puede contener letras.");

            // Validación de email (formato correcto y único)
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("El email es requerido.")
                .EmailAddress().WithMessage("El email no tiene un formato válido.")
                .MustAsync(BeUniqueEmail).WithMessage("El email ya está registrado.");

            // Validación de la contraseña (mínimo 8 caracteres, al menos una letra, un número y un signo)
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("La contraseña es requerida.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .Matches(@"[A-Za-z]").WithMessage("La contraseña debe contener al menos una letra.")
                .Matches(@"[0-9]").WithMessage("La contraseña debe contener al menos un número.")
                .Matches(@"[\W]").WithMessage("La contraseña debe contener al menos un carácter especial.");

            // Validación de duplicidad de nombre de usuario
            RuleFor(u => u.Nombre)
                .MustAsync(BeUniqueNombre).WithMessage("El nombre de usuario ya existe.");
        }

        // Validar que el email no esté duplicado
        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return !await _usuarioRepository.IsEmailDuplicadoAsync(email);
        }

        // Validar que el nombre no esté duplicado
        private async Task<bool> BeUniqueNombre(string nombre, CancellationToken cancellationToken)
        {
            return !await _usuarioRepository.IsNombreDuplicadoAsync(nombre);
        }
    }
}
