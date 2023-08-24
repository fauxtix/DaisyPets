using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.Scheduler;
using global::Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Notifications;
using Syncfusion.Blazor.Popups;
using Syncfusion.Blazor.Schedule;
using Syncfusion.Blazor.Spinner;
using System.Net.Sockets;
using System.Timers;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.Web.Blazor.Pages.Scheduler
{
    public partial class SchedulerMainPage
    {
        [Inject] ILogger<App>? logger { get; set; } = null;
        [Inject] public IStringLocalizer<App> L { get; set; }
        [Inject] protected IConfiguration? config { get; set; }
        [Inject] protected HttpClient? _httpClient { get; set; }
        protected IEnumerable<PetDto>? Pets { get; set; }

        protected SfSchedule<AppointmentDataDto>? ScheduleRef;
        protected DialogEffect Effects = DialogEffect.Zoom;
        protected SfSpinner SpinnerObj;
        protected SfToast ToastObj;
        protected IEnumerable<AppointmentDataDto>? Appointments { get; set; }
        protected AppointmentDataDto? Appointment { get; set; }

        protected string? urlBaseAddress;
        protected string? apptsEndpoint;
        protected string? petsEndpoint;

        protected View CurrentView = View.Month;
        protected DateTime CurrentDate = DateTime.UtcNow;
        protected bool AppointmentDialogVisibility = false;
        protected bool AppointmentReadVisibility { get; set; } = false;
        protected bool AppointmentWarningVisibility { get; set; } = false;
        protected bool ErrorVisibility { get; set; } = false;
        protected bool KeepDialogOpen { get; set; } = true;

        protected string Action;
        protected TimeSpan apptTimeSelected;
        protected int apptId = 0;
        protected AppointmentDataDto AppointmentRecord = new AppointmentDataDto();
        protected OpcoesRegisto CRUD_Event = OpcoesRegisto.Inserir;
        protected AlertMessageType AlertType { get; set; }

        protected List<string> sErrorMsgs;
        protected string BoxErrorTitle;
        protected string CssBoxErrorTitle;
        protected string ToastTitle = "";
        protected string ToastContent = "";
        protected string ToastCssClass = "";

        protected string? ToastMessage;
        protected string? ToastCss;
        protected string? ToastIcon;

        protected int viewInterval { get; set; } = 60;

        protected List<string> PastApptsCustomClass = new List<string>()
        {
            "e-past-app"
        };
        protected List<string> ApptsCustomClass = new List<string>()
        {
            "e-appts-app"
        };
        protected List<string> DewormersCustomClass = new List<string>()
        {
            "e-dewormers-app"
        };
        protected List<string> VaccinesCustomClass = new List<string>()
        {
            "e-vaccines-app"
        };
        protected List<string> TodoListCustomClass = new List<string>()
        {
            "e-todo-list-app"
        };
        protected List<string> NormalApptCustomClass = new List<string>()
        {
            "e-normal-appt-app"
        };

        private List<AppointmentDataDto> gridDataSource = new List<AppointmentDataDto>();
        protected bool ShowSchedule { get; set; } = true;
        protected string SearchValue { get; set; }
        protected string SubjectValue { get; set; }
        protected string LocationValue { get; set; }
        protected DateTime? DateStart { get; set; }
        protected DateTime? DateEnd { get; set; }

        public class Timezone
        {
            public string Name { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }
        }

        private DateTime SystemTime { get; set; } = DateTime.UtcNow;
        private Timezone TimezoneData { get; set; } = new Timezone() { Name = "UTC+00:00", Key = "UTC", Value = "UTC" };

        public class ResourceData
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public string Color { get; set; }
        }
        public List<ResourceData> ApptTypeData { get; set; } = new List<ResourceData> {
        new ResourceData{ Text = "Vacina", Id= 1, Color = "#df5286" },
        new ResourceData{ Text = "Desparasitante", Id= 2, Color = "#7fa900" },
        new ResourceData{ Text = "Veterinário", Id= 3, Color = "#ea7a57" },
        new ResourceData{ Text = "Tarefas", Id= 4, Color = "#ea6a58" },
        new ResourceData{ Text = "Normal", Id= 5, Color = "steelblue" }
    };
        public List<ResourceData> PetsData { get; set; } = new List<ResourceData>();


        protected override async Task OnInitializedAsync()
        {
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += new ElapsedEventHandler((object sender, ElapsedEventArgs e) =>
            {
                string key = this.TimezoneData.Key ?? "UTC";
                SystemTime = this.TimeConvertor(key);
                ScheduleRef?.PreventRender();
                InvokeAsync(() => { StateHasChanged(); });
            });
            timer.Enabled = true;
            urlBaseAddress = config?["ApiSettings:UrlBase"];
            apptsEndpoint = $"{urlBaseAddress}/Appointment";
            petsEndpoint = $"{urlBaseAddress}/Pets";
            Appointments = await GetAppointments();

            PetsData = (await GetPets()).ToList();
        }

        protected async Task<IEnumerable<ResourceData>> GetPets()
        {
            List<ResourceData> resourceData = new List<ResourceData>();
            var url = $"{petsEndpoint}/AllPetsVM";
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<PetVM>>(url);
            if (response == null)
            {
                return Enumerable.Empty<ResourceData>();
            }

            int i = 0;
            foreach (var pet in response)
            {
                i++;
                resourceData.Add(
                    new ResourceData
                    {
                        Id = i, //pet.Id,
                        Color = "DodgerBlue",
                        Text = pet.Nome
                    }
                    );
            }

            resourceData.Add(
                new ResourceData
                {
                    Id = i + 1, //99,
                    Color = "cyan",
                    Text = "Outro"
                }
                );

            return resourceData;
        }
        protected async Task<IEnumerable<AppointmentDataDto>> GetAppointments()
        {
            var getAllApptsEndpoint = $"{apptsEndpoint}/AllAppointmentsVM";
            try
            {
                var appts = await _httpClient.GetFromJsonAsync<IEnumerable<AppointmentDataDto>>(getAllApptsEndpoint);
                return appts ?? Enumerable.Empty<AppointmentDataDto>();
            }
            catch (SocketException socketEx)
            {
                // AlertTitle = "Erro ao aceder ao servidor ";
                // AlertVisibility = true;
                // WarningMessage = socketEx.Message;
                return Enumerable.Empty<AppointmentDataDto>();
            }
            catch (Exception ex)
            {
                // AlertTitle = "Erro ao aceder ao servidor ";
                // AlertVisibility = true;
                // WarningMessage = ex.Message;
                return Enumerable.Empty<AppointmentDataDto>();
            }
        }

        private async void RefreshUI()
        {
            AppointmentDialogVisibility = false;
            if (SpinnerObj is not null)
                await SpinnerObj.ShowAsync();
            await GetAppointments();
            if (SpinnerObj is not null)
                await SpinnerObj.HideAsync();
            await Task.Delay(200);
            await ToastObj.ShowAsync();
            await ScheduleRef.RefreshEventsAsync();
        }

        protected void OnEventRendered(EventRenderedArgs<AppointmentDataDto> args)
        {
            if (args.Data.Subject.ToLower().Contains("consulta"))
            {
                args.CssClasses = ApptsCustomClass;
            }
            else if (args.Data.Subject.ToLower().Contains("desparasitante"))
            {
                args.CssClasses = DewormersCustomClass;
            }
            else if (args.Data.Subject.ToLower().Contains("vacina"))
            {
                args.CssClasses = VaccinesCustomClass;
            }
            else if (args.Data.Subject.ToLower().Contains("event"))
            {
                args.CssClasses = TodoListCustomClass;
            }
            else
            {
                args.CssClasses = NormalApptCustomClass;
            }
        }

        protected async Task OnPopupOpen(PopupOpenEventArgs<AppointmentDataDto> args)
        {
            if (args.Type == PopupType.Editor | args.Type == PopupType.QuickInfo)
            {
                args.Cancel = true; //to prevent the default editor window

                this.Action = apptId == 0 ? "CellClick" : "AppointmentClick";
                //if (Action == "CellClick") // new event
                //{
                //    if (args.Data.StartTime.Date < DateTime.Today.Date) // check if appt in the past
                //    {
                //        sErrorMsgs = new List<string>() { L["MSG_MarcacaoPassado"] };

                //        ErrorVisibility = true;
                //        AlertType = AlertMessageType.Warning;
                //        AppointmentDialogVisibility = false;
                //        KeepDialogOpen = false;
                //    }
                //    else
                //    {
                //        Appointment = new AppointmentDataDto();
                //        CRUD_Event = OpcoesRegisto.Inserir; // new record
                //        AppointmentDialogVisibility = true;
                //        await ScheduleRef.OpenEditorAsync(Appointment, CurrentAction.Add);
                //    }
                //}
                //else
                //{
                //    Appointment = args.Data;
                //    apptId = Appointment.Id;

                //    CRUD_Event = OpcoesRegisto.Gravar; // edit record
                //    AppointmentDialogVisibility = true;
                //    await ScheduleRef.OpenEditorAsync(Appointment, CurrentAction.Save);
                //}


            }
        }

        protected void SaveAppointment()
        {
            //sErrorMsgs = validatorService.ValidateApptsEntries(AppointmentRecord);
            // bool PreValidationOk = (sErrorMsgs is null);
            if (CRUD_Event == OpcoesRegisto.Inserir)
            {

            }
            else
            {

            }
        }

        //#pragma warning disable BL0005, CA2000  // Component parameter should not be set outside of its component, Dispose objects before losing scope
        protected async Task OnEventSearch()
        {
            if (!string.IsNullOrEmpty(SearchValue) && ScheduleRef != null)
            {
                Query query = new Query().Search(SearchValue, new List<string> { "Subject", "Location", "Description" }, null, true, true);
                List<AppointmentDataDto> eventCollections = await ScheduleRef.GetEventsAsync(null, null, true);
                object data = await new DataManager() { Json = eventCollections }.ExecuteQuery<AppointmentDataDto>(query);
                List<AppointmentDataDto> resultData = (data as List<AppointmentDataDto>);
                if (resultData.Count > 0)
                {
                    ShowSchedule = false;
                    gridDataSource = resultData;
                }
                else
                {
                    ShowSchedule = true;
                }
            }
            else
            {
                ShowSchedule = true;
            }
        }

        protected async void OnActionCompleted(ActionEventArgs<AppointmentDataDto> args)
        {
            var actionType = args.ActionType;

            if (actionType == ActionType.EventCreate ||
                actionType == ActionType.EventChange ||
                actionType == ActionType.EventRemove)
            {
                if (actionType == ActionType.EventCreate)
                {
                    Appointment = args.AddedRecords[0];
                    await InsertAppt();
                }
                else if (actionType == ActionType.EventChange)
                {
                    Appointment = args.ChangedRecords[0];
                    apptId = Appointment.Id;
                    await UpdateAppt();
                }
                else if (actionType == ActionType.EventRemove)
                {
                    Appointment = args.DeletedRecords[0];
                    apptId = Appointment.Id;
                    await DeleteAppt();
                }
            }
        }
        protected async Task OnSearchClick()
        {
            DateTime? startDate = null;
            DateTime? endDate = null;
            List<WhereFilter> searchObj = new List<WhereFilter>();
            if (!string.IsNullOrEmpty(SubjectValue))
            {
                searchObj.Add(new WhereFilter() { Field = "Subject", Operator = "contains", value = SubjectValue, Condition = "or", IgnoreCase = true });
            }
            if (!string.IsNullOrEmpty(LocationValue))
            {
                searchObj.Add(new WhereFilter() { Field = "Location", Operator = "contains", value = LocationValue, Condition = "or", IgnoreCase = true });
            }
            if (DateStart != null)
            {
                startDate = Convert.ToDateTime(DateStart);
                searchObj.Add(new WhereFilter() { Field = "StartTime", Operator = "greaterthanorequal", value = startDate, Condition = "and", IgnoreCase = false });
            }
            if (DateEnd != null)
            {
                endDate = (Convert.ToDateTime(DateEnd)).AddDays(1);
                searchObj.Add(new WhereFilter() { Field = "EndTime", Operator = "lessthanorequal", value = endDate, Condition = "and", IgnoreCase = false });
            }
            if (searchObj.Count > 0)
            {
                Query query = new Query().Where(new WhereFilter() { Condition = "and", IsComplex = true, predicates = searchObj });
                List<AppointmentDataDto> eventCollections = await ScheduleRef.GetEventsAsync(startDate, endDate, true);
                object data = await new DataManager() { Json = eventCollections }.ExecuteQuery<AppointmentDataDto>(query);
                List<AppointmentDataDto> resultData = (data as List<AppointmentDataDto>);
                gridDataSource = resultData;
                ShowSchedule = false;
            }
            else
            {
                ShowSchedule = true;
            }
        }

        private DateTime TimeConvertor(string TimeZoneId)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId));
        }

#pragma warning restore BL0005, CA2000 // Component parameter should not be set outside of its component, Dispose objects before losing scope
        protected void OnClearClick()
        {
            ShowSchedule = true;
            SearchValue = SubjectValue = LocationValue = string.Empty;
            DateStart = DateEnd = null;
        }

        public async void OnPrintClick()
        {
            await ScheduleRef.PrintAsync();
        }

        protected void CloseAppointmentDialog()
        {
            AppointmentDialogVisibility = false;
        }

        protected void CloseReadAppointmentDialog()
        {
            AppointmentReadVisibility = false;
        }

        protected void closeApptErrorBox()
        {
            ErrorVisibility = false;
            AppointmentDialogVisibility = KeepDialogOpen;
        }

        private async Task InsertAppt()
        {
            try
            {
                await _httpClient.PostAsJsonAsync(apptsEndpoint, Appointment);
            }
            catch (Exception ex)
            {
                // AlertTitle = "Erro ao aceder ao servidor ";
                // AlertVisibility = true;
                // WarningMessage = ex.Message;
            }

            RefreshUI();

        }

        private async Task UpdateAppt()
        {
            try
            {
                await _httpClient.PutAsJsonAsync($"{apptsEndpoint}/{apptId}", Appointment);
            }
            catch (Exception ex)
            {
                // AlertTitle = "Erro ao aceder ao servidor ";
                // AlertVisibility = true;
                // WarningMessage = ex.Message;
            }

            RefreshUI();

        }

        private async Task DeleteAppt()
        {
            try
            {
                var result = await _httpClient.DeleteFromJsonAsync<AppointmentDataDto>($"{apptsEndpoint}/{apptId}");
                if (result != null)
                {
                    return;
                }
                else
                {
                    var error = "";
                    // AlertTitle = "Erro ao aceder ao servidor ";
                    // AlertVisibility = true;
                    // WarningMessage = ex.Message;
                }
            }
            catch (Exception ex)
            {
                // AlertTitle = "Erro ao aceder ao servidor ";
                // AlertVisibility = true;
                // WarningMessage = ex.Message;
            }

            RefreshUI();

        }



        protected async Task HideToast()
        {
            await ToastObj!.HideAsync();
        }


    }
}