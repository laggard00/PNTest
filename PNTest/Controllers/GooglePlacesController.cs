using Microsoft.AspNetCore.Mvc;
using PNTest.BLL.Models.RequestModel;
using PNTest.BLL.Services.Interfaces;
using PNTest.Idempotency;

namespace PNTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GooglePlacesController : ControllerBase
    {
        private readonly IGoogleApiService _googleApiService;
        private readonly IRequestService _requestPersistService;
        private readonly IResponsePersistService _responsePersistService;

        public GooglePlacesController(IGoogleApiService googleApiService,
                                      IResponsePersistService responsePersistService,
                                      IRequestService requestPersistService)
        {
            _googleApiService = googleApiService;
            _responsePersistService = responsePersistService;
            _requestPersistService = requestPersistService;
        }

        [HttpPost("nearby")]
        [Idempotent]
        public async Task<IActionResult> GetNearbyLocations([FromBody] LocationRequest locationRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int userId = 0; 

            if (HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is int id)
            {
                userId = id;
            }
            else
            {
                throw new Exception("Middleware not working");
            }

         
            var request = await _requestPersistService.SaveLocationRequest(locationRequest, userId);
            var result = await _googleApiService.GetNearbyLocations(locationRequest);
            await _responsePersistService.PersistLocationResponse(request.Id, result);

            return Ok(result);
        }
    }
}
