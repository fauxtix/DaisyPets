using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPets.Core.Application.Interfaces.Services.QuestPdf
{
    public interface IPetExportService
    {
        Task<string> ExportToJsonAsync(PetVM pet);
        Task<string> ExportToPdfAsync(PetVM pet);
    }
}