using Microsoft.AspNetCore.Mvc;
using ZudBron.Application.IService.IFieldCategories;
using ZudBron.Domain.DTOs.FieldCategories;

namespace ZudBron.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldCategoryController : ControllerBase
    {
        private readonly IFieldCategoryService _fieldCategoryService;

        public FieldCategoryController(IFieldCategoryService fieldCategoryService)
        {
            _fieldCategoryService = fieldCategoryService;
        }

        /// <summary>
        /// Barcha maydon kategoriyalarini olish
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _fieldCategoryService.GetAllCategoriesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Kategoriya ID orqali olish
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _fieldCategoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound("Kategoriya topilmadi");

            return Ok(category);
        }

        /// <summary>
        /// Yangi kategoriya yaratish
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFieldCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _fieldCategoryService.CreateCategoryAsync(dto);
            return StatusCode(201, "Kategoriya muvaffaqiyatli yaratildi");
        }

        /// <summary>
        /// Kategoriya ma'lumotlarini yangilash
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFieldCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _fieldCategoryService.UpdateCategoryAsync(dto);
                return Ok("Kategoriya muvaffaqiyatli yangilandi");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Kategoriya o‘chirish
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _fieldCategoryService.DeleteCategoryAsync(id);
                return Ok("Kategoriya muvaffaqiyatli o‘chirildi");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
