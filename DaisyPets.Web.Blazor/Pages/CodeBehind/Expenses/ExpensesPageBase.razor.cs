using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using DaisyPets.Core.Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DaisyPets.Web.Blazor.Pages.CodeBehind.Expenses
{
    public class ExpensesPageBase : ComponentBase
    {
        [Inject] IConfiguration? Config { get; set; }
        [Inject] public IStringLocalizer<App>? L { get; set; }
        [Inject] ILogger<App>? logger { get; set; } = null;
        [Inject] public IWebHostEnvironment? _env { get; set; }

        protected IEnumerable<DespesaVM>? Expenses { get; set; }

        protected int ExpenseId = 0;
        protected int ExpenseCategoryId;

        protected string? urlBaseAddress;

        protected string? ExpensesApiEndpoint { get; set; }
        protected string? LookupTablesApiEndpoint { get; set; }
        private decimal totalExpenses { get; set; }
        private decimal totalFilteredExpenses { get; set; }

        protected override async Task OnInitializedAsync()
        {
            urlBaseAddress = Config?["ApiSettings:UrlBase"];
            ExpensesApiEndpoint = $"{urlBaseAddress}/Despesa/";
            LookupTablesApiEndpoint = $"{urlBaseAddress}/LookupTables/";
            Expenses = await GetExpenses();
        }

        protected async Task<IEnumerable<DespesaVM>> GetExpenses()
        {
            string url = $"{ExpensesApiEndpoint}/AllVMAsync";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var expenses = await httpClient.GetFromJsonAsync<IEnumerable<DespesaVM>>(url);
                    if (expenses is not null)
                    {
                        return expenses;
                    }

                    return Enumerable.Empty<DespesaVM>();
                }

            }
            catch (Exception ex)
            {
                //MessageBoxAdv.Show($"Erro no API de despesas({ex.Message})", "Preenchimento de grelha");
                return Enumerable.Empty<DespesaVM>();
            }
        }

        protected async Task<DespesaDto> GeExpense(int Id)
        {
            string url = $"{ExpensesApiEndpoint}/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var _expense = await httpClient.GetFromJsonAsync<DespesaDto>(url);
                return _expense ?? new DespesaDto();
            }
        }

        protected IEnumerable<TipoDespesa>? GetTipoDespesas()
        {
            string url = $"{ExpensesApiEndpoint}/TipoDespesas";
            using (HttpClient httpClient = new HttpClient())
            {
                var task = httpClient.GetFromJsonAsync<IEnumerable<TipoDespesa>?>(url);
                var _expenses = task.Result;

                task.Wait();

                if (_expenses is null)
                {
                    //MessageBoxAdv.Show("Erro ao carregar tipo de despesas", "Daist Pets - Despesas");
                }

                return _expenses;
            }

        }

        protected async Task<List<string>> ValidateExpense(DespesaDto expenseToValidate)
        {
            try
            {
                string url = $"{ExpensesApiEndpoint}/ValidateExpense";
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsJsonAsync(url, expenseToValidate);

                    if (response.IsSuccessStatusCode == false)
                    {
                        var errors = await response.Content.ReadFromJsonAsync<List<string>>();
                        if (errors?.Count() > 0)
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
                var errMsg = ex.Message;
                return new List<string> { errMsg };
            }
        }



        protected async Task DeleteExpense()
        {
            string url = $"{ExpensesApiEndpoint}/{ExpenseId}";
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(url);
                response.EnsureSuccessStatusCode();

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    //MessageBoxAdv.Show("Erro ao apagar registo", "Contactos");
                    return;
                }
                else
                {
                    Expenses = await GetExpenses();
                }
            }
        }

        protected async Task<string> GetDescricaoCategoria(int categoryId)
        {
            string url = $"{ExpensesApiEndpoint}/DescricaoCategoriaDespesa/{categoryId}";
            using (HttpClient httpClient = new HttpClient())
            {

                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var category = await response.Content.ReadFromJsonAsync<LookupTableVM>();
                    return category?.Descricao ?? "";
                }

                return "";
            }
        }

        protected async Task<string> GetDescricaoTipoDespesa(int expenseTypeId)
        {
            string url = $"{ExpensesApiEndpoint}/{ExpenseId}";
            using (HttpClient httpClient = new HttpClient())
            {

                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var expenseTypes = await response.Content.ReadFromJsonAsync<IEnumerable<TipoDespesa>>();
                    if (expenseTypes.Any())
                    {
                        var tipoDespesa = expenseTypes.SingleOrDefault(o => o.Id == expenseTypeId);
                        return tipoDespesa.Descricao;
                    }
                    return "";
                }
            }

            return "";
        }


        protected async Task<string> GeExpenseType()
        {
            string url = $"{ExpensesApiEndpoint}/VMExpenseByIdAsync/{ExpenseId}";

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var expense = await response.Content.ReadFromJsonAsync<DespesaVM>();
                    if (expense is not null)
                    {
                        var tipoDespesa = expense.DescricaoTipoDespesa;
                        return tipoDespesa;
                    }
                    return "";
                }
            }

            return "";

        }

    }
}
