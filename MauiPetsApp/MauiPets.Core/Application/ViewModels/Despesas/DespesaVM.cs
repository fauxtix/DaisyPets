﻿namespace MauiPetsApp.Core.Application.ViewModels.Despesas
{
    public class DespesaVM
    {
        public int Id { get; set; }
        public string DataMovimento { get; set; } = string.Empty;
        public decimal ValorPago { get; set; } = 0;
        public string Descricao { get; set; } = string.Empty;
        public int IdTipoDespesa { get; set; }
        public string DescricaoTipoDespesa { get; set; } = string.Empty;
        public int IdCategoriaDespesa { get; set; }
        public string DescricaoCategoriaDespesa { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
        public string DataCriacao { get; set; } = string.Empty;
        public string TipoMovimento { get; set; } = "S"; // S - saída, E - Entrada (donativo)
    }
}
