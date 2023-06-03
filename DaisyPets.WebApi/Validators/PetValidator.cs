using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.ViewModels;
using FluentValidation;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace DaisyPets.WebApi.Validators
{
    /// <summary>
    /// Validador de 'Pets'
    /// </summary>
    public class PetValidator : AbstractValidator<PetDto>
    {
        public PetValidator()
        {
            RuleFor(p => p.Nome)
                .NotNull()
                .NotEmpty().WithMessage("Nome é requerido")
                .Length(3, 40).WithMessage("Tamanho do Nome deve ter entre 3 e 40 caracteres");
            RuleFor(p => p.IdTemperamento)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("Temperamento é requerido");
            RuleFor(p => p.IdEspecie)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("Espécie é requerida");
            RuleFor(p => p.IdRaca)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("Raça é requerida");
            RuleFor(p => p.IdSituacao)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("Situação é requerida");
            RuleFor(p => p.IdTamanho)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0).WithMessage("Tamanho é requerido");
            RuleFor(p => p.DataNascimento)
                .Must(BeAValidDate).WithMessage("Data de Nascimento deverá ser inferior à data corrente");
            RuleFor(p => p.DataChip)
                .Must(BeAValidDate).WithMessage("Data do Chip deverá ser inferior à data corrente")
                .When(o => o.Chipado == 1);
            RuleFor(p => p.NumeroChip)
                .NotNull()
                .NotEmpty().WithMessage("Número do chip é requerido")
                .Length(15).WithMessage("Número do Chip deverá ter 15 dígitos")
                .Must(BeAValidNumber).WithMessage("Número do Chip deve ser numérico")
                .When(o => o.Chipado == 1);
        }

        /// <summary>
        /// Custom validation
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        protected bool BeAValidDate(string date)
        {
            var parsedDate = DateTime.Parse(date);
            if (!DataFormat.IsValidDate(parsedDate))
                return false;
            else if(parsedDate.Date >= DateTime.Now.Date)
                return false;


            return true;
        }

        /// <summary>
        /// Verifica se campo é numérico
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        protected bool BeAValidNumber(string numero)
        {
            if (!DataFormat.IsNumeric(numero))
                return false;
            else if(string.IsNullOrEmpty(numero))
                return false;
            return true;
        }


    }
}
