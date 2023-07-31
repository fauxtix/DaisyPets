using DaisyPets.Core.Application.ViewModels.LookupTables;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Syncfusion.Blazor.Notifications;
using Syncfusion.Blazor.Popups;
using System.Dynamic;

namespace DaisyPets.Web.Blazor.Pages.CodeBehind.LookupTables
{
    public class TabAuxBase : ComponentBase
    {
        protected int RecordId;
        protected int ToastTimeOut = 3000;

        [Inject] protected IStringLocalizer<App>? L { get; set; }
        [Inject] protected HttpClient? _httpClient { get; set; }
        [Inject] protected IConfiguration? _env { get; set; }
        [Inject] protected ILogger<App>? _logger { get; set; }



        // Dialogs visibility flags
        protected bool EditDialogVisibility { get; set; } = false;
        protected bool ValidationErrorsVisibility { get; set; } = false;
        protected bool DeleteConfirmVisibility { get; set; } = false;
        protected bool ErrorVisibility { get; set; } = false;

        protected string ToastTitle = "";
        protected string ToastContent = "";
        protected string ToastCssClass = "";

        protected bool editRecord = true;
        protected List<string?>? lstErrorMsg;  // validation message(s)

        protected SfToast? ToastObj { get; set; }
        protected DialogEffect efeitos = DialogEffect.Zoom;

        private string? _uri = string.Empty;

        protected int Id { get; set; }
        protected string? Description { get; set; }


        public DialogEffect Effects = DialogEffect.Zoom;

        protected List<ExpandoObject> GenericModelList { get; set; } = new List<ExpandoObject>();

        protected override void OnInitialized()
        {
            _uri = $"{_env["ApiSettings:UrlBase"]}/LookupTables";

        }
        /// <summary>
        /// Cria novo registo
        /// </summary>
        /// <param name="descricao"></param>
        /// <param name="tabela"></param>
        /// <returns></returns>
        public async Task<bool> CriaNovoRegisto(string descricao, string tabela)
        {
            try
            {
                var descriptionExist = await CheckIfRecordExist(descricao, tabela);
                if (descriptionExist) return false;

                LookupTableVM lookupTable = new LookupTableVM()
                {
                    Descricao = descricao,
                    Tabela = tabela
                };

                var retCode = await _httpClient.PostAsJsonAsync(_uri, lookupTable);
                return retCode.IsSuccessStatusCode;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Erro ao inserir registo no API");
                return false;
            }
        }

        /// <summary>
        /// Update support table
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="descricao"></param>
        /// <param name="tabela"></param>
        /// <returns></returns>
        public async Task<bool> ActualizaDetalhes(int Id, string descricao, string tabela)
        {
            try
            {
                LookupTableVM lookupTable = new LookupTableVM()
                {
                    Descricao = descricao,
                    Tabela = tabela,
                    Id = Id
                };

                var updateEndPoint = $"{_uri}/{Id}";
                var retCode = await _httpClient.PutAsJsonAsync(updateEndPoint, lookupTable);
                return retCode.IsSuccessStatusCode;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Erro ao atualizar registo no API");
                return false;
            }
        }


        public async Task<bool> DeleteRegisto(int id, string tableName)
        {
            try
            {
                var retCode = await _httpClient.DeleteAsync($"{_uri}/{id}/{tableName}");
                return retCode.IsSuccessStatusCode;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Erro ao apagar registo no API");
                return false;
            }

        }

        public async Task<string> GetDescription(int id, string tableName)
        {
            var descriprion = await _httpClient.GetStringAsync($"{_uri}/GetDescriptionByIdAndTable/{id}/{tableName}");
            return descriprion;
        }

        public async Task<IEnumerable<LookupTableVM>> GetLookupTableData(string tableName)
        {

            var tableData = await _httpClient.GetFromJsonAsync<IEnumerable<LookupTableVM>>($"{_uri}/GetAllRecords/{tableName}");
            return tableData!.ToList();

            //try
            //{
            //    var tableData = await _httpClient.GetFromJsonAsync<IEnumerable<LookupTableVM>>($"{_uri}/GetAllRecords/{tableName}");
            //    return tableData!.ToList();
            //}
            //catch (Exception exc)
            //{
            //    _logger.LogError(exc, "Erro ao pesquisar API");
            //    return Enumerable.Empty<LookupTableVM>();
            //}

        }

        public async Task<bool> CheckIfRecordExist(string description, string tableName)
        {
            try
            {
                var existInDb = await _httpClient.GetFromJsonAsync<bool>($"{_uri}/CheckRecordExist/{description}/{tableName}");
                return existInDb;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Erro ao pesquisar API");
                return false;
            }
        }



        public async Task<int> GetCodByDescricao(string description, string tableName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_uri}/GetPKByDescriptionAndTable/{description}/{tableName}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var output = JsonConvert.DeserializeObject<int>(data);
                    return output;
                }

                return -1;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Erro ao pesquisar API");
                return -1;
            }
        }

        public string GetDescricao(int Codigo, string Tabela)
        {
            throw new NotImplementedException();
        }

        public int GetId(string Descricao, string Tabela)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// CheckFKInUse
        /// </summary>
        /// <param name="IdFK"></param>
        /// <param name="fieldToCheck"></param>
        /// <param name="tableToCheck"></param>
        /// <returns></returns>
        public async Task<bool> CheckFKInUse(int IdFK, string fieldToCheck, string tableToCheck)
        {
            try
            {
                var existInDb = await _httpClient.GetFromJsonAsync<bool>($"{_uri}/CheckFkInUse/{IdFK}/{fieldToCheck}/{tableToCheck}");
                return existInDb;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Erro ao pesquisar API");
                return false;
            }
        }

        /// <summary>
        /// Check Registo Exist (redundante) há a versão com 'If'
        /// </summary>
        /// <param name="description"></param>
        /// <param name="tableToCheck"></param>
        /// <returns></returns>
        public async Task<bool> CheckRegistoExist(string description, string tableToCheck)
        {
            try
            {
                var existInDb = await _httpClient.GetFromJsonAsync<bool>($"{_uri}/CheckRecordExist/{description}/{tableToCheck}");
                return existInDb;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Erro ao pesquisar API");
                return false;
            }
        }

        /// <summary>
        /// Get last inserted Id
        /// </summary>
        /// <param name="tableToCheck"></param>
        /// <returns></returns>
        public async Task<int> GetLastInsertedId(string tableToCheck)
        {
            try
            {
                var lastInsertedId = await _httpClient.GetFromJsonAsync<int>($"{_uri}/GetLastInsertedId/{tableToCheck}");
                return lastInsertedId;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Erro ao pesquisar API");
                return -1;
            }
        }

        public async Task<IEnumerable<ExpandoObject>> GetDataGenerics<T>(string sourceDbTable) where T : class
        {
            try
            {
                var GenericList = (await GetLookupTableData(sourceDbTable)).ToList().OrderBy(o => o.Descricao);
                foreach (var item in GenericList)
                {
                    dynamic GenericModel = new ExpandoObject();
                    GenericModel.Id = item.Id;
                    GenericModel.Descricao = item.Descricao;
                    GenericModelList.Add(GenericModel);
                }

                IEnumerable<ExpandoObject> outputList = GenericModelList.Cast<ExpandoObject>().ToList();
                GenericModelList.Clear(); // = new List<ExpandoObject>(); // se não incluir esta linha, os dados aparecem sempre a dobrar, em cada Insert/Delete
                return outputList;

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<List<string>> ValidateTableEntries(LookupTableVM table)
        {
            var validatorEndpoint = $"{_uri}/lookuptables/ValidateLookupTable";
            using (HttpClient httpClient = new HttpClient())
            {

                var response = await httpClient.PostAsJsonAsync(validatorEndpoint, table);
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

    }
}
