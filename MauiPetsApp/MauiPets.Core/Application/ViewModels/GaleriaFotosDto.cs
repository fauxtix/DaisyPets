﻿namespace MauiPetsApp.Core.Application.ViewModels
{
    public class GaleriaFotosDto
    {
        public int Id { get; set; }
        public int IdPet { get; set; }
        public string Imagem { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
    }
}
