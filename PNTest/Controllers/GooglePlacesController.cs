using Microsoft.AspNetCore.Mvc;
using PNTest.BLL.Models.RequestModel;
using PNTest.BLL.Services.Interfaces;

namespace PNTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GooglePlacesController : ControllerBase
    {
        private readonly IGoogleApiService _googleApiService;
        private readonly IRequestPersistService _requestPersistService;
        private readonly IResponsePersistService _responsePersistService;

        public GooglePlacesController(IGoogleApiService googleApiService, IResponsePersistService responsePersistService, IRequestPersistService requestPersistService)
        {
            _googleApiService = googleApiService;
            _responsePersistService = responsePersistService;
            _requestPersistService = requestPersistService;
        }

        [HttpPost("nearby")]
        public async Task<IActionResult> GetNearbyLocations([FromBody] LocationRequest locationRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = await _requestPersistService.PersistLocationRequest(locationRequest, 1);
            var result = await _googleApiService.GetNearbyLocations(locationRequest);
            await _responsePersistService.PersistLocationResponse(request.Id, result);

            return Ok(result);
        }
    }
}
