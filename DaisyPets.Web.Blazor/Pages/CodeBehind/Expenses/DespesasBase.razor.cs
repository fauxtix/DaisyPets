using DaisyPets.Core.Application.ViewModels.Despesas;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Notifications;
using Syncfusion.Blazor.Spinner;
using System.Globalization;
using System.Net.Sockets;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.Web.Blazor.Pages.CodeBehind.Expenses
{
    public class DespesasBase : ComponentBase, IDisposable
    {
        [Inject] protected IStringLocalizer<App>? L { get; set; }
        [Inject] ILogger<App>? logger { get; set; } = null;

        [Inject] protected IConfiguration? config { get; set; }
        [Inject] protected HttpClient? _httpClient { get; set; }
        protected IEnumerable<DespesaVM>? expensesList { get; set; }

        protected SfToast? ToastObj { get; set; }
        protected SfSpinner? SpinnerObj { get; set; }
        protected SfGrid<DespesaVM>? gridObj { get; set; }


        protected string ToastTitle = "";
        protected string ToastContent = "";
        protected string ToastCssClass = "";

        protected string? ToastMessage;
        protected string? ToastCss;
        protected string? ToastIcon;

        protected string? pageBadgeCaption;


        protected string spinnerLabel = "";
        protected string[] GroupedColumns = new string[] { "DescrImputacao" };
        protected string currentCulture = CultureInfo.CurrentCulture.Name;
        protected bool ErrorVisibility { get; set; } = false;

        protected OpcoesRegisto RecordMode { get; set; }

        protected string WarningCaption = "";
        protected string WarningTitle = "";

        protected bool EditExpenseDialogVisibility { get; set; } = false;
        protected bool DeleteConfirmVisibility { get; set; } = false;
        protected List<string> Error_Warnings_Msgs = new();

        protected bool editRecord { get; set; } = true;
        protected DespesaDto SelectedExpense = new();
        protected DespesaDto OriginalSelectedExpense = new();

        protected bool IsDirty = false;
        protected List<string> ValidationsMessages = new();

        protected bool WarningVisibility { get; set; }
        protected string? WarningMessage { get; set; }

        protected int expenseId;
        protected bool AlertVisibility { get; set; } = false;
        protected string? AlertTitle = "";

        protected string? urlBaseAddress;
        protected string? expensesEndpoint;


        protected Modules modulo { get; set; }

        protected string? NewCaption { get; set; }
        protected string? EditCaption { get; set; }
        protected string? DeleteCaption;

        protected bool ShowToolbarOptions { get; set; } = true;

        private bool shouldRender;
        protected override bool ShouldRender() => shouldRender;

        protected async override Task OnInitializedAsync()
        {

            urlBaseAddress = config?["ApiSettings:UrlBase"];
            expensesEndpoint = $"{urlBaseAddress}/Despesa";

            ShowToolbarOptions = false;
            pageBadgeCaption = L["TituloDespesasTodasDespesas"];
            if (currentCulture == "pt")
                currentCulture = "pt-PT";

            WarningCaption = "";
            WarningTitle = "";
            await GetExpenses();

            shouldRender = true;
        }

        protected async Task ConfirmDeleteYes()
        {
            ToastTitle = $"{L["DeleteMsg"]} {L["TituloDespesa"]}";
            var expenseDeleted = false;

            try
            {
                await Delete();

                expenseDeleted = true;
                ToastCssClass = "e-toast-success";
                ToastContent = L["RegistoAnuladoSucesso"];

                DeleteConfirmVisibility = false;

                await GetExpenses();

                StateHasChanged();

                await Task.Delay(200);
                await ToastObj!.ShowAsync();

            }
            catch (Exception ex)
            {
                expenseDeleted = false;
                ToastCssClass = "e-toast-danger";
                ToastContent = $"{L["FalhaAnulacaoRegisto"]}. Erro: {ex.Message}";
                await Task.Delay(200);
                await ToastObj!.ShowAsync();
            }
        }

        protected async Task GetExpenses()
        {
            spinnerLabel = L["MSG_PreparandoDados"];
            await Task.Delay(200);
            await SpinnerObj!.ShowAsync();

            try
            {
                expensesList = await GetAll();
                ShowToolbarOptions = expensesList.Any();

            }
            catch (SocketException socketEx)
            {
                AlertTitle = "Erro ao aceder ao servidor ";
                AlertVisibility = true;
                WarningMessage = socketEx.Message;
            }
            catch (Exception ex)
            {
                AlertTitle = "Erro ao aceder ao servidor ";
                AlertVisibility = true;
                WarningMessage = ex.Message;
            }
            finally
            {
                await Task.Delay(200);
                await SpinnerObj.HideAsync();
            }

        }

        public async Task<IEnumerable<DespesaVM>> GetAll()
        {

            var url = $"{expensesEndpoint}/AllVMAsync";
            try
            {
                    var expenses = await _httpClient.GetFromJsonAsync<IEnumerable<DespesaVM>>(url);
                    return expenses!.ToList();
            }
            catch (SocketException socketEx)
            {
                AlertTitle = "Erro ao aceder ao servidor ";
                AlertVisibility = true;
                WarningMessage = socketEx.Message;
                return Enumerable.Empty<DespesaVM>();
            }
            catch (Exception ex)
            {
                AlertTitle = "Erro ao aceder ao servidor ";
                AlertVisibility = true;
                WarningMessage = ex.Message;
                return Enumerable.Empty<DespesaVM>();
            }
        }

        protected async Task onAddExpense(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            NewCaption = $"{L["NewMsg"]} {L["TituloDespesa"]}";
            editRecord = false;
            SelectedExpense = new DespesaDto()
            {
                DataCriacao = DateTime.Now.Date.ToShortDateString(),
                DataMovimento = DateTime.Now.Date.ToShortDateString(),
                Descricao = "",
                IdCategoriaDespesa = -1,
                IdTipoDespesa = -1,
                Notas = "",
                TipoMovimento = "S",
                ValorPago = 0M
            };
            RecordMode = OpcoesRegisto.Inserir;

            EditExpenseDialogVisibility = true;
        }

        public async Task<bool> AddOrSaveExpense()
        {
            IsDirty = false;
            ErrorVisibility = false;
            WarningMessage = string.Empty;
            WarningVisibility = false;


            ValidationsMessages = await ValidateExpense();
            ToastTitle = L["TituloDespesas"];

            if (ValidationsMessages.Any() == false)
            {
                if (RecordMode == OpcoesRegisto.Gravar)
                {
                    editRecord = true;
                    EditCaption = L["EditMsg"] + " " + L["TituloDespesa"];

                    try
                    {

                            var result = await _httpClient.PutAsJsonAsync($"{expensesEndpoint}/{expenseId}", SelectedExpense);
                            var success = result.IsSuccessStatusCode;

                            await DisplaySuccessOrFailResults(success, RecordMode);

                            return success;
                    }
                    catch (Exception exc)
                    {
                        ValidationsMessages = new List<string> { exc.Message };
                        ErrorVisibility = true;
                        logger?.LogError(exc.Message, $"{L["MSG_ApiError"]}");
                        return false;
                    }
                }

                else // !editMode (Insert)
                {
                    editRecord = false;
                    NewCaption = L["NewMsg"] + " " + L["TituloDespesa"];


                    try
                    {
                            var result = await _httpClient.PostAsJsonAsync(expensesEndpoint, SelectedExpense);
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
                await GetExpenses();
                EditExpenseDialogVisibility = false;

                StateHasChanged();
                await Task.Delay(100);
                await ToastObj!.ShowAsync();

                return true;
            }
            else
            {
                ErrorVisibility = true;
                return false;
            }
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            await GetExpenses(); // return 'expensesList'

            if (expensesList.Count() == 0)
            {
                ToastTitle = L[""];
                ToastMessage = L["TituloManutencaoDespesas"];
                ToastCss = "e-toast-warning";

                StateHasChanged();
                await Task.Delay(100);
                await ToastObj!.ShowAsync();
                await Task.Delay(1000);
                return;
            }

            if (args.Item.Id == "Expenses_Grid_excelexport")
            {
                ExcelExportProperties excelExportProperties = new ExcelExportProperties();
                excelExportProperties.IncludeTemplateColumn = true;
                excelExportProperties.IncludeHeaderRow = true;
                excelExportProperties.FileName = $"Expenses_{DateTime.Now.ToShortTimeString()}.xlsx";
                await gridObj!.ExportToExcelAsync(excelExportProperties);
            }
            else if (args.Item.Id == "ThisYear")
            {
                DateTime start = new DateTime(DateTime.Now.Year, 1, 1),
                                end = start.AddYears(1);

                var yearQry = from record in expensesList
                              where DateTime.Parse(record.DataMovimento) >= start
                               && DateTime.Parse(record.DataMovimento) < end
                              select record;
                pageBadgeCaption = L["TituloDespesasEsteAno"];
                expensesList = yearQry;

            }
            else if (args.Item.Id == "LastYear")
            {
                DateTime start = new DateTime(DateTime.Now.Year, 1, 1).AddYears(-1),
                                end = start.AddYears(1);

                var yearQry = from record in expensesList
                              where DateTime.Parse(record.DataMovimento) >= start
                               && DateTime.Parse(record.DataMovimento) < end
                              select record;
                pageBadgeCaption = L["TituloDespesasAnoAnterior"];
                expensesList = yearQry;

            }
            else if (args.Item.Id == "ThisMonth")
            {
                var monthQry = expensesList
                .Where(p => DateTime.Parse(p.DataMovimento).Date.Year == DateTime.Today.Year &&
               DateTime.Parse(p.DataMovimento).Date.Month == DateTime.Today.Month);
                pageBadgeCaption = L["TituloMesAnterior"]; ;

                expensesList = monthQry;
            }
            else // All expenses
            {
                pageBadgeCaption = L["TituloTotalDespesas"];
            }
        }


        protected async Task OnExpenseDoubleClickHandler(RecordDoubleClickEventArgs<DespesaVM> args)
        {
            expenseId = args.RowData.Id;
            modulo = Modules.Pagamentos;
            SelectedExpense = await GetExpenseById(expenseId);

            EditExpenseDialogVisibility = true;

            EditCaption = L["EditMsg"] + " " + L["TituloDespesa"];
            RecordMode = OpcoesRegisto.Gravar;
        }

        public async Task OnExpenseCommandClicked(CommandClickEventArgs<DespesaVM> args)
        {
            expenseId = args.RowData.Id;
            modulo = Modules.Pagamentos;
            DeleteCaption = "";

            SelectedExpense = await GetExpenseById(expenseId);
            DeleteCaption = SelectedExpense.DataMovimento;


            if (args.CommandColumn.Type == CommandButtonType.Edit)
            {

                EditExpenseDialogVisibility = true;
                EditCaption = L["EditMsg"] + " " + L["TituloDespesa"];
                RecordMode = OpcoesRegisto.Gravar;
                StateHasChanged();
            }

            if (args.CommandColumn.Type == CommandButtonType.Delete)
            {
                WarningTitle = $"{L["DeleteMsg"]} {L["TituloDespesa"]}";
                DeleteConfirmVisibility = true;
                StateHasChanged();
            }
            if (args.CommandColumn.Type == CommandButtonType.None)
            {
                editRecord = false;
                RecordMode = OpcoesRegisto.Inserir;
                EditExpenseDialogVisibility = true;
                NewCaption = L["TituloNovaDespesaPorDuplicacao"];
                StateHasChanged();
            }
        }

        protected void IgnoreChangesAlert()
        {
            IsDirty = false;
            ErrorVisibility = false;
            EditExpenseDialogVisibility = false;
        }

        protected void ContinueEdit()
        {
            IsDirty = false;
            EditExpenseDialogVisibility = true;

        }

        public void ConfirmDeleteNo()
        {
            DeleteConfirmVisibility = false;
        }

        public void CloseValidationErrorBox()
        {
            DeleteConfirmVisibility = false;
            EditExpenseDialogVisibility = true;
        }

        protected void CloseEditDialog()
        {
            IsDirty = false;
            ErrorVisibility = false;

            EditExpenseDialogVisibility = false;
        }

        private async Task<List<string>> ValidateExpense()
        {
            var validatorEndpoint = $"{expensesEndpoint}/ValidateExpense";
            var errors = await Validate(SelectedExpense!, validatorEndpoint);

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

        protected async Task<DespesaDto> GetExpenseById(int Id)
        {
            var url = $"{expensesEndpoint}/{Id}";
            try
            {
                    var expense = await _httpClient.GetFromJsonAsync<DespesaDto>(url);
                    if (expense?.Id > 0)
                    {
                        return expense;
                    }
                    else
                    {
                        return new DespesaDto();
                    }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new DespesaDto();
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

            await GetExpenses();
            await InvokeAsync(StateHasChanged);

            await Task.Delay(100);
            await ToastObj.ShowAsync();

            EditExpenseDialogVisibility = false;

            // await gridObj!.Refresh();

        }

        public async Task<bool> Delete()
        {
            try
            {
                using (HttpResponseMessage result = await _httpClient.DeleteAsync($"{expensesEndpoint}/{expenseId}"))
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

        protected async Task HideToast()
        {
            await ToastObj!.HideAsync();
        }

        public void Dispose()
        {
            ToastObj?.Dispose();
            SpinnerObj?.Dispose();
            gridObj?.Dispose();
        }
    }
}
