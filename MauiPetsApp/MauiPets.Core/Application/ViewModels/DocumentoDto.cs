﻿namespace MauiPetsApp.Core.Application.ViewModels
{
    public class DocumentoDto
    {
        public int Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? DocumentPath { get; set; }
        public string CreatedOn { get; set; } = string.Empty;
        public int PetId { get; set; }
    }
}
