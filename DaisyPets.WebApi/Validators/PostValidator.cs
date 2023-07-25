using DaisyPets.Core.Application.ViewModels;
using FluentValidation;

namespace DaisyPets.WebApi.Validators
{
    /// <summary>
    /// Validador de posts
    /// </summary>
    public class PostValidator : AbstractValidator<PostDto>
    {
        /// <summary>
        /// Construtor de posts
        /// </summary>
        public PostValidator()
        {
            RuleFor(p => p.Title)
                .NotNull()
                .NotEmpty().WithMessage("Preencha Título, p.f.")
                .Length(5, 60).WithMessage("Tamanho entre 5 e 60 caracteres");
            RuleFor(p => p.Introduction)
                .NotNull()
                .NotEmpty().WithMessage("Preencha Introdução, p.f.");
            RuleFor(p => p.BodyText)
                .NotNull()
                .NotEmpty().WithMessage("Preencha corpo do post, p.f.");
            RuleFor(p => p.Image)
                .NotNull()
                .NotEmpty().WithMessage("Selecione imagem, p.f.");
        }
    }
}
