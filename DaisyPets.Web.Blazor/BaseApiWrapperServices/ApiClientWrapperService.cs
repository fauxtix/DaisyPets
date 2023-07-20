using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DaisyPets.Web.Blazor.BaseApiWrapperServices
{
    public class ApiClientWrapperService<T>: HttpClient, IApiClientWrapperService<T> where T : class
    {
        private string basePath;
        private const string MEDIA_TYPE = "application/json";

        public ApiClientWrapperService(string baseAddress, string basePath)
        {
            BaseAddress = new Uri(baseAddress);
            this.basePath = basePath;
        }

        public async Task<T> Get(int? id)
        {
            try
            {
                SetupHeaders();

                var response = await GetAsync(basePath + $"/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var returnModel = JsonConvert.DeserializeObject<T>(result);

                    return returnModel;
                }
                else
                {
                    throw new Exception
                        ("Failed to retrieve item id: " + id + $" returned " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<T>> GetAll()
        {
            try
            {
                SetupHeaders();

                var response = await GetAsync(basePath);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var returnModel = JsonConvert.DeserializeObject<List<T>>(result);

                    return returnModel;
                }
                else
                {
                    throw new Exception
                        ($"Failed to retrieve items returned {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task Create(T item)
        {
            try
            {
                SetupHeaders();
                var serializedJson = GetSerializedObject(item);
                var bodyContent = GetBodyContent(serializedJson);

                var response = await PostAsync(basePath, bodyContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception
                    ($"Failed to create the resource returned {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task Update(int? id, T item)
        {
            try
            {
                SetupHeaders();
                var serializedJson = GetSerializedObject(item);
                var bodyContent = GetBodyContent(serializedJson);

                var response = await PutAsync(basePath + $"/{id}", bodyContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to Update the resource id: {id}, returned {response.StatusCode}");
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                SetupHeaders();

                var response = await DeleteAsync(basePath + $"/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception
                    ($"Failed to Delete the resource id: {id}, returned {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #region Client Helper Methods
        protected virtual void SetupHeaders()
        {
            DefaultRequestHeaders.Clear();

            //Define request data format  
            DefaultRequestHeaders.Accept.Add
                (new MediaTypeWithQualityHeaderValue
                (MEDIA_TYPE));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected virtual string GetSerializedObject(object obj)
        {
            var serializedJson = JsonConvert.SerializeObject(obj);

            return serializedJson;
        }

        protected virtual StringContent GetBodyContent(string serializedJson)
        {
            var bodyContent = new StringContent
                (serializedJson, Encoding.UTF8, "application/json");

            return bodyContent;
        }
        #endregion
    }
}
