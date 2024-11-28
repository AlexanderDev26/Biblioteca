using Biblioteca.Domain.Models.Biblioteca;
using FluentValidation;

namespace Biblioteca.Application.Validator.Biblioteca;

public class LibroValidator : AbstractValidator<LibroModel>
{
    public LibroValidator()
    {
        RuleFor(l => l.Codigo)
            .NotEmpty().WithMessage("El código es obligatorio.")
            .MaximumLength(50).WithMessage("El código no puede exceder los 50 caracteres.");

        RuleFor(l => l.Titulo)
            .NotEmpty().WithMessage("El título es obligatorio.")
            .MaximumLength(255).WithMessage("El título no puede exceder los 255 caracteres.");

        RuleFor(l => l.Autor)
            .NotEmpty().WithMessage("El autor es obligatorio.")
            .MaximumLength(100).WithMessage("El autor no puede exceder los 100 caracteres.");
    }
}