using Microsoft.Extensions.Options;
using PNTest.BLL.Models.RequestModel;
using PNTest.BLL.Services.Interfaces;
using PNTest.BLL.Settings;
using System.Net.Http.Json;

namespace PNTest.BLL.Services
{
    public sealed class GoogleApiService : IGoogleApiService
    {
        private readonly HttpClient _httpClient;
        private readonly GoogleApiSettings _options;
        private const int _radius = 100;
        public GoogleApiService(HttpClient httpClient, IOptions<GoogleApiSettings> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _httpClient.BaseAddress = new Uri(options.Value.BaseUrl!);
        }

        public async Task<object?> GetNearbyLocations(LocationRequestModel locationRequest)
        {
            var locationString = string.Join(',', new[] { locationRequest.Latitude, locationRequest.Longitude });

            var response = await _httpClient.GetAsync($"maps/api/place/nearbysearch/json?location={locationString}&radius={_radius}&key={_options.ApiKey}");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<object>();
            return data;
        }
    }
}
