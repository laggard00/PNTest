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

        public GooglePlacesController(IGoogleApiService googleApiService)
        {
            _googleApiService = googleApiService;
        }

        [HttpPost("nearby")]
        public async Task<IActionResult> GetNearbyLocations([FromBody] LocationRequest locationRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _googleApiService.GetNearbyLocations(locationRequest);
            return Ok(result);
        }
    }
}
