using Microsoft.AspNetCore.Mvc;
using ZudBron.Application.IService.IFieldServices;
using ZudBron.Domain.DTOs.FieldDTO;

namespace ZudBron.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SportFieldsController : ControllerBase
    {
        private readonly ISportFieldService _sportFieldService;

        public SportFieldsController(ISportFieldService sportFieldService)
        {
            _sportFieldService = sportFieldService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _sportFieldService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _sportFieldService.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrUpdateSportFieldDto dto)
        {
            var id = await _sportFieldService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CreateOrUpdateSportFieldDto dto)
        {
            var updated = await _sportFieldService.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _sportFieldService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpGet("{id}/reviews")]
        public async Task<IActionResult> GetReviews(Guid id)
        {
            var reviews = await _sportFieldService.GetReviewsBySportFieldIdAsync(id);
            return Ok(reviews);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter([FromBody] SportFieldFilterDto filter)
        {
            var result = await _sportFieldService.FilterAsync(filter);
            return Ok(result);
        }
    }
}
