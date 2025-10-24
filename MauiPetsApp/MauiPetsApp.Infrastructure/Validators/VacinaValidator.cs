using FluentValidation;
using MauiPetsApp.Core.Application.Formatting;
using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPetsApp.Infrastructure.Validators
{
    /// <summary>
    /// Construtor de validador de contactos
    /// </summary>
    public class VacinaValidator : AbstractValidator<VacinaDto>
    {
        /// <summary>
        /// Validador de contactos
        /// </summary>
        public VacinaValidator()
        {
            RuleFor(p => p.ProximaTomaEmMeses)
                .NotNull()
                .NotEmpty().WithMessage("Preencha 'Prx. toma em meses.")
                .GreaterThan(0).WithMessage("'Meses' deve ser positivo");

            RuleFor(p => p.IdTipoVacina)
                .NotNull()
                .GreaterThan(0).WithMessage("Preencha Tipo de vacina.");

            RuleFor(p => p.DataToma)
                .NotNull()
                .NotEmpty().WithMessage("Preencha 'Data da toma'.")
                .Must(BeAValidDate).WithMessage("'Data inválida / ou no futuro");

        }

        #region Custom Validators


        protected bool BeAValidDescription(string descricao)
        {
            descricao = descricao.Replace("'", " ").Replace("-", "").Replace(" ", "");
            return descricao.All(char.IsLetterOrDigit);
        }

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

