﻿using FluentValidation;
using MauiPetsApp.Core.Application.Formatting;
using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPetsApp.Infrastructure.Validators
{
    /// <summary>
    /// Construtor de validador de contactos
    /// </summary>
    public class ConsultaValidator : AbstractValidator<ConsultaVeterinarioDto>
    {
        /// <summary>
        /// Validador de contactos
        /// </summary>
        public ConsultaValidator()
        {
            RuleFor(p => p.Motivo)
                .NotNull()
                .NotEmpty().WithMessage("Motivo é um campo requerido");
            RuleFor(p => p.Diagnostico)
                .NotNull()
                .NotEmpty().WithMessage("Diagnóstico é um campo requerido");

            RuleFor(p => p.Tratamento)
                .NotNull()
                .NotEmpty().WithMessage("Tratamento é um campo requerido");

            RuleFor(p => p.DataConsulta)
                .Must(BeAValidDate).WithMessage("Data inválida");

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

