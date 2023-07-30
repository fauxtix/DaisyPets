using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.TodoManager;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Syncfusion.Blazor.Calendars;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.Web.Blazor.Pages.TodoLists
{
    public partial class AddEditTodo
    {
        [Parameter]
        public ToDoDto? UserSelectedRecord { get; set; }
        protected IEnumerable<LookupTableVM>? ToDoCategories { get; set; }

        [Parameter]
        public OpcoesRegisto EditMode { get; set; }

        [Parameter]
        public string? HeaderCaption { get; set; }

        [Parameter]
        public string? PetName { get; set; }
        protected DateTime dStart { get; set; }
        protected DateTime dEnd { get; set; }

        [Inject]
        IConfiguration? config { get; set; }

        [Inject]
        ILogger<App>? logger { get; set; } = null;

        [Inject]
        public IStringLocalizer<App>? L { get; set; }

        protected string? urlBaseAddress;
        protected int ToDoId { get; set; }

        protected int idxCategory;
        protected string? todosEndpoint;
        protected bool? pendente { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            urlBaseAddress = config?["ApiSettings:UrlBase"];
            todosEndpoint = $"{urlBaseAddress}/ToDos";
            ToDoCategories = await GetLookupData("ToDoCategories");
        }

        protected override void OnParametersSet()
        {
            ToDoId = UserSelectedRecord.Id;
            idxCategory = UserSelectedRecord.CategoryId;
            pendente = Convert.ToBoolean(UserSelectedRecord.Status);
            if (DataFormat.IsValidDate(UserSelectedRecord.StartDate))
            {
                dStart = DateTime.Parse(UserSelectedRecord.StartDate);
            }

            if (DataFormat.IsValidDate(UserSelectedRecord.EndDate))
            {
                dEnd = DateTime.Parse(UserSelectedRecord.EndDate);
            }
        }

        protected void DataInicioChanged(ChangedEventArgs<DateTime> args)
        {
            UserSelectedRecord.StartDate = args.Value.ToShortDateString();
        }

        protected void DataFimChanged(ChangedEventArgs<DateTime> args)
        {
            UserSelectedRecord.EndDate = args.Value.ToShortDateString();
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

        protected void onChangeCategory(Syncfusion.Blazor.DropDowns.ChangeEventArgs<int, LookupTableVM> args)
        {
            idxCategory = args.Value;
            UserSelectedRecord!.CategoryId = idxCategory;
        }

        private void onChangePendente(Microsoft.AspNetCore.Components.ChangeEventArgs args)
        {
            pendente = Convert.ToBoolean(args.Value);
            UserSelectedRecord.Status = pendente.Value ? 1 : 0;
        }
    }
}