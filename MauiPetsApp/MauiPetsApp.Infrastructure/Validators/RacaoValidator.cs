using FluentValidation;
using MauiPetsApp.Core.Application.Formatting;
using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPetsApp.Infrastructure.Validators
{
    /// <summary>
    /// Construtor de validador de Rações
    /// </summary>
    public class RacaoValidator : AbstractValidator<RacaoDto>
    {
        /// <summary>
        /// Validador de contactos
        /// </summary>
        public RacaoValidator()
        {
            RuleFor(p => p.Marca)
                .NotNull()
                .NotEmpty().WithMessage("Preencha campo Marca, p.f.");
            RuleFor(p => p.QuantidadeDiaria)
                .NotNull()
                .NotEmpty().WithMessage("Preencha quantida diária, p.f.")
                .Must(BeAValidNumber).WithMessage("Quantidade diária deve ser um valor numérico")
                .GreaterThan(0).WithMessage("Quantidade diária deve ter um valor positivo");
            RuleFor(p => p.DataCompra)
                .Must(BeAValidDate).WithMessage("Data da compra deverá ser inferior à data corrente");
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

