using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.Despesas;
using FluentValidation;

namespace DaisyPets.WebApi.Validators
{
    /// <summary>
    /// Construtor de validador de Rações
    /// </summary>
    public class DespesaValidator : AbstractValidator<DespesaDto>
    {
        /// <summary>
        /// Validador de contactos
        /// </summary>
        public DespesaValidator()
        {
            RuleFor(p => p.ValorPago)
                .NotNull()
                .NotEmpty().WithMessage("Preencha Valor Pago, p.f.")
                .GreaterThan(0).WithMessage("valor pago deve ser positivo")
                .Must(BeAValidNumber).WithMessage("Quantidade diária deve ser um valor numérico")
                .GreaterThan(0).WithMessage("Quantidade diária deve ter um valor positivo");
            RuleFor(p => p.DataMovimento)
                .Must(BeAValidDate).WithMessage("Data da compra deverá ser inferior à data corrente");
        }

        #region Custom Validators

        /// <summary>
        ///  custom valida
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        protected bool BeAValidNumber(decimal numero)
        {
            if (!DataFormat.IsInteger(numero))
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

