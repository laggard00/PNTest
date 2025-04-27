using Microsoft.AspNetCore.Mvc;
using PNTest.BLL.Services.Interfaces;
using PNTest.DAL.Context;

namespace PNTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRequests([FromQuery] string? type, [FromQuery] string? search)
        {
            var requests = await _requestService.GetRequests(type, search);
            return Ok(requests);
        }
    }
}
