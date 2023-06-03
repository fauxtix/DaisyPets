using DaisyPets.Core.Application.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System;

namespace DaisyPets.UI.ApiServices
{
    public class AccessAPIService : IAccessAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _apiUrl;
        public AccessAPIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiUrl = $"{_configuration["UrlBase"]}";
        }

        public async Task<IEnumerable<PetVM>> GetAllPets()
        {
            using (var response = await _httpClient.GetAsync((GetPetsUrl())))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        IncludeFields = true,
                        PropertyNameCaseInsensitive = true
                    };
                    var pets = JsonSerializer.Deserialize<IEnumerable<PetVM>>(content, options);
                    return pets!.ToList();
                }
                else
                {
                    throw new Exception("Não foi possível ler a base de dados: " + response.StatusCode);
                }
            }
        }


        public string GetPetsUrl()
        {
            return $"{_apiUrl}/Pets;";
        }
    }
}
