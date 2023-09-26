using FluentValidation;
using MauiPetsApp.Core.Application.Formatting;
using MauiPetsApp.Core.Application.ViewModels.Scheduler;

namespace MauiPetsApp.Infrastructure.Validators
{
    public class Appointmentvalidator : AbstractValidator<AppointmentDataDto>
    {
        /// <summary>
        /// Appointment validator
        /// </summary>
        public Appointmentvalidator()
        {
            RuleFor(p => p.Subject)
                 .NotNull()
                  .NotEmpty().WithMessage("Preencha assunto, p.f.");
            RuleFor(p => p.Description)
                .NotNull()
                .NotEmpty().WithMessage("Preencha campo Descrição, p.f.");

            RuleFor(p => p.StartTime)
                .NotNull()
                .NotEmpty().WithMessage("Preencha Data início, p.f.");

            RuleFor(p => p.EndTime)
                .Must(BeAValidDate).WithMessage("Data inválida")
                .When(p => p.EndTime != DateTime.MinValue);
        }

        protected bool BeAValidDate(DateTime date)
        {
            if (!DataFormat.IsValidDate(date))
                return false;
            else if (date > DateTime.Now.Date)
                return false;


            return true;
        }

    }
}
