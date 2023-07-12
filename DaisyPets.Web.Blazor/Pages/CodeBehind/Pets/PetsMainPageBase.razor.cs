using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System.Net.Http;
using System;
using System.Security.Policy;
using static DaisyPets.Core.Application.Enums.Common;
using Syncfusion.Blazor.Popups;
using Syncfusion.Blazor.Notifications;
using Syncfusion.Blazor.Calendars;

namespace DaisyPets.Web.Blazor.Pages.CodeBehind.Pets
{
    public class PetsMainPageBase : ComponentBase, IDisposable
    {
        [Inject] IConfiguration? config { get; set; }
        [Inject] public IStringLocalizer<App>? L { get; set; }
        [Inject] ILogger<App>? logger { get; set; } = null;

        protected string? urlBaseAddress;
        protected IEnumerable<PetVM>? Pets { get; set; }
        protected IEnumerable<LookupTableVM>? Species { get; set; }
        protected IEnumerable<LookupTableVM>? Breeds { get; set; }
        protected IEnumerable<LookupTableVM>? Situations { get; set; }
        protected IEnumerable<LookupTableVM>? Sizes { get; set; }
        protected IEnumerable<LookupTableVM>? Temperaments { get; set; }

        protected PetDto? SelectedPet { get; set; }
        protected VacinaDto? SelectedVaccine { get; set; }
        protected RacaoDto? SelectedPetFood { get; set; }
        protected DesparasitanteDto? SelectedDewormer { get; set; }
        protected ConsultaVeterinarioDto? SelectedConsultation { get; set; }
        protected DocumentoDto? SelectedDocument { get; set; }


        protected string? petsEndpoint;
        protected string? petVaccinesEndpoint;
        protected string? petDocumentsEndpoint;
        protected string? petFoodEndpoint;
        protected string? petDewormersEndpoint;
        protected string? petConsultationsEndpoint;



        protected SfUploader? sfUploader;
        protected int MaxFileSize = 10 * 1024 * 1024; // 10 MB
        protected string controllerName_Save = "api/upload";
        protected string controllerName_Remove = "api/remove";
        protected string? uploadedFile;
        protected OpcoesRegisto RecordMode { get; set; }
        protected string? NewCaption { get; set; }
        protected string? EditCaption { get; set; }
        protected string? DeleteCaption;
        protected Modules modulo { get; set; }
        protected bool AddEditPetVisibility { get; set; }
        protected bool AddEditDocumentVisibility { get; set; }
        protected bool AddEditPetFoodVisibility { get; set; }
        protected bool AddEditConsultationVisibility { get; set; }
        protected bool AddEditDewormerVisibility { get; set; }
        protected bool AddEditVaccineVisibility { get; set; }
        protected bool DeletePetVisibility { get; set; }
        protected bool DeleteDocumentVisibility { get; set; }
        protected bool DeletePetFoodVisibility { get; set; }
        protected bool DeleteConsultationVisibility { get; set; }
        protected bool DeleteDewormerVisibility { get; set; }
        protected bool DeleteVaccineVisibility { get; set; }

        protected int PetId { get; set; }
        protected int PetSizeId { get; set; }
        protected int PetSituationId { get; set; }
        protected int PetBreedId { get; set; }
        protected int PetTemperamentId { get; set; }
        protected int PetSpecieId { get; set; }

        protected bool ErrorVisibility { get; set; } = false;
        protected bool AlertVisibility { get; set; } = false;
        protected string? WarningMessage { get; set; }
        protected bool WarningVisibility { get; set; }
        protected List<string> ValidationsMessages = new();

        protected string? alertTitle = "";

        protected string? ToastTitle;
        protected string? ToastMessage;
        protected string? ToastCss;
        protected string? ToastIcon;

        protected DialogEffect Effect = DialogEffect.Zoom;
        protected SfToast? ToastObj { get; set; }


        protected override async Task OnInitializedAsync()
        {
            PetId = 0;
            PetSizeId = 0;
            PetSituationId = 0;
            PetBreedId = 0;
            PetTemperamentId = 0;
            PetSpecieId = 0;

            urlBaseAddress = config["ApiSettings:UrlBase"];
            petsEndpoint = $"{urlBaseAddress}/Pets/AllPetsVM";
            petVaccinesEndpoint = $"{urlBaseAddress}/Vacinacao/PetVaccines";
            petDocumentsEndpoint = $"{urlBaseAddress}/Document/AllDocumentsVM";
            petFoodEndpoint = $"{urlBaseAddress}/Racao/RacaoVMById";
            petDewormersEndpoint = $"{urlBaseAddress}/Desparasitante/PetDewormers";
            petConsultationsEndpoint = $"{urlBaseAddress}/Consulta/PetAppointments";

            Pets = await GetAllPets();

            Species = (await GetLookupData("Especie")).ToList();
            Breeds = (await GetLookupData("Raca")).ToList();
            Situations = (await GetLookupData("Situacao")).ToList();
            Sizes = (await GetLookupData("Tamanho")).ToList();
            Temperaments = (await GetLookupData("Temperamento")).ToList();

        }

        private async Task<List<string>> ValidateData()
        {
            var validatorEndpoint = $"{urlBaseAddress}/Pets/ValidatePets";
            using (HttpClient httpClient = new HttpClient())
            {

                var response = await httpClient.PostAsJsonAsync(validatorEndpoint, SelectedPet);
                if (response.IsSuccessStatusCode == false)
                {
                    var errors = response.Content.ReadFromJsonAsync<List<string>>().Result;
                    if (errors.Count() > 0)
                    {
                        return errors;
                    }

                    else

                        return new List<string>();
                }

                return new List<string>();
            }


        }
        public async Task<bool> SavePetData()
        {
            if ((await ValidateData()).Count > 0)
            {
                ErrorVisibility = true;
                return false;
            }

            var url = $"{urlBaseAddress}/Pets";

            if (RecordMode == OpcoesRegisto.Gravar)
            {
                ToastTitle = $"{L["btnSalvar"]} {L["TituloInquilino"]}";
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var result = await httpClient.PutAsJsonAsync($"{url}/{PetId}", SelectedPet);
                        var success = result.IsSuccessStatusCode;
                        if (!success)
                        {
                            AlertVisibility = true;
                            alertTitle = "Atualização de dados do inquilino";
                            WarningMessage = $"{L["MSG_ApiError"]}";
                        }
                        else
                        {
                            ToastCss = "e-toast-success";
                            ToastMessage = $"{L["SuccessUpdate"]}";
                            ToastIcon = "fas fa-check";
                        }

                        await InvokeAsync(StateHasChanged);
                        AddEditPetVisibility = false;
                        return success;
                    }
                }
                catch (Exception exc)
                {
                    logger.LogError(exc.Message, "Erro ao atualizar Pet)");
                    return false;
                }
            }
            else // Insert
            {
                ToastTitle = $"{L["creationMsg"]} {L["TituloInquilino"]}";
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var result = await httpClient.PostAsJsonAsync(url, SelectedPet);
                        var success = result.IsSuccessStatusCode;
                        if (!success)
                        {
                            AlertVisibility = true;
                            alertTitle = "Atualização de dados do inquilino";
                            WarningMessage = $"{L["MSG_ApiError"]}";
                        }
                        else
                        {
                            ToastCss = "e-toast-success";
                            ToastMessage = $"{L["SuccessUpdate"]}";
                            ToastIcon = "fas fa-check";
                        }

                        return success;
                    }
                }
                catch (Exception exc)
                {
                    logger.LogError(exc.Message, "Erro ao atualizar Pet)");
                    return false;
                }
            }
        }

        private async Task<IEnumerable<PetVM>?> GetAllPets()
        {

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetFromJsonAsync<IEnumerable<PetVM>>(petsEndpoint);
                    return response?.ToList();
                }
                catch (System.Net.Http.HttpRequestException exa)
                {
                    var msg = exa.Message;
                    return Enumerable.Empty<PetVM>();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    return Enumerable.Empty<PetVM>();
                }
            }
        }

        private async Task<IEnumerable<VacinaVM>?> GetPetVaccines(int Id)
        {

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetFromJsonAsync<IEnumerable<VacinaVM>>($"{petVaccinesEndpoint}/{Id}");
                    return response?.ToList();
                }
                catch (System.Net.Http.HttpRequestException exa)
                {
                    var msg = exa.Message;
                    return Enumerable.Empty<VacinaVM>();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    return Enumerable.Empty<VacinaVM>();
                }
            }
        }

        protected IEnumerable<VacinaVM>? GetPetVaccinesHistory(int id)
        {

            IEnumerable<VacinaVM> vaccinesList = Task.Run(async () => await GetPetVaccines(id)).Result ?? Enumerable.Empty<VacinaVM>();
            return vaccinesList;
        }

        private async Task<IEnumerable<DocumentoVM>?> GetPetDocuments(int Id)
        {

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetFromJsonAsync<IEnumerable<DocumentoVM>>($"{petDocumentsEndpoint}/{Id}");
                    return response?.ToList();
                }
                catch (System.Net.Http.HttpRequestException exa)
                {
                    var msg = exa.Message;
                    return Enumerable.Empty<DocumentoVM>();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    return Enumerable.Empty<DocumentoVM>();
                }
            }
        }

        protected IEnumerable<DocumentoVM>? GetPetDocumentsHistory(int id)
        {

            IEnumerable<DocumentoVM> documentsList = Task.Run(async () => await GetPetDocuments(id)).Result ?? Enumerable.Empty<DocumentoVM>();
            return documentsList;
        }

        private async Task<IEnumerable<ConsultaVeterinarioVM>?> GetPetConsultations(int Id)
        {

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetFromJsonAsync<IEnumerable<ConsultaVeterinarioVM>>($"{petConsultationsEndpoint}/{Id}");
                    return response?.ToList();
                }
                catch (System.Net.Http.HttpRequestException exa)
                {
                    var msg = exa.Message;
                    return Enumerable.Empty<ConsultaVeterinarioVM>();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    return Enumerable.Empty<ConsultaVeterinarioVM>();
                }
            }
        }

        protected IEnumerable<ConsultaVeterinarioVM>? GetPetConsultationsHistory(int id)
        {

            IEnumerable<ConsultaVeterinarioVM> documentsList = Task.Run(async () => await GetPetConsultations(id)).Result ?? Enumerable.Empty<ConsultaVeterinarioVM>();
            return documentsList;
        }

        private async Task<IEnumerable<RacaoVM>?> GetPetFood(int Id)
        {

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetFromJsonAsync<IEnumerable<RacaoVM>>($"{petFoodEndpoint}/{Id}");
                    return response?.ToList();
                }
                catch (System.Net.Http.HttpRequestException exa)
                {
                    var msg = exa.Message;
                    return Enumerable.Empty<RacaoVM>();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    return Enumerable.Empty<RacaoVM>();
                }
            }
        }

        protected IEnumerable<RacaoVM>? GetPetFoodHistory(int id)
        {

            IEnumerable<RacaoVM> documentsList = Task.Run(async () => await GetPetFood(id)).Result ?? Enumerable.Empty<RacaoVM>();
            return documentsList;
        }

        private async Task<IEnumerable<DesparasitanteVM>?> GetPetDewormers(int Id)
        {

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetFromJsonAsync<IEnumerable<DesparasitanteVM>>($"{petDewormersEndpoint}/{Id}");
                    return response?.ToList();
                }
                catch (System.Net.Http.HttpRequestException exa)
                {
                    var msg = exa.Message;
                    return Enumerable.Empty<DesparasitanteVM>();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    return Enumerable.Empty<DesparasitanteVM>();
                }
            }
        }

        protected IEnumerable<DesparasitanteVM>? GetPetDewormersHistory(int id)
        {

            IEnumerable<DesparasitanteVM> documentsList = Task.Run(async () => await GetPetDewormers(id)).Result ?? Enumerable.Empty<DesparasitanteVM>();
            return documentsList;
        }

        public void onAddPet(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            RecordMode = OpcoesRegisto.Inserir;
            NewCaption = $"{L["NewMsg"]} {L["TituloFiador"]}";
            modulo = Modules.Pets;

            SelectedPet = new()
            {
                Chip = "",
                Chipado = 0,
                Cor = "",
                DataChip = DateTime.Now.ToShortDateString(),
                DataNascimento = DateTime.Now.ToShortDateString(),
                DoencaCronica = "",
                Esterilizado = 1,
                Foto = "",
                Genero = "M",
                IdPeso = 0,
                IdEspecie = 0,
                IdRaca = 0,
                IdSituacao = 0,
                IdTamanho = 0,
                IdTemperamento = 0,
                Medicacao = "",
                Nome = "",
                NumeroChip = "",
                Observacoes = "",
                Padrinho = 0
            };

            AddEditPetVisibility = true;
        }

        public void onAddDocument(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            RecordMode = OpcoesRegisto.Inserir;
            NewCaption = $"{L["NewMsg"]} {L["TituloFiador"]}";
            modulo = Modules.Documents;

            SelectedDocument = new()
            {
                CreatedOn = DateTime.Now.ToShortDateString(),
                Description = "",
                DocumentPath = "",
                PetId = 0,
                Title = ""
            };

            AddEditDocumentVisibility = true;
        }
        public void onAddConsultation(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            RecordMode = OpcoesRegisto.Inserir;
            NewCaption = $"{L["NewMsg"]} {L["TituloFiador"]}";
            modulo = Modules.Consultations;

            SelectedConsultation = new()
            {
                DataConsulta = DateTime.Now.ToShortDateString(),
                Diagnostico = "",
                Motivo = "",
                Notas = "",
                Tratamento = "",
                IdPet = 0
            };

            AddEditConsultationVisibility = true;
        }

        public void onAddVaccine(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            RecordMode = OpcoesRegisto.Inserir;
            NewCaption = $"{L["NewMsg"]} {L["TituloFiador"]}";
            modulo = Modules.Vaccines;

            SelectedVaccine = new()
            {
                DataToma = DateTime.Now.ToString("yyyy-MM-dd"),
                IdPet = PetId,
                Marca = "",
                ProximaTomaEmMeses = 3,
            };

            AddEditVaccineVisibility = true;
        }
        public void onAddPetFood(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            RecordMode = OpcoesRegisto.Inserir;
            NewCaption = $"{L["NewMsg"]} {L["TituloFiador"]}";
            modulo = Modules.PetFood;

            SelectedPetFood = new()
            {
                DataCompra = DateTime.Now.ToString("yyyy-MM-dd"),
                IdPet = PetId,
                Marca = "",
                QuantidadeDiaria = 200
            };

            AddEditPetFoodVisibility = true;
        }

        public void onAddDewormer(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            RecordMode = OpcoesRegisto.Inserir;
            NewCaption = $"{L["NewMsg"]} {L["TituloFiador"]}";
            modulo = Modules.Dewormers;

            SelectedDewormer = new()
            {
                DataAplicacao = DateTime.Now.ToString("yyyy-MM-dd"),
                IdPet = PetId,
                Marca = "",
                DataProximaAplicacao = DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd"),
                Tipo = "I"
            };

            AddEditDewormerVisibility = true;
        }

        private async Task<IEnumerable<LookupTableVM>> GetLookupData(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
                return null;

            urlBaseAddress = config?["ApiSettings:UrlBase"];
            var lookupTablesEndpoint = $"{urlBaseAddress}/LookupTables/GetAllRecords/{tableName}";
            using (HttpClient httpClient = new HttpClient())
            {
                var result = await httpClient.GetFromJsonAsync<IEnumerable<LookupTableVM>>(lookupTablesEndpoint);
                if (result is null)
                {
                    return Enumerable.Empty<LookupTableVM>();
                }

                return result;
            }
        }

        protected async Task RowSelectHandler(RowSelectEventArgs<PetVM> args)
        {
            PetId = args.Data.Id;
            SelectedPet = await GetPetById(PetId);
        }

        public async Task OnPetCommandClicked(CommandClickEventArgs<PetVM> args)
        {
            modulo = Modules.Pets;

            if (args.CommandColumn.Type == CommandButtonType.Edit)
            {
                PetId = args.RowData.Id;
                SelectedPet = await GetPetById(PetId);

                AddEditPetVisibility = true;
                EditCaption = $"{L["EditMsg"]} {L["TituloInquilino"]}";
                RecordMode = OpcoesRegisto.Gravar;
            }

            if (args.CommandColumn.Type == CommandButtonType.Delete)
            {
                DeletePetVisibility = true;
                DeleteCaption = SelectedPet?.Nome;
            }
        }


        protected async Task<PetDto> GetPetById(int Id)
        {
            var url = $"{urlBaseAddress}/Pets/{Id}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var pet = await httpClient.GetFromJsonAsync<PetDto>(url);
                    if (pet?.Id > 0)
                    {
                        return pet;
                    }
                    else
                    {
                        return new PetDto();
                    }
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new PetDto();
            }
        }

        protected void OnActionCompleteHandler(Syncfusion.Blazor.Inputs.ActionCompleteEventArgs args)
        {
            if (args.FileData.Count() == 0) return;

            uploadedFile = args.FileData.Select(p => p.Name).FirstOrDefault();
            //HideUploadedFile = false;

            //Document!.URL = uploadedFile;

            StateHasChanged();
        }

        protected async Task HideToast()
        {
            await ToastObj!.HideAsync();
        }

        public void Dispose()
        {
            sfUploader?.Dispose();
            ToastObj?.Dispose();
        }
    }
}
