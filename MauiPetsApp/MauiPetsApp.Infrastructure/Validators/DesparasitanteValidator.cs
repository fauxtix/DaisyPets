using FluentValidation;
using MauiPetsApp.Core.Application.Formatting;
using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPetsApp.Infrastructure.Validators
{
    /// <summary>
    /// Construtor de validador de contactos
    /// </summary>
    public class DesparasitanteValidator : AbstractValidator<DesparasitanteDto>
    {
        /// <summary>
        /// Validador de contactos
        /// </summary>
        public DesparasitanteValidator()
        {
            RuleFor(p => p.Marca)
                .NotNull()
                .NotEmpty().WithMessage("Marca do desparasitante em falta");
            RuleFor(p => p.Tipo)
                .NotNull()
                .NotEmpty().WithMessage("Tipo de desparasitante em falta");
            RuleFor(p => p.DataAplicacao)
                .Must(BeAValidDate).WithMessage("Data da aplicação deverá ser inferior à data corrente");
            RuleFor(p => DataFormat.DateParse(p.DataProximaAplicacao))
                .GreaterThan(x => DataFormat.DateParse(x.DataAplicacao)).WithMessage("Próxima aplicação deverá ser posterior à data da aplicação");
        }

        #region Custom Validators

        /// <summary>
        /// Validate date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        protected bool BeAValidDate(string date)
        {
            //var parsedDate = DateTime.Parse(date);
            var isDate = DataFormat.IsValidDate(date);
            if (!isDate)
                return false;
            else if (DataFormat.DateParse(date) >= DateTime.Now.Date)
                return false;
            return true;
        }

        /// <summary>
        /// Validate type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected bool BeAValidType(string type)
        {
            if (type != "E" || type != "I") return false;

            return true;
        }

        #endregion
    }
}
