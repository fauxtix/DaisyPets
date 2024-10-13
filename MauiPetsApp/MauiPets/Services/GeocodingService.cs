using System.Net.Http.Json;

namespace MauiPets.Services
{
    public class GeocodingService
    {
        private readonly HttpClient _httpClient;

        public GeocodingService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Location> GetCoordinatesFromAddressAsync(string address)
        {
            if (string.IsNullOrEmpty(address))
                return null;

            var encodedAddress = System.Net.WebUtility.UrlEncode(address);
            var url = $"https://nominatim.openstreetmap.org/search?q={encodedAddress}&format=json&addressdetails=1";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var results = await response.Content.ReadFromJsonAsync<List<NominatimResult>>();
                if (results != null && results.Count > 0)
                {
                    var location = results[0];
                    return new Location(double.Parse(location.Lat), double.Parse(location.Lng));
                }
            }

            return null;
        }

        private class NominatimResult
        {
            public string Lat { get; set; }
            public string Lng { get; set; }
        }
    }

    // Classe Location
    public class Location
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
