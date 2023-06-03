using DaisyPets.Core.Application.ViewModels;
using FluentValidation;

namespace DaisyPets.WebApi.Validators
{
    /// <summary>
    /// Construtor de validador de contactos
    /// </summary>
    public class ContactValidator : AbstractValidator<ContactoVM>
    {
        /// <summary>
        /// Validador de contactos
        /// </summary>
        public ContactValidator()
        {
            RuleFor(p => p.Nome)
                .NotNull()
                .NotEmpty().WithMessage("Preencha {PropertyName}, p.f.")
                .Length(5, 60).WithMessage("Tamanho entre 5 e 60 caracteres");


            RuleFor(p => p.Morada)
                .NotNull()
                .NotEmpty().WithMessage("Preencham Morada, p.f.")
                .Length(5, 60).WithMessage("Tamanho entre 5 e 60 caracteres");

            RuleFor(p => p.Movel)
                .NotNull()
                .NotEmpty().WithMessage("Preencha 'Móvel', p.f.")
                .Must(BeAValidContact).WithMessage("'Movel' deve ser numérico");

            RuleFor(p => p.eMail)
                .EmailAddress()
                .When(p => !string.IsNullOrEmpty(p.eMail))
                .WithMessage("e-mail inválido.");
        }

        #region Custom Validators

        protected bool BeAValidContact(string contacto)
        {
            return contacto.All(char.IsDigit);
        }

        protected bool BeAValidDescription(string descricao)
        {
            descricao = descricao.Replace("'", " ").Replace("-", "").Replace(" ", "");
            return descricao.All(char.IsLetterOrDigit);
        }

        #endregion

    }
}

