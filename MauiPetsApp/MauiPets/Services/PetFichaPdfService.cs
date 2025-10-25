using MauiPetsApp.Core.Application.ViewModels;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

namespace MauiPets.Services;

public class PetFichaPdfService : IPetFichaPdfService
{
    public MemoryStream GenerateFichaPetPdfAsync(
        PetVM pet,
        IEnumerable<VacinaVM> vacinas,
        IEnumerable<DesparasitanteVM> desparasitantes,
        IEnumerable<RacaoVM> racoes,
        IEnumerable<ConsultaVeterinarioVM> marcacoes)
    {
        using var document = new PdfDocument();
        var page = document.Pages.Add();
        var graphics = page.Graphics;
        float y = 20;

        // Cabeçalho
        graphics.DrawString($"Ficha do Pet: {pet.Nome}", new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold), PdfBrushes.DarkBlue, new Syncfusion.Drawing.PointF(0, y));
        y += 35;

        // Foto
        //if (!string.IsNullOrEmpty(pet.Foto))
        //{
        //    try
        //    {
        //        using var imageStream = await FileSystem.OpenAppPackageFileAsync(pet.Foto);
        //        if (imageStream != null)
        //        {
        //            var pdfImage = new PdfBitmap(imageStream);
        //            graphics.DrawImage(pdfImage, 0, y, 100, 100);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Erro ao carregar imagem do recurso: {ex.Message}");
        //    }
        //    y += 110;
        //}

        // Dados principais do Pet
        graphics.DrawString($"Raça: {pet.RacaAnimal}", new PdfStandardFont(PdfFontFamily.Helvetica, 14), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, y));
        y += 22;
        graphics.DrawString($"Espécie: {pet.EspecieAnimal}", new PdfStandardFont(PdfFontFamily.Helvetica, 14), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, y));
        y += 22;
        graphics.DrawString($"Temperamento: {pet.TemperamentoAnimal}", new PdfStandardFont(PdfFontFamily.Helvetica, 14), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, y));
        y += 22;
        graphics.DrawString($"Situação: {pet.SituacaoAnimal}", new PdfStandardFont(PdfFontFamily.Helvetica, 14), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, y));
        y += 22;
        string dataNascStr = pet.DataNascimento?.Length >= 10 ? pet.DataNascimento[..10] : pet.DataNascimento;
        string dataChipStr = pet.DataChip?.Length >= 10 ? pet.DataChip[..10] : pet.DataChip;
        if (DateTime.TryParse(dataNascStr, out var dataNasc))
        {
            int idade = CalculateAge(dataNasc);
            graphics.DrawString($"Data Nascimento: {dataNasc:yyyy-MM-dd} ({idade} anos)", new PdfStandardFont(PdfFontFamily.Helvetica, 14), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, y));
        }
        else
        {
            graphics.DrawString($"Data Nascimento: {pet.DataNascimento}", new PdfStandardFont(PdfFontFamily.Helvetica, 14), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, y));
        }
        y += 22;
        graphics.DrawString($"Esterilizado: {(pet.Esterilizado ? "Sim" : "Não")}", new PdfStandardFont(PdfFontFamily.Helvetica, 14), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, y));
        y += 32;
        graphics.DrawString($"Chipado: {(pet.Chipado ? "Sim" : "Não")}", new PdfStandardFont(PdfFontFamily.Helvetica, 14), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, y));
        if (pet.Chipado)
        {
            y += 22;
            graphics.DrawString($"Data Chip: {dataChipStr}", new PdfStandardFont(PdfFontFamily.Helvetica, 14), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, y));
            y += 22;
            graphics.DrawString($"Nº Chip: {pet.NumeroChip}", new PdfStandardFont(PdfFontFamily.Helvetica, 14), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, y));
        }
        if (pet.Observacoes.Length > 0)
        {
            y += 30;
            graphics.DrawString($"Observações: {pet.Observacoes}", new PdfStandardFont(PdfFontFamily.Helvetica, 14), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, y));
        }
        y += 32;

        // Vacinas
        if (vacinas != null && vacinas.Any())
        {
            graphics.DrawString("Vacinas:", new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Bold), PdfBrushes.DarkGreen, new Syncfusion.Drawing.PointF(0, y));
            y += 22;
            foreach (var vacina in vacinas)
            {
                graphics.DrawString(
                    $"- {vacina.DataToma}: {vacina.Marca} (Próxima: {vacina.ProximaTomaEmMeses} meses)",
                    new PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new Syncfusion.Drawing.PointF(10, y));
                y += 18;
            }
            y += 10;
        }

        // Desparasitantes
        if (desparasitantes != null && desparasitantes.Any())
        {
            graphics.DrawString("Desparasitantes:", new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Bold), PdfBrushes.DarkGreen, new Syncfusion.Drawing.PointF(0, y));
            y += 22;
            foreach (var desp in desparasitantes)
            {
                var dataAplic = FormatDate(desp.DataAplicacao);
                var dataProx = FormatDate(desp.DataProximaAplicacao);
                graphics.DrawString(
                    $"- {dataAplic}: {desp.Marca} (Tipo: {desp.Tipo}, Próxima: {dataProx})",
                    new PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new Syncfusion.Drawing.PointF(10, y));
                y += 18;
            }
            y += 10;
        }

        // Rações
        if (racoes != null && racoes.Any())
        {
            graphics.DrawString("Rações:", new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Bold), PdfBrushes.DarkGreen, new Syncfusion.Drawing.PointF(0, y));
            y += 22;
            foreach (var racao in racoes)
            {
                graphics.DrawString(
                    $"- {racao.DataCompra}: {racao.Marca} ({racao.QuantidadeDiaria} vezes/dia)",
                    new PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new Syncfusion.Drawing.PointF(10, y));
                y += 18;
            }
            y += 10;
        }

        // Marcações
        if (marcacoes != null && marcacoes.Any())
        {
            var labelFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12, PdfFontStyle.Bold);
            var valueFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
            graphics.DrawString("Marcações/Veterinário:", new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Bold), PdfBrushes.DarkGreen, new Syncfusion.Drawing.PointF(0, y));
            y += 22;

            float margemEsquerda = 20;
            float larguraDisponivel = page.GetClientSize().Width - margemEsquerda - 20; // margem direita

            foreach (var m in marcacoes)
            {
                // Data (linha própria)
                graphics.DrawString(
                    $"Data: {m.DataConsulta}",
                    valueFont, PdfBrushes.Black, new Syncfusion.Drawing.PointF(margemEsquerda, y));
                y += 16;

                // Motivo (linha separada, com wrap)
                var motivoText = $"Motivo: {m.Motivo}";
                var motivoElement = new PdfTextElement(motivoText, valueFont);
                var motivoLayout = motivoElement.Draw(
                    page,
                    new Syncfusion.Drawing.RectangleF(margemEsquerda + 10, y, larguraDisponivel - 10, 1000),
                    new Syncfusion.Pdf.Graphics.PdfLayoutFormat() { Layout = PdfLayoutType.Paginate }
                );
                y = motivoLayout.Bounds.Bottom + 2;

                // Diagnóstico (linha separada, com wrap)
                var diagText = $"Diagnóstico: {m.Diagnostico}";
                var diagElement = new PdfTextElement(diagText, valueFont);
                var diagLayout = diagElement.Draw(
                    page,
                    new Syncfusion.Drawing.RectangleF(margemEsquerda + 10, y, larguraDisponivel - 10, 1000),
                    new Syncfusion.Pdf.Graphics.PdfLayoutFormat() { Layout = PdfLayoutType.Paginate }
                );
                y = diagLayout.Bounds.Bottom + 2;

                // Tratamento (linha separada, com wrap)
                var tratText = $"Tratamento: {m.Tratamento}";
                var tratElement = new PdfTextElement(tratText, valueFont);
                var tratLayout = tratElement.Draw(
                    page,
                    new Syncfusion.Drawing.RectangleF(margemEsquerda + 10, y, larguraDisponivel - 10, 1000),
                    new Syncfusion.Pdf.Graphics.PdfLayoutFormat() { Layout = PdfLayoutType.Paginate }
                );
                y = tratLayout.Bounds.Bottom + 10;
            }
        }
        var stream = new MemoryStream();
        document.Save(stream);
        stream.Position = 0;
        return stream;
    }

    private string FormatDate(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return "";
        // Extrai só a parte da data se vier com hora (ex: 2024-05-06T00:00:00)
        var data = raw.Length >= 10 ? raw[..10] : raw;
        if (DateTime.TryParse(data, out var dt))
            return dt.ToString("yyyy-MM-dd");
        // Se não conseguiu, tenta com o original
        if (DateTime.TryParse(raw, out dt))
            return dt.ToString("yyyy-MM-dd");
        // Se ainda não conseguiu, devolve como está
        return raw;
    }
    private int CalculateAge(DateTime birthDate)
    {
        DateTime today = DateTime.Today;
        int age = today.Year - birthDate.Year;

        // Verifica se já houve aniversário este ano
        if (birthDate.Date > today.AddYears(-age)) age--;

        return age;
    }

}