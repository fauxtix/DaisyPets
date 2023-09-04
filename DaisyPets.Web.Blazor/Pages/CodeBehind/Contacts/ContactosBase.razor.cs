using DaisyPets.Core.Application.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Localization;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Notifications;
using Syncfusion.Blazor.Popups;
using Syncfusion.Blazor.Spinner;
using System.Net.Sockets;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.Web.Blazor.Pages.CodeBehind.Contacts
{
    /// <summary>
    /// Base class for contacts pages
    /// </summary>
    public class ContactosBase : ComponentBase, IDisposable
    {
        /// <summary>
        /// contacts service
        /// </summary>
        [Inject] protected IStringLocalizer<App>? L { get; set; }
        [Inject] ILogger<App>? logger { get; set; } = null;

        [Inject] protected IConfiguration? config { get; set; }
        [Inject] protected HttpClient? _httpClient { get; set; }


        /// <summary>
        /// list of Contacts
        /// </summary>
        protected IEnumerable<ContactoVM>? Contacts { get; set; }
        public ContactoVM? SelectedContact { get; set; }
        protected ContactoVM? OriginalContactData { get; set; }
        protected OpcoesRegisto RecordMode { get; set; }
        protected int ContactId { get; set; }
        protected string? NewCaption { get; set; }
        protected string? EditCaption { get; set; }
        protected string? DeleteCaption;

        protected bool AddEditVisibility { get; set; }
        protected bool DeleteVisibility { get; set; }
        protected bool WarningVisibility { get; set; }
        protected string? WarningMessage { get; set; }
        public bool ErrorVisibility { get; set; } = false;


        public DialogEffect Effect = DialogEffect.Zoom;
        protected SfSpinner? SpinnerObj { get; set; }
        protected SfGrid<ContactoVM>? contactsGridObj { get; set; }
        protected SfToast? ToastObj { get; set; }

        protected bool IsDirty = false;
        protected List<string>? ValidationMessages = new();

        protected string? ToastTitle;
        protected string? ToastMessage;
        protected string? ToastCss;
        protected string? ToastIcon;


        protected string? urlBaseAddress;
        protected string? contactsEndpoint;
        protected string? AlertTitle = "";
        protected bool AlertVisibility { get; set; } = false;
        protected List<string> ValidationsMessages = new();

        /// <summary>
        /// Startup Contactos
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            urlBaseAddress = config?["ApiSettings:UrlBase"];
            contactsEndpoint = $"{urlBaseAddress}/Contacts";

            ToastTitle = "";
            ToastMessage = "";
            ToastCss = "";
            ToastIcon = "";

            AddEditVisibility = false;
            DeleteVisibility = false;
            WarningVisibility = false;
            WarningMessage = "";
            ContactId = 0;
            IsDirty = false;

            Contacts = await GetAllContacts();
            if (!Contacts.Any())
            {
                WarningMessage = "Sem dados para mostrar";
                WarningVisibility = true;
            }
        }

        /// <summary>
        /// Get all Contacts
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ContactoVM>> GetAllContacts()
        {
            IEnumerable<ContactoVM>? ContactsList = await GetAll();
            ContactsList.OrderByDescending(p => p.Id).ToList();
            return ContactsList;
        }

        public async Task<IEnumerable<ContactoVM>> GetAll()
        {

            var url = $"{contactsEndpoint}/AllContactsVM";
            try
            {
                var contacts = await _httpClient.GetFromJsonAsync<IEnumerable<ContactoVM>>(url);
                return contacts!.ToList();
            }
            catch (SocketException socketEx)
            {
                AlertTitle = "Erro ao aceder ao servidor ";
                AlertVisibility = true;
                WarningMessage = socketEx.Message;
                return Enumerable.Empty<ContactoVM>();
            }
            catch (Exception ex)
            {
                AlertTitle = "Erro ao aceder ao servidor ";
                AlertVisibility = true;
                WarningMessage = ex.Message;
                return Enumerable.Empty<ContactoVM>();
            }
        }

        protected async Task<ContactoVM> GetContactById(int Id)
        {
            var url = $"{contactsEndpoint}/{Id}";
            try
            {
                var contact = await _httpClient.GetFromJsonAsync<ContactoVM>(url);
                if (contact?.Id > 0)
                {
                    return contact;
                }
                else
                {
                    return new ContactoVM();
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new ContactoVM();
            }
        }



        protected async Task OnContactDoubleClickHandler(RecordDoubleClickEventArgs<ContactoVM> args)
        {
            ContactId = args.RowData.Id;
            SelectedContact = await GetContactById(ContactId);
            AddEditVisibility = true;
            EditCaption = $"{L["editionMsg"]} {L["TituloCampo3Editoras"]}";
            RecordMode = OpcoesRegisto.Gravar;
        }

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task OnContactCommandClicked(CommandClickEventArgs<ContactoVM> args)
        {
            ContactId = args.RowData.Id;
            SelectedContact = await GetContactById(ContactId);

            DeleteCaption = SelectedContact?.Nome;

            if (args.CommandColumn.Type == CommandButtonType.Edit)
            {
                AddEditVisibility = true;
                EditCaption = $"{L["editionMsg"]} {L["TituloCampo3Editoras"]}";
                RecordMode = OpcoesRegisto.Gravar;
                StateHasChanged();
            }

            if (args.CommandColumn.Type == CommandButtonType.Delete)
            {
                DeleteVisibility = true;
                StateHasChanged();
            }
        }

        protected async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "C_Grid_pdfexport")  //Id is combination of Grid's ID and itemname
            {
                await PdfExport();
            }
        }

        protected List<PdfHeaderFooterContent> HeaderContent = new List<PdfHeaderFooterContent>
        {
            new PdfHeaderFooterContent()
            {
                Type = ContentType.Text,
                Value = "Lista de Contactos",
                Position = new PdfPosition()
                 {
                        X = 0,
                        Y = 0
                },
                Style = new PdfContentStyle()
                {
                    TextBrushColor = "#000000", FontSize = 13
                }
            },
            new PdfHeaderFooterContent()
            {
                Type = ContentType.Line,
                Points = new PdfPoints()
                {
                    X1 = 0,
                    Y1 = 14,
                    X2 = 685,
                    Y2 = 14
                },
                Style = new PdfContentStyle()
                {
                    PenColor = "#000080",
                    DashStyle = PdfDashStyle.Solid
                }
             }
        };

        protected async Task PdfExport()
        {
            PdfExportProperties ExportProperties = new PdfExportProperties();
            ExportProperties.Header = new PdfHeader()
            {
                FromTop = 0,
                Height = 60,
                Contents = HeaderContent
            };

            ExportProperties.DisableAutoFitWidth = true;

            //Below code is to customize the columns width for the pdf exported grid irrespective of the actual grid columns width

            ExportProperties.Columns = new List<GridColumn>()
            {
                new GridColumn(){ Field="Nome", HeaderText="Nome", TextAlign=TextAlign.Left, Width="250"},
                new GridColumn(){ Field="Contacto", HeaderText="Contacto", TextAlign=TextAlign.Left, Width="100"},
                new GridColumn(){ Field="TipoContacto", HeaderText=" Tipo", TextAlign=TextAlign.Left, Width="150"}
            };

            await contactsGridObj.PdfExport(ExportProperties);

        }

        /// <summary>
        /// Grava registo
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveContactData()
        {
            IsDirty = false;
            ErrorVisibility = false;
            WarningMessage = string.Empty;
            WarningVisibility = false;

            ValidationMessages = await ValidateContact();

            if (ValidationMessages.Any() == false)
            {
                if (RecordMode == OpcoesRegisto.Gravar)
                {
                    ToastTitle = "Gravação de dados do Contacto";

                    var result = await _httpClient.PutAsJsonAsync($"{contactsEndpoint}/{ContactId}", SelectedContact);
                    var success = result.IsSuccessStatusCode;

                    await DisplaySuccessOrFailResults(success, RecordMode);

                    return success;
                }

                else // !editMode (Insert)
                {
                    ToastTitle = "Criação de Contacto";
                    NewCaption = L["NewMsg"] + " " + L["TituloCampo3Editoras"];


                    try
                    {
                        var result = await _httpClient.PostAsJsonAsync(contactsEndpoint, SelectedContact);
                        var success = result.IsSuccessStatusCode;
                    }
                    catch (Exception exc)
                    {
                        ValidationsMessages = new List<string> { exc.Message };
                        ErrorVisibility = true;

                        logger?.LogError(exc.Message, $"{L["MSG_ApiError"]}");
                        return false;
                    }
                }
                await GetAll();
                AddEditVisibility = false;

                StateHasChanged();
                await Task.Delay(100);
                await ToastObj!.ShowAsync();

                return true;
            }

            else
            {
                AddEditVisibility = false;
                StateHasChanged();
                AlertVisibility = true;
                return false;
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

            await GetAll();
            await InvokeAsync(StateHasChanged);

            await Task.Delay(100);
            await ToastObj.ShowAsync();

            AddEditVisibility = false;

        }

        private async Task<List<string>> ValidateContact()
        {
            var validatorEndpoint = $"{contactsEndpoint}/ValidateContacts";
            var errors = await Validate(SelectedContact!, validatorEndpoint);

            return errors.Count() > 0 ? errors : new List<string>();
        }

        private async Task<List<string>> Validate<T>(T entity, string validatorEndPoint) where T : class
        {
            try
            {

                var response = await _httpClient.PostAsJsonAsync(validatorEndPoint, entity);
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
            catch (Exception ex)
            {
                return new List<string>() { ex.Message };
            }
        }

        /// <summary>
        /// Inicializa dados do Contacto
        /// </summary>
        /// <param name="args"></param>
        public void onAddContact(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            RecordMode = OpcoesRegisto.Inserir;
            NewCaption = $"{L["NewMsg"]} {L["TituloCampo3Editoras"]}";

            SelectedContact = new ContactoVM()
            {
                IdTipoContacto = 1,
                Movel = "",
                DescricaoTipoContacto = "",
                eMail = "",
                Localidade = "",
                Morada = "",
                Nome = "",
                Notas = ""
            };

            AddEditVisibility = true;
        }

        /// <summary>
        /// Fecha diálogo de criação / edição do contacto
        /// Verifica se registo foi alterado
        /// </summary>
        protected async Task CloseEditDialog()
        {
            AddEditVisibility = false;
        }

        protected void ContinueEdit()
        {
            IsDirty = false;
            AddEditVisibility = true;
        }

        protected void IgnoreChangesAlert()
        {
            IsDirty = false;
            ErrorVisibility = false;
            AddEditVisibility = false;
        }

        public async Task ConfirmDeleteYes()
        {
            WarningVisibility = false;
            WarningMessage = "";
            ToastTitle = "Apagar Contacto";

            try
            {
                DeleteVisibility = false;

                var resultOk = await Delete();
                if (resultOk)
                {
                    Contacts = await GetAllContacts();
                    ToastCss = "e-toast-success";
                    ToastMessage = "Operação concluída com sucesso";
                    ToastIcon = "fas fa-check";
                }
                else
                {
                    ToastCss = "e-toast-danger";
                    ToastMessage = "Erro ao remover Contacto";
                    ToastIcon = "fas fa-exclamation";
                }

                StateHasChanged();
                await Task.Delay(100);
                await ToastObj!.ShowAsync();

            }
            catch (Exception exc)
            {

                ToastTitle = "Error";
                ToastCss = "e-toast-danger";
                ToastMessage = $"Erro ao remover Contacto ({exc.Message})";
                ToastIcon = "fas fa-exclamation";

                StateHasChanged();
                await Task.Delay(100);
                await ToastObj!.ShowAsync();
            }
        }

        public async Task<bool> Delete()
        {
            try
            {
                using (HttpResponseMessage result = await _httpClient.DeleteAsync($"{contactsEndpoint}/{ContactId}"))
                {
                    var success = result.IsSuccessStatusCode;
                    return success;
                }
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public void ConfirmDeleteNo()
        {
            DeleteVisibility = false;
        }


        /// <summary>
        ///  Fecha diálogo de validação
        /// </summary>
        public void CloseValidationErrorBox()
        {
            ErrorVisibility = false;
            AddEditVisibility = true;
        }

        protected async Task HideToast()
        {
            await ToastObj!.HideAsync();
        }

        private protected void EditContact(ContactoVM contact)
        {
            SelectedContact = contact;
            ContactId = contact.Id;
            AddEditVisibility = true;
            EditCaption = $"{L["editionMsg"]} {L["TituloCampo3Editoras"]}";
            RecordMode = OpcoesRegisto.Gravar;
        }

        public void Dispose()
        {
            ToastObj?.Dispose();
            SpinnerObj?.Dispose();
            contactsGridObj?.Dispose();
        }
    }
}