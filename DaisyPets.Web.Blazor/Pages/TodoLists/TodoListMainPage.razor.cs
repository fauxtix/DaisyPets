using DaisyPets.Core.Application.TodoManager;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Notifications;
using Syncfusion.Blazor.Popups;
using System.Net.Sockets;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.Web.Blazor.Pages.TodoLists
{
    public partial class TodoListMainPage
    {
        [Inject] IConfiguration? config { get; set; }
        [Inject] HttpClient httpClient { get; set; }
        [Inject] public IStringLocalizer<App>? L { get; set; }
        [Inject] ILogger<App>? logger { get; set; } = null;
        [Inject] public IWebHostEnvironment? _env { get; set; }

        protected string? urlBaseAddress;
        protected IEnumerable<ToDoDto>? ToDoList { get; set; }
        protected IEnumerable<LookupTableVM>? ToDoCategories { get; set; }
        protected ToDoDto? SelectedToDo { get; set; }

        protected string? todosEndpoint;
        protected OpcoesRegisto RecordMode { get; set; }
        protected string? NewCaption { get; set; }
        protected string? EditCaption { get; set; }

        protected string? DeleteCaption;
        protected Modules Module { get; set; }
        protected bool AddEditToDoVisibility { get; set; }
        protected bool DeleteToDoVisibility { get; set; }
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
        protected SfGrid<ToDoDto>? todosGridObj { get; set; }

        protected SfCalendar<DateTime>? calendarObj { get; set; }
        protected SfToast? ToastObj { get; set; }
        protected int ToDoId { get; set; }

        protected DateTime SelectedDate { get; set; } = DateTime.Now;
        protected DateTime startDate { get; set; }
        protected DateTime endDate { get; set; }
        protected bool Pending { get; set; } = true;

        public string SelectedValue { get; set; } = DateTime.Now.ToString("d/M/yyyy");
        public DateTime? CurrentDate { get; set; } = DateTime.Now;

        public bool MultiSelect { get; set; } = true;
        public DateTime[] MultiValue = new DateTime[] { DateTime.Now, DateTime.Now };

        protected int idxCategory;
        protected SfGrid<ToDoDto>? gridObj { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ToDoId = 0;
            idxCategory = 1;
            urlBaseAddress = config?["ApiSettings:UrlBase"];
            todosEndpoint = $"{urlBaseAddress}/ToDos";
            ToDoList = await GetTodoList();
            ToDoCategories = await GetLookupData("ToDoCategories");
        }

        private async Task<IEnumerable<ToDoDto>> GetTodoList()
        {
            try
            {
                var result = (await GetData<ToDoDto>(todosEndpoint!)).ToList();
                return result;
            }
            catch (SocketException socketEx)
            {
                AlertTitle = "Erro ao aceder ao servidor ";
                AlertVisibility = true;
                WarningMessage = socketEx.Message;
                return Enumerable.Empty<ToDoDto>();
            }
            catch (Exception ex)
            {
                AlertTitle = "Erro ao aceder ao servidor ";
                AlertVisibility = true;
                WarningMessage = ex.Message;
                return Enumerable.Empty<ToDoDto>();
            }
        }

        private async Task<IEnumerable<LookupTableVM>> GetLookupData(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
                return null;
            urlBaseAddress = config?["ApiSettings:UrlBase"];
            var lookupTablesEndpoint = $"{urlBaseAddress}/LookupTables/GetAllRecords/{tableName}";
            try
            {
                var result = await httpClient.GetFromJsonAsync<IEnumerable<LookupTableVM>>(lookupTablesEndpoint);
                if (result is null)
                {
                    return Enumerable.Empty<LookupTableVM>();
                }

                return result;
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

        private async Task<IEnumerable<T>> GetData<T>(string url)
            where T : class
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

        protected async Task RowSelectHandler(RowSelectEventArgs<ToDoDto> args)
        {
            ToDoId = args.Data.Id;
            SelectedToDo = await GetDtoById(ToDoId);
        }

        public void ValueChange(@Syncfusion.Blazor.DropDowns.ChangeEventArgs<int, string> args)
        {
            var x = args;
            //if (args.Value == "United States")
            //{
            //    States = new List<string>() { "New York", "Virginia", "Washington" };
            //}
            //else if (args.Value == "Australia")
            //{
            //    States = new List<string>() { "Queensland", "Tasmania", "Victoria" };
            //}
            //Enabled = true;
            //GridRef.PreventRender(false);
        }

        public void OnActionBegin(Syncfusion.Blazor.Grids.ActionEventArgs<LookupTableVM> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
            {
                var d = args;
            }
        }

        protected void onChangeBreed(Syncfusion.Blazor.DropDowns.ChangeEventArgs<int, LookupTableVM> args)
        {
            idxCategory = args.Value;
            //UserSelectedPet!.IdRaca = idxBreed;
        }

        protected async Task<ToDoDto> GetDtoById(int Id)
        {
            var url = $"{urlBaseAddress}/ToDos/{Id}";
            try
            {
                var todo = await httpClient.GetFromJsonAsync<ToDoDto>(url);
                if (todo?.Id > 0)
                {
                    return todo;
                }
                else
                {
                    return new ToDoDto();
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new ToDoDto();
            }
        }

        public async Task OnPetCommandClicked(CommandClickEventArgs<ToDoDto> args)
        {
            Module = Modules.ToDos;
            ToDoId = args.RowData.Id;
            SelectedToDo = await GetDtoById(ToDoId);

            if (args.CommandColumn.Type == CommandButtonType.Edit)
            {
                AddEditToDoVisibility = true;
                EditCaption = $"{L["EditMsg"]} {L["TituloTarefa"]}";
                RecordMode = OpcoesRegisto.Gravar;
            }

            if (args.CommandColumn.Type == CommandButtonType.Delete)
            {
                WarningTitle = $"{L["DeleteMsg"]}  {L["TituloTarefa"]}?";
                DeleteToDoVisibility = true;
                DeleteCaption = SelectedToDo?.Description;
            }
            if (args.CommandColumn.Type == CommandButtonType.None)
            {
                RecordMode = OpcoesRegisto.Duplicar;
                AddEditToDoVisibility = true;
                NewCaption = L["TituloNovaTarefaPorDuplicacao"];
                StateHasChanged();
            }

        }

        public async Task<bool> SaveToDoData()
        {
            var validationMessages = await ValidateToDo();
            if (validationMessages.Any())
            {
                ValidationsMessages = validationMessages;
                ErrorVisibility = true;
                return false;
            }

            var url = $"{urlBaseAddress}/Todos";
            var result = await Save(SelectedToDo!, url);
            return result;
        }

        private async Task<List<string>> ValidateToDo()
        {
            var validatorEndpoint = $"{urlBaseAddress}/Todos/ValidateToDo";
            var errors = await Validate(SelectedToDo!, validatorEndpoint);
            return errors.Count() > 0 ? errors : new List<string>();
        }

        private async Task<List<string>> Validate<T>(T entity, string validatorEndPoint)
            where T : class
        {
            try
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
            catch (Exception ex)
            {
                return new List<string>()
                {
                    ex.Message
                };
            }
        }

        private async Task<bool> Save<T>(T dto, string url)
            where T : class
        {
            string? entityTitle = string.Empty;
            int? entityId = 0;
            switch (Module)
            {
                case Modules.ToDos:
                    entityTitle = L["TituloTarefas"];
                    entityId = ToDoId;
                    break;
            }

            if (RecordMode == OpcoesRegisto.Gravar)
            {
                ToastTitle = $"{L["btnSalvar"]} {entityTitle}";
                try
                {
                    var result = await httpClient.PutAsJsonAsync($"{url}/{entityId}", dto);
                    var success = result.IsSuccessStatusCode;
                    await DisplaySuccessOrFailResults(success, RecordMode);
                    return success;
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
                    var result = await httpClient.PostAsJsonAsync(url, dto);
                    var success = result.IsSuccessStatusCode;
                    await DisplaySuccessOrFailResults(success, RecordMode);
                    return success;
                }
                catch (Exception exc)
                {
                    logger?.LogError(exc.Message, $"{L["MSG_ApiError"]}");
                    return false;
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

            ToDoList = await GetTodoList();
            await InvokeAsync(StateHasChanged);
            await Task.Delay(100);
            await ToastObj.ShowAsync();
            AddEditToDoVisibility = false;
        }

        protected async Task HideToast()
        {
            await ToastObj!.HideAsync();
        }

        protected async Task ConfirmDeleteYes()
        {
            ToastTitle = $"{L["DeleteMsg"]} {L["TituloTarefa"]}";
            await DeleteTodo();
            ToastCssClass = "e-toast-success";
            ToastContent = L["RegistoAnuladoSucesso"];
            await Task.Delay(200);
            await ToastObj!.ShowAsync();
        }

        private async Task DeleteTodo()
        {
            string url = $"{urlBaseAddress}/Todos/{ToDoId}";
            try
            {
                var response = await httpClient.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    ValidationsMessages = new List<string>()
                        {
                            L["FalhaAnulacaoRegisto"]
                        };
                    ErrorVisibility = true;
                    return;
                }

                ToDoList = await GetTodoList();

                DeleteToDoVisibility = false;
                AlertTitle = $"Apagar {L["TituloTarefa"]}";
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

        public void onAddToDo(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            RecordMode = OpcoesRegisto.Inserir;
            NewCaption = $"{L["NewMsg"]}  {L["TituloTarefa"]}";
            Module = Modules.ToDos;
            SelectedToDo = new()
            {
                CategoryId = -1,
                Description = "",
                StartDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm"),
                Completed = 0,
                Generated = 0
            };
            AddEditToDoVisibility = true;
        }

        public async Task CalendarValuechangeHandler(ChangedEventArgs<DateTime> args)
        {
            ToDoList = await GetTodoList();

            var dateSelected = args.Value.Date;
            var output = ToDoList?.Where(d => d.TodoStartDate.Date == dateSelected).ToList();

            if (output?.Any() == false)
            {
                AlertVisibility = true;
                AlertTitle = "ToDo lists";
                WarningMessage = $"Sem dados para mostrar para a data {dateSelected.ToShortDateString()}...";
                await InvokeAsync(StateHasChanged);
                return;
            }

            SelectedDate = args.Value;
            ToDoList = output;
        }

        public async void CustomDates(RenderDayCellEventArgs args)
        {
            var CurrentMonth = args.Date.Month;
            var Count = 0;

            foreach (var item in ToDoList)
            {
                if (args.Date.Month == CurrentMonth && (args.Date.Month == item.TodoStartDate.Month && args.Date.Day == item.TodoStartDate.Day))
                {
                    args.CellData.ClassList += " personal-appointment";
                    CurrentMonth = item.TodoStartDate.Month;
                    Count++;
                }

                if (Count == 0)
                {
                    this.SelectedValue = this.SelectedDate.ToString("d/M/yyyy");
                }
            }
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            ToDoList = await GetTodoList();
            if (ToDoList?.Count() == 0)
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

            if (args.Item.Id == "ToDos_Grid_excelexport")
            {
                try
                {
                    ExcelExportProperties excelExportProperties = new ExcelExportProperties();
                    excelExportProperties.IncludeTemplateColumn = true;
                    excelExportProperties.IncludeHeaderRow = true;
                    excelExportProperties.FileName = $"ToDos_{DateTime.Now.ToShortTimeString()}.xlsx";
                    await gridObj!.ExportToExcelAsync(excelExportProperties);
                }
                catch (Exception ex)
                {
                    var s = ex.Message;
                    throw;
                }
            }
            else if (args.Item.Id == "WithAlerts")
            {
                //var alertsQuery = ToDoList.Where((tl => tl.TodoStartDate.Date > DateTime.Now.Date &&
                //    ((tl.TodoStartDate - DateTime.Now).TotalDays >= 15) && (tl.TodoStartDate - DateTime.Now).TotalDays <= 31)).ToList();
                //ToDoList = alertsQuery;
                var alertsQuery = ToDoList.Where(tl => 
                        (tl.TodoStartDate.Date > DateTime.Now.Date && tl.TodoStartDate.Month == DateTime.Now.Month) ||
                        (tl.TodoEndDate.Date > DateTime.Now.Date && tl.TodoEndDate.Month == DateTime.Now.Month) ||
                        ((tl.TodoEndDate.Date - DateTime.Now.Date).TotalDays <= 31)
                     )
                    .ToList();
                ToDoList = alertsQuery;
            }
            else if (args.Item.Id == "Pending")
            {
                var pendingQry =
                    from record in ToDoList
                    where record.Completed == 0
                    select record;
                //pageBadgeCaption = L["TituloDespesasEsteAno"];
                ToDoList = pendingQry;
            }
            else if (args.Item.Id == "Completed")
            {
                var completedQry =
                    from record in ToDoList
                    where record.Completed == 1
                    select record;
                //pageBadgeCaption = L["TituloDespesasEsteAno"];
                ToDoList = completedQry;
            }
            else if (args.Item.Id == "All")
            {
                ToDoList = await GetTodoList();
            }
        }

        protected void RowBound(RowDataBoundEventArgs<ToDoDto> Args)
        {
            int generatedEntry = Args.Data.Generated;
            if (generatedEntry == 1) // remove edit and duplicate buttons if generated entry = 1 (yes)
            {
                Args.Row.AddClass(new string[] { "e-removeCustomcommand" });
                //Args.Row.AddClass(new string[] { "e-removeEditcommand" });
            }
        }


    }
}