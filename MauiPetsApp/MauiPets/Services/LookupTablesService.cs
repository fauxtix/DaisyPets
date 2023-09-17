using MauiPets.Helpers;
using MauiPets.Mvvm.Models;
using System.Dynamic;
using System.Text.Json;

namespace MauiPets.Services
{
    public class LookupTablesService
    {
        public DevHttpsConnectionHelper devSslHelper;
        public HttpClient httpClient;
        public JsonSerializerOptions _serializerOptions;
        List<LookupTableVM> _lookupTable = new();
        protected List<ExpandoObject> GenericModelList { get; set; } = new();

        public LookupTablesService()
        {
            devSslHelper = new DevHttpsConnectionHelper(sslPort: 4400);
            httpClient = devSslHelper.HttpClient;
            _serializerOptions = new JsonSerializerOptions
            {
                IncludeFields = true,
                PropertyNameCaseInsensitive = true
            };

        }

        public async Task<string> GetDescription(int id, string tableName)
        {
            var uri = $"{devSslHelper.DevServerRootUrl}/api/LookupTables";

            var descriprion = await httpClient.GetStringAsync($"{uri}/GetDescriptionByIdAndTable/{id}/{tableName}");
            return descriprion;
        }

        public async Task<List<LookupTableVM>> GetLookupTableData(string tableName)
        {
            var uri = $"{devSslHelper.DevServerRootUrl}/api/LookupTables/GetAllRecords/{tableName}";
            try
            {
                var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        _lookupTable = await JsonSerializer.DeserializeAsync<List<LookupTableVM>>(responseStream, _serializerOptions);
                    }
                }

                return _lookupTable;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ExpandoObject>> GetDataGenerics<T>(string sourceDbTable) where T : class
        {
            try
            {
                var genericList = (await GetLookupTableData(sourceDbTable)).ToList().OrderBy(o => o.Descricao);
                foreach (var item in genericList)
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

    }
}
