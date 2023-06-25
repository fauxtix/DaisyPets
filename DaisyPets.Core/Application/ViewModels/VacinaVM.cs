﻿namespace DaisyPets.Core.Application.ViewModels
{
    public class VacinaVM
    {
        public int Id { get; set; }
        public int IdPet { get; set; }
        public string DataToma { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int ProximaTomaEmMeses { get; set; }
        public string NomePet { get; set; } = string.Empty;

        public DateTime DataProximaToma
        {
            get { return DateTime.Parse(DataToma).AddMonths(ProximaTomaEmMeses); }
        }
        public int DiasParaProximaToma
        {
            get { return (int)(DateTime.Parse(DataToma).AddMonths(ProximaTomaEmMeses) - DateTime.Now).TotalDays; }
        }
    }
}
