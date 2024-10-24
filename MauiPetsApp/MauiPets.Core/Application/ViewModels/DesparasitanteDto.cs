﻿using MauiPetsApp.Core.Application.Formatting;

namespace MauiPetsApp.Core.Application.ViewModels
{
    public class DesparasitanteDto
    {
        public int Id { get; set; }
        public int IdPet { get; set; }
        public string Tipo { get; set; } = string.Empty; // "I" - Interno - "E" - Externo
        public string Marca { get; set; } = string.Empty;
        public string DataAplicacao { get; set; } = string.Empty;
        //public string DataProximaAplicacao { get; set; } = string.Empty;
        private string? dataProximaAplicacao;

        public string DataProximaAplicacao
        {
            get { return dataProximaAplicacao ?? ""; }
            set { dataProximaAplicacao = DataFormat.DateParse(DataAplicacao).AddMonths(3).ToShortDateString(); }
        }

    }
}
