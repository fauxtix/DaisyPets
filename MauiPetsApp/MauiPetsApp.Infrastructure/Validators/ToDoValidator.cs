using FluentValidation;
using MauiPetsApp.Core.Application.Formatting;
using MauiPetsApp.Core.Application.TodoManager;

namespace MauiPetsApp.Infrastructure.Validators
{
    /// <summary>
    /// Validador de To Do lists
    /// </summary>
    public class ToDoValidator : AbstractValidator<ToDoDto>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public ToDoValidator()
        {
            RuleFor(p => p.Description)
                .NotNull()
                .NotEmpty().WithMessage("Preencha descrição, p.f.");
            RuleFor(p => p.StartDate)
                .Must(BeAValidDate).WithMessage("Data iinício nválida");
            RuleFor(x => DataFormat.DateParse(x.EndDate).Date >= DataFormat.DateParse(x.EndDate).Date);

            RuleFor(r => DataFormat.DateParse(r.EndDate))
                .NotNull()
                .NotEmpty().WithMessage("End date is required")
                .GreaterThan(r => DataFormat.DateParse(r.StartDate)).WithMessage("End date must after Start date");
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

            return true;
        }

        #endregion

    }
}
