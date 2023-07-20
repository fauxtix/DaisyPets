using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Notifications;
using Syncfusion.Blazor.Popups;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.Web.Blazor.Pages.CodeBehind.Pets
{
    public class PetsMainPageBase : ComponentBase, IDisposable
    {
        [Inject] IConfiguration? config { get; set; }
        [Inject] public IStringLocalizer<App>? L { get; set; }
        [Inject] ILogger<App>? logger { get; set; } = null;
        [Inject] public IWebHostEnvironment _env { get; set; }

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
        protected bool ShowFileVisibility { get; set; }


        protected int PetId { get; set; }
        protected int PetSizeId { get; set; }
        protected int PetSituationId { get; set; }
        protected int PetBreedId { get; set; }
        protected int PetTemperamentId { get; set; }
        protected int PetSpecieId { get; set; }

        protected int PetDocumentId { get; set; }
        protected int PetDewormerId { get; set; }
        protected int PetVaccineId { get; set; }
        protected int PetFoodId { get; set; }
        protected int PetConsultationId { get; set; }


        protected bool ErrorVisibility { get; set; } = false;
        protected bool AlertVisibility { get; set; } = false;
        protected string? WarningMessage { get; set; }
        protected string? WarningTitle { get; set; }
        protected bool WarningVisibility { get; set; }
        protected List<string> ValidationsMessages = new();

        protected string? alertTitle = "";

        protected string? ToastTitle;
        protected string? ToastMessage;
        protected string? ToastCss;
        protected string? ToastIcon;

        protected string ToastContent = "";
        protected string ToastCssClass = "";


        protected DialogEffect Effect = DialogEffect.Zoom;
        protected SfGrid<PetVM>? petsGridObj { get; set; }

        protected SfToast? ToastObj { get; set; }
        protected string? documentFilePath { get; set; }


        protected override async Task OnInitializedAsync()
        {
            PetId = 0;
            PetSizeId = 0;
            PetSituationId = 0;
            PetBreedId = 0;
            PetTemperamentId = 0;
            PetSpecieId = 0;

            PetDocumentId = 0;
            documentFilePath = "";

            urlBaseAddress = config?["ApiSettings:UrlBase"];
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

        private async Task<List<string>> ValidatePetDocument()
        {
            var validatorEndpoint = $"{urlBaseAddress}/Document/ValidateDocument";
            using (HttpClient httpClient = new HttpClient())
            {

                var response = await httpClient.PostAsJsonAsync(validatorEndpoint, SelectedDocument);
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
        private async Task<List<string>> ValidatePetVaccine()
        {
            var validatorEndpoint = $"{urlBaseAddress}/Vacinacao/ValidateVaccine";
            using (HttpClient httpClient = new HttpClient())
            {

                var response = await httpClient.PostAsJsonAsync(validatorEndpoint, SelectedVaccine);
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
            var validationMessages = await ValidateData();
            if (validationMessages.Any())
            {
                ValidationsMessages = validationMessages;
                ErrorVisibility = true;
                return false;
            }

            var url = $"{urlBaseAddress}/Pets";

            //var currentCulture = CultureInfo.CurrentCulture;
            //var culture = currentCulture.Name;
            //if (culture.ToLower().Contains("us"))
            //{
            //    DateTime date = DateTime.Parse(SelectedPet.DataNascimento, currentCulture);
            //    SelectedPet.DataNascimento = date.ToString(currentCulture);
            //}

            if (RecordMode == OpcoesRegisto.Gravar)
            {
                ToastTitle = $"{L["btnSalvar"]} {L["Pet_Title"]}";
                try
                {

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var result = await httpClient.PutAsJsonAsync($"{url}/{PetId}", SelectedPet);
                        var success = result.IsSuccessStatusCode;
                        if (!success)
                        {
                            AlertVisibility = true;
                            alertTitle = L["FalhaGravacaoRegisto"];
                            WarningMessage = $"{L["MSG_ApiError"]}";
                        }
                        else
                        {
                            ToastCss = "e-toast-success";
                            ToastMessage = $"{L["SuccessUpdate"]}";
                            ToastIcon = "fas fa-check";
                        }

                        Pets = await GetAllPets();
                        await Task.Delay(100);
                        await ToastObj.ShowAsync();

                        await InvokeAsync(StateHasChanged);
                        AddEditPetVisibility = false;
                        return success;
                    }
                }
                catch (Exception exc)
                {
                    logger?.LogError(exc.Message, $"{L["MSG_ApiError"]}");
                    return false;
                }
            }
            else // Insert
            {
                ToastTitle = $"{L["creationMsg"]} {L["Pet_Title"]}";
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var result = await httpClient.PostAsJsonAsync(url, SelectedPet);
                        var success = result.IsSuccessStatusCode;
                        if (!success)
                        {
                            AlertVisibility = true;
                            alertTitle = L["FalhaCriacaoRegisto"];
                            WarningMessage = $"{L["MSG_ApiError"]}";
                        }
                        else
                        {
                            ToastCss = "e-toast-success";
                            ToastMessage = $"{L["SuccessInsert"]}";
                            ToastIcon = "fas fa-check";
                        }

                        AddEditPetVisibility = false;

                        await Task.Delay(100);
                        await ToastObj.ShowAsync();
                        await InvokeAsync(StateHasChanged);

                        Pets = await GetAllPets();

                        return success;
                    }
                }
                catch (Exception exc)
                {
                    logger?.LogError(exc.Message, $"{L["MSG_ApiError"]}");
                    return false;
                }
            }
        }

        public async Task<bool> SavePetDocument()
        {
            var validationMessages = await ValidatePetDocument();
            if (validationMessages.Any())
            {
                ValidationsMessages = validationMessages;
                ErrorVisibility = true;
                return false;
            }

            var url = $"{urlBaseAddress}/Document";

            if (RecordMode == OpcoesRegisto.Gravar)
            {
                ToastTitle = $"{L["btnSalvar"]} {L["Pet_Title"]}";
                try
                {

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var result = await httpClient.PutAsJsonAsync($"{url}/{PetDocumentId}", SelectedDocument);
                        var success = result.IsSuccessStatusCode;
                        if (!success)
                        {
                            AlertVisibility = true;
                            alertTitle = L["FalhaGravacaoRegisto"];
                            WarningMessage = $"{L["MSG_ApiError"]}";
                        }
                        else
                        {
                            ToastCss = "e-toast-success";
                            ToastMessage = $"{L["SuccessUpdate"]}";
                            ToastIcon = "fas fa-check";
                        }

                        Pets = await GetAllPets();
                        await Task.Delay(100);
                        await ToastObj.ShowAsync();

                        await InvokeAsync(StateHasChanged);
                        AddEditDocumentVisibility = false;
                        return success;
                    }
                }
                catch (Exception exc)
                {
                    logger?.LogError(exc.Message, $"{L["MSG_ApiError"]}");
                    return false;
                }
            }
            else // Insert
            {
                ToastTitle = $"{L["creationMsg"]} {L["Pet_Title"]}";
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var result = await httpClient.PostAsJsonAsync(url, SelectedDocument);
                        var success = result.IsSuccessStatusCode;
                        if (!success)
                        {
                            AlertVisibility = true;
                            alertTitle = L["FalhaCriacaoRegisto"];
                            WarningMessage = $"{L["MSG_ApiError"]}";
                        }
                        else
                        {
                            ToastCss = "e-toast-success";
                            ToastMessage = $"{L["SuccessInsert"]}";
                            ToastIcon = "fas fa-check";
                        }

                        AddEditDocumentVisibility = false;

                        await Task.Delay(100);
                        await ToastObj.ShowAsync();
                        await InvokeAsync(StateHasChanged);

                        Pets = await GetAllPets();

                        return success;
                    }
                }
                catch (Exception exc)
                {
                    logger?.LogError(exc.Message, $"{L["MSG_ApiError"]}");
                    return false;
                }
            }
        }

        public async Task<bool> SavePetVaccine()
        {
            var validationMessages = await ValidatePetVaccine();
            if (validationMessages.Any())
            {
                ValidationsMessages = validationMessages;
                ErrorVisibility = true;
                return false;
            }

            var url = $"{urlBaseAddress}/Vacinacao";

            if (RecordMode == OpcoesRegisto.Gravar)
            {
                ToastTitle = $"{L["btnSalvar"]} {L["Pet_Vaccines"]}";
                try
                {

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var result = await httpClient.PutAsJsonAsync($"{url}/{PetVaccineId}", SelectedVaccine);
                        var success = result.IsSuccessStatusCode;
                        if (!success)
                        {
                            AlertVisibility = true;
                            alertTitle = L["FalhaGravacaoRegisto"];
                            WarningMessage = $"{L["MSG_ApiError"]}";
                        }
                        else
                        {
                            ToastCss = "e-toast-success";
                            ToastMessage = $"{L["SuccessUpdate"]}";
                            ToastIcon = "fas fa-check";
                        }

                        Pets = await GetAllPets();
                        await Task.Delay(100);
                        await ToastObj.ShowAsync();

                        await InvokeAsync(StateHasChanged);
                        AddEditVaccineVisibility = false;
                        return success;
                    }
                }
                catch (Exception exc)
                {
                    logger?.LogError(exc.Message, $"{L["MSG_ApiError"]}");
                    return false;
                }
            }
            else // Insert
            {
                ToastTitle = $"{L["creationMsg"]} {L["Pet_Vaccines"]}";
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var result = await httpClient.PostAsJsonAsync(url, SelectedVaccine);
                        var success = result.IsSuccessStatusCode;
                        if (!success)
                        {
                            AlertVisibility = true;
                            alertTitle = L["FalhaCriacaoRegisto"];
                            WarningMessage = $"{L["MSG_ApiError"]}";
                        }
                        else
                        {
                            ToastCss = "e-toast-success";
                            ToastMessage = $"{L["SuccessInsert"]}";
                            ToastIcon = "fas fa-check";
                        }

                        AddEditVaccineVisibility = false;

                        await Task.Delay(100);
                        await ToastObj.ShowAsync();
                        await InvokeAsync(StateHasChanged);

                        Pets = await GetAllPets();

                        return success;
                    }
                }
                catch (Exception exc)
                {
                    logger?.LogError(exc.Message, $"{L["MSG_ApiError"]}");
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
                    logger?.LogError(exa.Message, $"{L["MSG_ApiError"]}");

                    return Enumerable.Empty<PetVM>();
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex.Message, $"{L["MSG_ApiError"]}");
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
                    logger?.LogError(exa.Message, $"{L["MSG_ApiError"]}");

                    return Enumerable.Empty<VacinaVM>();
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex.Message, $"{L["MSG_ApiError"]}");

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
                catch (HttpRequestException exa)
                {
                    logger?.LogError(exa.Message, $"{L["MSG_ApiError"]}");

                    return Enumerable.Empty<DocumentoVM>();
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex.Message, $"{L["MSG_ApiError"]}");
                    return Enumerable.Empty<DocumentoVM>();
                }
            }
        }

        protected IEnumerable<DocumentoVM>? GetPetDocumentsHistory(int id)
        {

            IEnumerable<DocumentoVM> documentsList =
                Task.Run(async () => await GetPetDocuments(id)).Result ??
                Enumerable.Empty<DocumentoVM>();
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
                catch (HttpRequestException exa)
                {
                    logger?.LogError(exa.Message, $"{L["MSG_ApiError"]}");

                    return Enumerable.Empty<ConsultaVeterinarioVM>();
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex.Message, $"{L["MSG_ApiError"]}");
                    return Enumerable.Empty<ConsultaVeterinarioVM>();
                }
            }
        }

        protected IEnumerable<ConsultaVeterinarioVM>? GetPetConsultationsHistory(int id)
        {

            IEnumerable<ConsultaVeterinarioVM> documentsList =
                Task.Run(async () => await GetPetConsultations(id)).Result ??
                Enumerable.Empty<ConsultaVeterinarioVM>();
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
                catch (HttpRequestException exa)
                {
                    logger?.LogError(exa.Message, $"{L["MSG_ApiError"]}");

                    return Enumerable.Empty<RacaoVM>();
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex.Message, $"{L["MSG_ApiError"]}");

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
                    logger?.LogError(exa.Message, $"{L["MSG_ApiError"]}");

                    return Enumerable.Empty<DesparasitanteVM>();
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex.Message, $"{L["MSG_ApiError"]}");
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
            NewCaption = $"{L["NewMsg"]} {L["Pet_Title"]}";
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
            NewCaption = $"{L["NewMsg"]} ({L["TituloDocumento"]})";
            modulo = Modules.Documents;

            SelectedDocument = new()
            {
                PetId = PetId,
                CreatedOn = DateTime.Now.ToShortDateString(),
                Description = "",
                DocumentPath = "",
                Title = ""
            };

            AddEditDocumentVisibility = true;
        }

        private async Task<DocumentoDto?> GetPetDocument()
        {

            using (HttpClient httpClient = new HttpClient())
            {
                var url = $"{urlBaseAddress}/Document";

                try
                {
                    var document = await httpClient.GetFromJsonAsync<DocumentoDto>($"{url}/{PetDocumentId}");
                    return document;
                }
                catch (HttpRequestException exa)
                {
                    logger?.LogError(exa.Message, $"{L["MSG_ApiError"]}");

                    return new DocumentoDto();
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex.Message, $"{L["MSG_ApiError"]}");
                    return new DocumentoDto();
                }
            }
        }

        public void onAddConsultation(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            RecordMode = OpcoesRegisto.Inserir;
            NewCaption = $"{L["NewMsg"]} ({L["Pet_Consultations"]})";
            modulo = Modules.Consultations;

            SelectedConsultation = new()
            {
                DataConsulta = DateTime.Now.ToShortDateString(),
                Diagnostico = "",
                Motivo = "",
                Notas = "",
                Tratamento = "",
                IdPet = PetId
            };

            AddEditConsultationVisibility = true;
        }

        public void onAddVaccine(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            RecordMode = OpcoesRegisto.Inserir;
            NewCaption = $"{L["NewMsg"]} ({L["Pet_Vaccines"]})";
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
            NewCaption = $"{L["NewMsg"]} ({L["Pet_Food"]})";
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
            NewCaption = $"{L["NewMsg"]} ({L["Pet_Dewormers"]})";
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
                EditCaption = $"{L["EditMsg"]} {L["Pet_Title"]}";
                RecordMode = OpcoesRegisto.Gravar;
            }

            if (args.CommandColumn.Type == CommandButtonType.Delete)
            {
                WarningTitle = $"{L["DeleteMsg"]} {L["Pet_Title"]}?";
                DeletePetVisibility = true;
                DeleteCaption = SelectedPet?.Nome;
            }
        }

        public async Task OnPetDocumentCommandClicked(CommandClickEventArgs<DocumentoVM> args)
        {
            modulo = Modules.Documents;
            PetDocumentId = args.RowData.Id;
            SelectedDocument = await GetSelectedDocument(PetDocumentId);
            if (args.CommandColumn.Type == CommandButtonType.None)
            {
                RecordMode = OpcoesRegisto.Info;
                var fileName = args.RowData.DocumentPath;
                string? fileExtension = Path.GetExtension(fileName);
                documentFilePath = Path.Combine(_env.WebRootPath, "uploads", "PetDocuments", fileName!);
                ShowFileVisibility = true;
            }

            if (args.CommandColumn.Type == CommandButtonType.Edit)
            {
                PetDocumentId = args.RowData.Id;

                AddEditDocumentVisibility = true;
                EditCaption = $"{L["EditMsg"]} {L["Pet_Documents"]}";
                RecordMode = OpcoesRegisto.Gravar;
            }

        }

        public async Task OnPetDewormerCommandClicked(CommandClickEventArgs<DesparasitanteVM> args)
        {
            modulo = Modules.Dewormers;
            PetDewormerId = args.RowData.Id;
            SelectedDewormer = await GetSelectedDewormer(PetDewormerId);

            if (args.CommandColumn.Type == CommandButtonType.Edit)
            {
                AddEditDewormerVisibility = true;
                EditCaption = $"{L["EditMsg"]} {L["Pet_Title"]}";
                RecordMode = OpcoesRegisto.Gravar;
            }
        }

        public async Task OnPetVaccineCommandClicked(CommandClickEventArgs<VacinaVM> args)
        {
            modulo = Modules.Vaccines;
            PetVaccineId = args.RowData.Id;
            SelectedVaccine = await GetSelectedVaccine(PetVaccineId);

            if (args.CommandColumn.Type == CommandButtonType.Edit)
            {
                AddEditVaccineVisibility = true;
                EditCaption = $"{L["EditMsg"]} {L["Pet_Vaccines"]}";
                RecordMode = OpcoesRegisto.Gravar;
            }
        }

        public async Task OnPetFoodCommandClicked(CommandClickEventArgs<RacaoVM> args)
        {
            modulo = Modules.PetFood;
            PetFoodId = args.RowData.Id;
            SelectedPetFood = await GetSelectedDogFeed(PetFoodId);

            if (args.CommandColumn.Type == CommandButtonType.Edit)
            {
                AddEditPetFoodVisibility = true;
                EditCaption = $"{L["EditMsg"]} {L["Pet_Food"]}";
                RecordMode = OpcoesRegisto.Gravar;
            }
        }

        public async Task OnPetConsultationCommandClicked(CommandClickEventArgs<ConsultaVeterinarioVM> args)
        {
            modulo = Modules.Consultations;
            PetConsultationId = args.RowData.Id;
            SelectedConsultation = await GetSelectedConsultation(PetConsultationId);

            if (args.CommandColumn.Type == CommandButtonType.Edit)
            {
                AddEditPetFoodVisibility = true;
                EditCaption = $"{L["EditMsg"]} {L["Pet_Consultations"]}";
                RecordMode = OpcoesRegisto.Gravar;
            }
        }



        private async Task<DocumentoDto?> GetSelectedDocument(int petDocumentId)
        {
            string url = $"{urlBaseAddress}/Document/{PetDocumentId}";
            using (HttpClient httpClient = new HttpClient())
            {
                var _document = await httpClient.GetFromJsonAsync<DocumentoDto>(url);
                return _document ?? new DocumentoDto();
            }
        }
        private async Task<DesparasitanteDto?> GetSelectedDewormer(int petDewormerId)
        {
            string url = $"{urlBaseAddress}/Desparasitante/{petDewormerId}";
            using (HttpClient httpClient = new HttpClient())
            {
                var _dewormer = await httpClient.GetFromJsonAsync<DesparasitanteDto>(url);
                return _dewormer ?? new DesparasitanteDto();
            }
        }

        private async Task<RacaoDto?> GetSelectedDogFeed(int petDogFeedId)
        {
            string url = $"{urlBaseAddress}/Racao/{petDogFeedId}";
            using (HttpClient httpClient = new HttpClient())
            {
                var _petFood = await httpClient.GetFromJsonAsync<RacaoDto>(url);
                return _petFood ?? new RacaoDto();
            }
        }

        private async Task<VacinaDto?> GetSelectedVaccine(int petVaccineId)
        {
            string url = $"{urlBaseAddress}/Vacinacao/{petVaccineId}";
            using (HttpClient httpClient = new HttpClient())
            {
                var _petVaccine = await httpClient.GetFromJsonAsync<VacinaDto>(url);
                return _petVaccine ?? new VacinaDto();
            }
        }

        private async Task<ConsultaVeterinarioDto?> GetSelectedConsultation(int petConsultationId)
        {
            string url = $"{urlBaseAddress}/Consulta/{petConsultationId}";
            using (HttpClient httpClient = new HttpClient())
            {
                var _petConsultation = await httpClient.GetFromJsonAsync<ConsultaVeterinarioDto>(url);
                return _petConsultation ?? new ConsultaVeterinarioDto();
            }
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "Pets_Grid_pdfexport")  //Id is combination of Grid's ID and itemname
            {
                await petsGridObj!.ExportToPdfAsync();
                return;
            }

            if (args.Item.Text == "Expand all")
            {
                await petsGridObj.ExpandAllDetailRowAsync();
            }
            else if (args.Item.Text == "Collapse all")
            {
                await petsGridObj.CollapseAllDetailRowAsync();
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

        protected async Task ConfirmDeleteYes()
        {
            ToastTitle = $"{L["DeleteMsg"]} {L["Pet_Title"]}";

            await DeletePet();

            ToastCssClass = "e-toast-success";
            ToastContent = L["RegistoAnuladoSucesso"];

            await Task.Delay(200);
            await ToastObj!.ShowAsync();

        }

        private async Task DeletePet()
        {
            string url = $"{urlBaseAddress}/Pets/{PetId}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync(url);
                    response.EnsureSuccessStatusCode();
                    if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        ValidationsMessages = new List<string>() { L["FalhaAnulacaoRegisto"] };
                        ErrorVisibility = true;
                        return;
                    }

                    Pets = await GetAllPets();
                }

                DeletePetVisibility = false;
                alertTitle = "Apagar Pet";
                WarningMessage = L["SuccessDelete"];
                AlertVisibility = true;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message, $"{L["MSG_ApiError"]}");

                alertTitle = $"{L["DeleteMsg"]} {L["Pet_Title"]}";
                WarningMessage = $"Erro ({ex.Message})";
                AlertVisibility = true;
            }

        }


        protected async Task HideToast()
        {
            await ToastObj!.HideAsync();
        }

        public void Dispose()
        {
            sfUploader?.Dispose();
            ToastObj?.Dispose();
            petsGridObj?.Dispose();
        }
    }
}
