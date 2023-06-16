using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.Despesas;
using FluentValidation;

namespace DaisyPets.WebApi.Validators
{
    /// <summary>
    /// Construtor de validador de contactos
    /// </summary>
    public class TipoDespesaValidator : AbstractValidator<TipoDespesaDto>
    {
        /// <summary>
        /// Validador de contactos
        /// </summary>
        public TipoDespesaValidator()
        {
            RuleFor(p => p.Descricao)
                .NotNull()
                .NotEmpty().WithMessage("Preencha campo 'descrição', p.f.");
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
            //else if (parsedDate.Date > DateTime.Now.Date)
            //    return false;


            return true;
        }

        #endregion

    }
}

