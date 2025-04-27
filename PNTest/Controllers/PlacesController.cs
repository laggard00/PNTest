using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PNTest.BLL.Models.RequestModel;
using PNTest.BLL.Models.ResponseModel;
using PNTest.BLL.Services.Interfaces;
using PNTest.Idempotency;
using PNTest.SignalR;

namespace PNTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : BaseController
    {
        private readonly IGoogleApiService _googleApiService;
        private readonly IRequestService _requestPersistService;
        private readonly IResponsePersistService _responsePersistService;
        private readonly IHubContext<RequestHub> _hubContext;
        private readonly ILocationService _locationService;
        public PlacesController(IGoogleApiService googleApiService,
                                      IResponsePersistService responsePersistService,
                                      IRequestService requestPersistService,
                                      IHubContext<RequestHub> hubContext,
                                      ILocationService locationService)
        {
            _googleApiService = googleApiService;
            _responsePersistService = responsePersistService;
            _requestPersistService = requestPersistService;
            _hubContext = hubContext;
            _locationService = locationService;
        }

        [HttpPost("nearby")]
        [Idempotent]
        public async Task<IActionResult> GetNearbyLocations([FromBody] LocationRequest locationRequest, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);




            var request = await _requestPersistService.SaveLocationRequest(locationRequest, UserId);
            var result = await _googleApiService.GetNearbyLocations(locationRequest);
            await _responsePersistService.PersistLocationResponse(request.Id, result);
            await _hubContext.Clients.All.SendAsync("ReceiveNewRequest", $"New location request by  {UserId}  \n lat: {locationRequest.Latitude} \n lng: {locationRequest.Longitude}", cancellationToken);
            return Ok(result);
        }

        [HttpPost("favorite")]
        public async Task<IActionResult> AddFavoriteLocation([FromBody] FavoritePlaceRequest favoritePlaceRequest, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _locationService.AddFavoriteLocation(favoritePlaceRequest.placeId, UserId);
            return Created();
        }

    }
}
