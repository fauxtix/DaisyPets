using FluentValidation;
using MauiPetsApp.Core.Application.Formatting;
using MauiPetsApp.Core.Application.ViewModels.Despesas;

namespace MauiPetsApp.Infrastructure.Validators
{
    /// <summary>
    /// Construtor do validador de despesas
    /// </summary>
    public class DespesaValidator : AbstractValidator<DespesaDto>
    {
        /// <summary>
        /// Validador de despesas
        /// </summary>
        public DespesaValidator()
        {
            RuleFor(p => p.ValorPago)
                .NotNull()
                .NotEmpty().WithMessage("Preencha Valor Pago, p.f.")
                .GreaterThan(0).WithMessage("Valor pago deve ser positivo")
                .Must(BeAValidNumber).WithMessage("Valor deve ser numérico");
            RuleFor(p => p.DataMovimento)
                .Must(BeAValidDate).WithMessage("Data do movimento deverá ser <= à data corrente");
            RuleFor(p => p.IdTipoDespesa)
                .GreaterThan(0).WithMessage("Deve escolher Tipo de despesa");
            RuleFor(p => p.IdCategoriaDespesa)
                .GreaterThan(0).WithMessage("Deve escolher Categoria");
        }

        #region Custom Validators

        /// <summary>
        ///  custom validator to see if the entry is numeric
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        protected bool BeAValidNumber(decimal numero)
        {
            if (!DataFormat.IsNumeric(numero))
                return false;

            return true;
        }

        /// <summary>
        /// Valida data
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

