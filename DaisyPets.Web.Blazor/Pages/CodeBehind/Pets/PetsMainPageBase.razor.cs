using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Notifications;
using Syncfusion.Blazor.Popups;
using System.Net.Sockets;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.Web.Blazor.Pages.CodeBehind.Pets
{
    public class PetsMainPageBase : ComponentBase, IDisposable
    {
        [Inject] IConfiguration? config { get; set; }
        [Inject] public IStringLocalizer<App>? L { get; set; }
        [Inject] ILogger<App>? logger { get; set; } = null;
        [Inject] public IWebHostEnvironment _env { get; set; }
        [Inject] IJSRuntime Runtime { get; set; }

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
        protected Modules Module { get; set; }
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
        protected string? PetName { get; set; }
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

        protected string? AlertTitle = "";

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

        protected bool firstrender { get; set; } = true;

        protected IDictionary<int, PetVM> ExpandedRows = new Dictionary<int, PetVM>();

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

        private async Task<List<string>> ValidatePet()
        {
            var validatorEndpoint = $"{urlBaseAddress}/Pets/ValidatePets";
            var errors = await Validate(SelectedPet!, validatorEndpoint);

            return errors.Count() > 0 ? errors : new List<string>();
        }

        private async Task<List<string>> ValidatePetDocument()
        {
            var validatorEndpoint = $"{urlBaseAddress}/Document/ValidateDocument";
            var errors = await Validate(SelectedDocument!, validatorEndpoint);

            return errors.Count() > 0 ? errors : new List<string>();
        }
        private async Task<List<string>> ValidatePetVaccine()
        {
            var validatorEndpoint = $"{urlBaseAddress}/Vacinacao/ValidateVaccine";

            var errors = await Validate(SelectedVaccine!, validatorEndpoint);
            return errors.Count() > 0 ? errors : new List<string>();
        }

        private async Task<List<string>> ValidatePetFood()
        {
            var validatorEndpoint = $"{urlBaseAddress}/Racao/ValidateRacao";

            var errors = await Validate(SelectedPetFood!, validatorEndpoint);
            return errors.Count() > 0 ? errors : new List<string>();
        }

        private async Task<List<string>> ValidatePetDewormer()
        {
            var validatorEndpoint = $"{urlBaseAddress}/Desparasitante/ValidateDesparasitantes";

            var errors = await Validate(SelectedDewormer!, validatorEndpoint);
            return errors.Count() > 0 ? errors : new List<string>();
        }
        private async Task<List<string>> ValidatePetConsultation()
        {
            var validatorEndpoint = $"{urlBaseAddress}/Consulta/ValidateAppointment";

            var errors = await Validate(SelectedConsultation!, validatorEndpoint);
            return errors.Count() > 0 ? errors : new List<string>();
        }

        public async Task<bool> SavePetData()
        {
            var validationMessages = await ValidatePet();
            if (validationMessages.Any())
            {
                ValidationsMessages = validationMessages;
                ErrorVisibility = true;
                return false;
            }

            var url = $"{urlBaseAddress}/Pets";
            var result = await Save(SelectedPet!, url);
            return result;
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
            var result = await Save(SelectedDocument!, url);
            return result;
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

            var result = await Save(SelectedVaccine!, url);
            return result;
        }

        public async Task<bool> SavePetFood()
        {
            var validationMessages = await ValidatePetFood();
            if (validationMessages.Any())
            {
                ValidationsMessages = validationMessages;
                ErrorVisibility = true;
                return false;
            }

            var url = $"{urlBaseAddress}/Racao";
            var result = await Save(SelectedPetFood!, url);
            return result;
        }

        public async Task<bool> SavePetDewormer()
        {
            var validationMessages = await ValidatePetDewormer();
            if (validationMessages.Any())
            {
                ValidationsMessages = validationMessages;
                ErrorVisibility = true;
                return false;
            }

            var url = $"{urlBaseAddress}/Desparasitante";
            var result = await Save(SelectedDewormer!, url);
            return result;
        }

        public async Task<bool> SavePetConsultation()
        {
            var validationMessages = await ValidatePetConsultation();
            if (validationMessages.Any())
            {
                ValidationsMessages = validationMessages;
                ErrorVisibility = true;
                return false;
            }

            var url = $"{urlBaseAddress}/Consulta";
            var result = await Save(SelectedConsultation!, url);
            return result;
        }


        private async Task<IEnumerable<PetVM>?> GetAllPets()
        {
            try
            {
                var result = await GetData<PetVM>(petsEndpoint!);
                return result.ToList();

            }
            catch (SocketException socketEx)
            {
                AlertTitle = "Erro ao aceder ao servidor ";
                AlertVisibility = true;
                WarningMessage = socketEx.Message;
                return Enumerable.Empty<PetVM>();
            }
            catch (Exception ex)
            {
                AlertTitle = "Erro ao aceder ao servidor ";
                AlertVisibility = true;
                WarningMessage = ex.Message;
                return Enumerable.Empty<PetVM>();
            }
        }

        private async Task<IEnumerable<VacinaVM>?> GetPetVaccines(int Id)
        {
            var url = $"{petVaccinesEndpoint}/{Id}";
            var result = await GetData<VacinaVM>(url!);
            return result.ToList();
        }

        protected IEnumerable<VacinaVM>? GetPetVaccinesHistory(int id)
        {

            IEnumerable<VacinaVM> vaccinesList = Task.Run(async () => await GetPetVaccines(id)).Result ?? Enumerable.Empty<VacinaVM>();
            return vaccinesList;
        }

        private async Task<IEnumerable<DocumentoVM>?> GetPetDocuments(int Id)
        {
            var url = $"{petDocumentsEndpoint}/{Id}";
            var result = await GetData<DocumentoVM>(url!);
            return result.ToList();
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
            var url = $"{petConsultationsEndpoint}/{Id}";
            var result = await GetData<ConsultaVeterinarioVM>(url!);
            return result.ToList();
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
            var url = $"{petFoodEndpoint}/{Id}";
            var result = await GetData<RacaoVM>(url!);
            return result.ToList();
        }

        protected IEnumerable<RacaoVM>? GetPetFoodHistory(int id)
        {

            IEnumerable<RacaoVM> documentsList = Task.Run(async () => await GetPetFood(id)).Result ?? Enumerable.Empty<RacaoVM>();
            return documentsList;
        }

        private async Task<IEnumerable<DesparasitanteVM>?> GetPetDewormers(int Id)
        {
            var url = $"{petDewormersEndpoint}/{Id}";
            var result = await GetData<DesparasitanteVM>(url!);
            return result.ToList();
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
            Module = Modules.Pets;

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
            Module = Modules.Documents;

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
            Module = Modules.Consultations;

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
            Module = Modules.Vaccines;

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
            Module = Modules.PetFood;

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
            Module = Modules.Dewormers;

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
            try
            {
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
            catch (SocketException socketEx)
            {
                AlertTitle = "Erro ao aceder ao servidor ";
                AlertVisibility = true;
                WarningMessage = socketEx.Message;
                return Enumerable.Empty<LookupTableVM>();
            }
            catch (Exception ex)
            {
                AlertTitle = "Erro ao aceder ao servidor ";
                AlertVisibility = true;
                WarningMessage = ex.Message;
                return Enumerable.Empty<LookupTableVM>();
            }

        }

        protected async Task RowSelectHandler(RowSelectEventArgs<PetVM> args)
        {
            PetId = args.Data.Id;
            PetName = args.Data.Nome;
            SelectedPet = await GetPetById(PetId);
        }

        public async Task OnPetCommandClicked(CommandClickEventArgs<PetVM> args)
        {
            Module = Modules.Pets;

            if (args.CommandColumn.Type == CommandButtonType.Edit)
            {
                PetId = args.RowData.Id;
                SelectedPet = await GetPetById(PetId);
                PetName = SelectedPet.Nome;

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
            Module = Modules.Documents;
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
            Module = Modules.Dewormers;
            PetDewormerId = args.RowData.Id;
            SelectedDewormer = await GetSelectedDewormer(PetDewormerId);

            if (args.CommandColumn.Type == CommandButtonType.Edit)
            {
                AddEditDewormerVisibility = true;
                EditCaption = $"{L["EditMsg"]} {L["Pet_Dewormers"]}";
                RecordMode = OpcoesRegisto.Gravar;
            }
        }

        public async Task OnPetVaccineCommandClicked(CommandClickEventArgs<VacinaVM> args)
        {
            Module = Modules.Vaccines;
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
            Module = Modules.PetFood;
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
            Module = Modules.Consultations;
            PetConsultationId = args.RowData.Id;
            SelectedConsultation = await GetSelectedConsultation(PetConsultationId);

            if (args.CommandColumn.Type == CommandButtonType.Edit)
            {
                AddEditConsultationVisibility = true;
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
                AlertTitle = "Apagar Pet";
                WarningMessage = L["SuccessDelete"];
                AlertVisibility = true;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message, $"{L["MSG_ApiError"]}");

                AlertTitle = $"{L["DeleteMsg"]} {L["Pet_Title"]}";
                WarningMessage = $"Erro ({ex.Message})";
                AlertVisibility = true;
            }

        }


        private async Task<List<string>> Validate<T>(T entity, string validatorEndPoint) where T : class
        {
            try
            {

                using (HttpClient httpClient = new HttpClient())
                {

                    var response = await httpClient.PostAsJsonAsync(validatorEndPoint, entity);
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
            catch (Exception ex)
            {
                return new List<string>() { ex.Message };
            }
        }

        private async Task<bool> Save<T>(T dto, string url) where T : class
        {
            string? entityTitle = string.Empty;
            int? entityId = 0;

            switch (Module)
            {
                case Modules.Pets:
                    entityTitle = L["Pet_Title"];
                    entityId = PetId;
                    break;
                case Modules.Documents:
                    entityTitle = L["Pet_Documents"];
                    entityId = PetDocumentId;
                    break;
                case Modules.Dewormers:
                    entityTitle = L["Pet_Dewormers"];
                    entityId = PetDewormerId;
                    break;
                case Modules.PetFood:
                    entityTitle = L["Pet_Food"];
                    entityId = PetFoodId;
                    break;
                case Modules.Consultations:
                    entityTitle = L["Pet_Consultations"];
                    entityId = PetId;
                    break;
                case Modules.Vaccines:
                    entityTitle = L["Pet_Vaccines"];
                    entityId = PetVaccineId;
                    break;
            }

            if (RecordMode == OpcoesRegisto.Gravar)
            {
                ToastTitle = $"{L["btnSalvar"]} {entityTitle}";
                try
                {

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var result = await httpClient.PutAsJsonAsync($"{url}/{entityId}", dto);
                        var success = result.IsSuccessStatusCode;

                        await DisplaySuccessOrFailResults(success, RecordMode);

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
                ToastTitle = $"{L["creationMsg"]} {entityTitle}";
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var result = await httpClient.PostAsJsonAsync(url, dto);
                        var success = result.IsSuccessStatusCode;

                        await DisplaySuccessOrFailResults(success, RecordMode);

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

        private void CloseAddEditDialog()
        {
            switch (Module)
            {
                case Modules.Pets:
                    AddEditPetVisibility = false;
                    break;
                case Modules.Documents:
                    AddEditDocumentVisibility = false;
                    break;
                case Modules.Dewormers:
                    AddEditDewormerVisibility = false;
                    break;
                case Modules.PetFood:
                    AddEditPetFoodVisibility = false;
                    break;
                case Modules.Consultations:
                    AddEditConsultationVisibility = false;
                    break;
                case Modules.Vaccines:
                    AddEditVaccineVisibility = false;
                    break;
            }
        }

        private async Task<IEnumerable<T>> GetData<T>(string url) where T : class
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetFromJsonAsync<IEnumerable<T>>(url);
                    if (response == null)
                    {
                        return Enumerable.Empty<T>();
                    }

                    return response;
                }
                catch (HttpRequestException exa)
                {
                    logger?.LogError(exa.Message, L["MSG_ApiError"]);

                    return Enumerable.Empty<T>();
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex.Message, L["MSG_ApiError"]);
                    return Enumerable.Empty<T>();
                }
            }
        }

        private async Task DisplaySuccessOrFailResults(bool success, OpcoesRegisto addUpdateOperation)
        {

            // TODO adapt to delete operations

            if (addUpdateOperation == OpcoesRegisto.Gravar) // update
            {
                if (!success)
                {
                    AlertVisibility = true;
                    AlertTitle = L["FalhaGravacaoRegisto"];
                    WarningMessage = L["MSG_ApiError"];
                }
                else
                {
                    ToastCss = "e-toast-success";
                    ToastMessage = L["SuccessUpdate"];
                    ToastIcon = "fas fa-check";
                }
            }
            else // insert
            {
                if (!success)
                {
                    AlertVisibility = true;
                    AlertTitle = L["FalhaCriacaoRegisto"];
                    WarningMessage = L["MSG_ApiError"];
                }
                else
                {
                    ToastCss = "e-toast-success";
                    ToastMessage = L["SuccessInsert"];
                    ToastIcon = "fas fa-check";
                }
            }

            Pets = await GetAllPets();
            await InvokeAsync(StateHasChanged);

            await Task.Delay(100);
            await ToastObj.ShowAsync();

            CloseAddEditDialog();

            await petsGridObj!.Refresh();

        }


        public async Task DataBound()
        {
            if (firstrender)
            {
                var dotNetReference = DotNetObjectReference.Create(this);          // create dotnet ref
                await Runtime.InvokeAsync<string>("detail", dotNetReference);     // send the dotnet ref to JS side
                firstrender = false;
            }

            foreach (var a in ExpandedRows)
            {
                var PKIndex = await petsGridObj.GetRowIndexByPrimaryKeyAsync(a.Key);
                await petsGridObj.ExpandCollapseDetailRowAsync((PetVM)petsGridObj.CurrentViewData.ElementAt(Convert.ToInt32(PKIndex)));     //Expand the already expnaded detailrows
            }
        }
        public void DetailDataBound(DetailDataBoundEventArgs<PetVM> args)
        {
            if (!ExpandedRows.ContainsKey(args.Data.Id))
            {
                ExpandedRows.Add(args.Data.Id, args.Data);  //add the expanded rows to dictionary
            }
        }

        [JSInvokable("DetailCollapse")]                            // method called from JS when collapse is done
        public void DetailRowCollapse(string CollapseIndex)
        {
            var rowIndex = int.Parse(CollapseIndex);
            PetVM CollapseRow = (PetVM)petsGridObj.CurrentViewData.ElementAt(Convert.ToInt32((rowIndex - 1).ToString()));
            ExpandedRows.Remove(CollapseRow.Id);              //Remove the collapsed row from expanded dictionary
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
