using AutoMapper;
using MauiPets.Helpers;
using MauiPets.Mvvm.Views;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MauiPets.Services
{

    public class PetService
    {
        protected private IMapper _mapper { get; set; }
        public DevHttpsConnectionHelper devSslHelper;
        public HttpClient httpClient;
        public JsonSerializerOptions _serializerOptions;
        List<PetVM> _pets = new();
        PetVM _pet = new();
        public PetService(IMapper mapper)
        {
            devSslHelper = new DevHttpsConnectionHelper(sslPort: 4400);
            httpClient = devSslHelper.HttpClient;
            _serializerOptions = new JsonSerializerOptions
            {
                IncludeFields = true,
                PropertyNameCaseInsensitive = true
            };
            _mapper = mapper;
        }

        public async Task<List<PetVM>> GetPetsAsync()
        {
            var uri = $"{devSslHelper.DevServerRootUrl}/api/Pets/AllPetsVM";

            var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    _pets = await JsonSerializer.DeserializeAsync<List<PetVM>>(responseStream, _serializerOptions);
                }
            }

            return _pets;
        }
        public async Task<PetVM> GetPetVMByIdAsync(int Id)
        {
            var uri = $"{devSslHelper.DevServerRootUrl}/api/Pets/PetVMById/{Id}";

            var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    _pet = await JsonSerializer.DeserializeAsync<PetVM>(responseStream, _serializerOptions);
                }
            }

            return _pet;
        }


        public async Task<PetDto> GetPetByIdAsync(int id)
        {
            var uri = $"{devSslHelper.DevServerRootUrl}/api/Pets/{id}";
            var response = await httpClient.GetAsync(uri);
            //var response = await httpClient.GetAsync(devSslHelper.DevServerRootUrl + "/api/Pets/AllPetsVM");

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    return await JsonSerializer.DeserializeAsync<PetDto>(responseStream, _serializerOptions);
                }
            }

            return new PetDto();
        }


        public async Task<bool> SavePetData(PetDto petDto)
        {
            int petId = petDto.Id;

            string json = JsonSerializer.Serialize(petDto, _serializerOptions);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var uri = $"{devSslHelper.DevServerRootUrl}/api/Pets/{petId}";
            HttpResponseMessage response = null;
            response = await httpClient.PutAsJsonAsync(uri, petDto);
            var body = await response.Content.ReadAsStringAsync();

            //var otherresponse = await httpClient.PutAsync(uri, content);


            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        protected virtual StringContent GetBodyContent(string serializedJson)
        {
            var bodyContent = new StringContent
                (serializedJson, Encoding.UTF8, "application/json");

            return bodyContent;
        }


    }
}
