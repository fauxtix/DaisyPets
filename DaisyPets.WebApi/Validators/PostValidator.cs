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
                .Length(5, 128).WithMessage("Tamanho entre 5 e 128 caracteres");
            RuleFor(p => p.Introduction)
                .NotNull()
                .NotEmpty().WithMessage("Preencha Introdução, p.f.");
            RuleFor(p => p.BodyText)
                .NotNull()
                .NotEmpty().WithMessage("Preencha corpo do post, p.f.")
                .When(p => string.IsNullOrEmpty(p.PostUrl));
            RuleFor(p => p.PostUrl)
                .NotNull()
                .NotEmpty().WithMessage("Preencha Url do post, p.f.")
                .When(p => string.IsNullOrEmpty(p.BodyText));
            RuleFor(p => p.Image)
                .NotNull()
                .NotEmpty().WithMessage("Selecione imagem, p.f.");
        }
    }
}
