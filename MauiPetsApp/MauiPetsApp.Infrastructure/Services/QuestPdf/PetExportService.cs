using MauiPets.Core.Application.Interfaces.Services.QuestPdf;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using QuestPDF.Fluent;
using System.Text.Json;

namespace MauiPetsApp.Infrastructure.Services.QuestPdf
{
    public class PetExportService : IPetExportService
    {
        private readonly IVacinasService _vacinasService;
        public PetExportService(IVacinasService vacinasService)
        {
            _vacinasService = vacinasService;
        }

        public async Task<string> ExportToJsonAsync(PetVM pet)
        {
            string filePath = Path.Combine(Path.GetTempPath(), $"{pet.Nome}_registo.json");
            var json = JsonSerializer.Serialize(pet, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
            return filePath;
        }

        public async Task<string> ExportToPdfAsync(PetVM pet)
        {
            var petVaccines = await _vacinasService.GetPetVaccinesVMAsync(pet.Id);
            string filePath = Path.Combine(Path.GetTempPath(), $"{pet.Nome}_registo.pdf");

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text($"Ficha do {pet.Nome}").FontSize(24).Bold();

                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Nascimento: {pet.DataNascimento:dd/MM/yyyy}").FontSize(14);
                        col.Item().Text($"Raça: {pet.RacaAnimal}");

                        col.Item().Text("Vacinas:").FontSize(16).Bold();
                        if (petVaccines != null && petVaccines.Any())
                        {
                            foreach (var v in petVaccines)
                                col.Item().Text($"{v.Marca} - {v.DataProximaToma:dd/MM/yyyy}");
                        }
                        else
                        {
                            col.Item().Text("Sem vacinas registadas.").Italic().FontColor(QuestPDF.Infrastructure.Color.FromHex("#808080"));
                        }
                    });

                    page.Footer().AlignCenter().Text("Gerado por DaisyPetsApp");
                });
            }).GeneratePdf(filePath);

            await Task.CompletedTask;
            return filePath;
        }
    }
}