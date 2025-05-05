using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using ZudBron.Application.IService.IUserServices;
using ZudBron.Domain.DTOs.UserDTOs;

namespace ZudBron.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Fullname ni yangilash?",
            Description = "Yangi FullName kiriting va bosing"
            )]
        public async Task<IActionResult> ChangeUserFullName([FromBody] ChangeUserFullNameDto request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Foydalanuvchi aniqlanmadi");

                var result = await _userService.ChangeUserFullNameService(request, userId);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Email ni yangilash?",
            Description = "Yangi Email kiriting va bosing"
            )]
        public async Task<IActionResult> ChangeUserEmail([FromBody] ChangeUserEmailDto request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Foydalanuvchi aniqlanmadi");

                var result = await _userService.ChangeUserEmailService(request, userId);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "ChangeUserEmail uchun kodni tasdiqlash",
            Description = "Emailga yuborilgan kodni kiriting va bosing"
            )]
        public async Task<IActionResult> VerifyChangeUserEmailCode([FromBody] ChangeUserEmailVerificationCode request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Foydalanuvchi aniqlanmadi");

                var result = await _userService.VerifyChangeUserEmailCodeService(request, userId);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Server xatosi", Error = ex.Message });
            }
        }
    }
}
