using Microsoft.Extensions.Options;
using PNTest.BLL.Models.RequestModel;
using PNTest.BLL.Models.ResponseModel;
using PNTest.BLL.Services.Interfaces;
using PNTest.BLL.Settings;
using System.Globalization;
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

        public async Task<IEnumerable<LocationResponse>> GetNearbyLocations(LocationRequest locationRequest)
        {
            var locationString = string.Join(",", new[] { locationRequest.Latitude.ToString("F6", CultureInfo.InvariantCulture), locationRequest.Longitude.ToString("F6", CultureInfo.InvariantCulture) });


            var response = await _httpClient.GetAsync($"maps/api/place/nearbysearch/json?location={locationString}&radius={_radius}&type={locationRequest.Type}&key={_options.ApiKey}");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<GoogleResponse?>();
            var transformedData = TransformGoogleResponse(data);
            return transformedData;
        }

        private IEnumerable<LocationResponse> TransformGoogleResponse(GoogleResponse? googleResponse)
        {
            List<LocationResponse> locations = new();
            if (googleResponse != null)
            {

                foreach (var location in googleResponse.results)
                {
                    locations.Add(new LocationResponse()
                    {
                        PlaceId = location.place_id,
                        LocationName = location.name,
                        LocationType = string.Join(",", location.types),
                        Address = location.vicinity,
                        Longitude = location.geometry.location.lng,
                        Latitude = location.geometry.location.lat,

                    });
                }
            }
            return locations;

        }
    }
}
