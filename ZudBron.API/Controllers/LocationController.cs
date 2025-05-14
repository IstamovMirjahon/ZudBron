using Microsoft.AspNetCore.Mvc;
using ZudBron.Application.IService.ILocationServices;
using ZudBron.Domain.DTOs.LocationDTO;

namespace ZudBron.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Foydalanuvchi joylashuvi va sport maydon manzili asosida marshrut URL'ini qaytaradi.
        /// </summary>
        [HttpPost("route")]
        public async Task<IActionResult> GetRoute([FromBody] RouteRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _locationService.GetGoogleMapsRouteAsync(request);
                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] CreateLocationDto dto)
        {
            var locationId = await _locationService.CreateLocationAsync(dto);
            return Ok(new { message = "Location created successfully", id = locationId.Value });
        }
    }
}
