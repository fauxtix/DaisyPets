using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.ViewModels;
using FluentValidation;

namespace DaisyPets.WebApi.Validators
{
    /// <summary>
    /// Construtor de validador de Documentos
    /// </summary>
    public class DocumentoValidator : AbstractValidator<DocumentoDto>
    {
        /// <summary>
        /// Validador de contactos
        /// </summary>
        public DocumentoValidator()
        {
            RuleFor(p => p.Title)
                .NotNull()
                .NotEmpty().WithMessage("Preencha Título, p.f.");
            RuleFor(p => p.DocumentPath)
                .NotNull()
                .NotEmpty().WithMessage("Selecione documento, p.f.");
        }

        #region Custom Validators

        protected bool BeAValidNumber(int numero)
        {
            if (!DataFormat.IsNumeric(numero))
                return false;
            return true;
        }

        protected bool BeAValidDate(string date)
        {
            var parsedDate = DateTime.Parse(date);
            if (!DataFormat.IsValidDate(parsedDate))
                return false;
            else if (parsedDate.Date >= DateTime.Now.Date)
                return false;


            return true;
        }

        #endregion

    }
}

