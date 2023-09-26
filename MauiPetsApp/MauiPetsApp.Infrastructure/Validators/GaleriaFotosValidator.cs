using FluentValidation;
using MauiPetsApp.Core.Application.Formatting;
using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPetsApp.Infrastructure.Validators
{
    /// <summary>
    /// Construtor de validador de contactos
    /// </summary>
    public class GaleriaFotosValidator : AbstractValidator<GaleriaFotosDto>
    {
        /// <summary>
        /// Validador de contactos
        /// </summary>
        public GaleriaFotosValidator()
        {
            RuleFor(p => p.Imagem)
                .NotNull()
                .NotEmpty().WithMessage("Preencha campo Imagem, p.f.");

            RuleFor(p => p.Data)
                .Must(BeAValidDate).WithMessage("Data inválida");

        }

        #region Custom Validators

        /// <summary>
        /// Valida data da consulta
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        protected bool BeAValidDate(string date)
        {
            var parsedDate = DateTime.Parse(date);
            if (!DataFormat.IsValidDate(parsedDate))
                return false;
            else if (parsedDate.Date > DateTime.Now.Date)
                return false;
            return true;
        }

        #endregion

    }
}

