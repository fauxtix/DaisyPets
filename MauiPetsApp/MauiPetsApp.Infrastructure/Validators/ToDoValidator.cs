using FluentValidation;
using MauiPetsApp.Core.Application.Formatting;
using MauiPetsApp.Core.Application.TodoManager;
using System.Reflection.Metadata.Ecma335;

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
            RuleFor(x => DataFormat.DateParse(x.EndDate!).Date >= DataFormat.DateParse(x.EndDate!).Date);

            RuleFor(r => r.StartDate!)
                .Must(NotUpdateStateInFuture)
                .When(r => r.Completed == 1)
                .WithMessage("Não pode dar tarefa como concluída.... está no futuro! Verifique, p.f.");
        }

        #region Custom Validators

        /// <summary>
        /// Valida data da consulta
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        protected bool BeAValidDate(string? date)
        {
            var parsedDate = DateTime.Parse(date!);
            if (!DataFormat.IsValidDate(parsedDate))
                return false;

            return true;
        }

        protected bool NotUpdateStateInFuture(string? date)
        {
            var parsedDate = DateTime.Parse(date!);
            if (parsedDate.Date > DateTime.Now.Date) return false;

            return true;
        }

        #endregion

    }
}
