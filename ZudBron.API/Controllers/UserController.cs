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
            Summary = "Email ni yangilash yoki qo'shish?",
            Description = "Yangi Email kiriting va bosing"
            )]
        public async Task<IActionResult> ChangeUserEmailOrAddUserEmail([FromBody] ChangeUserEmailDto request)
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
        public async Task<IActionResult> VerifyChangeUserEmailOrAddUserEmailCode([FromBody] ChangeUserEmailOrPhoneNumberVerificationCodeDto request)
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

        [HttpPut]
        [SwaggerOperation(
            Summary = "PhoneNumber ni yangilash yoki qo'shish?",
            Description = "Yangi PhoneNumber kiriting va bosing"
            )]
        public async Task<IActionResult> ChangeUserPhoneNumberOrAddUserPhoneNumber([FromBody] ChangeUserPhoneNumberDto request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Foydalanuvchi aniqlanmadi");

                var result = await _userService.ChangeUserPhoneNumberService(request, userId);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "ChangeUserPhoneNumber uchun kodni tasdiqlash",
            Description = "Telefon raqamga yuborilgan kodni kiriting va bosing"
            )]
        public async Task<IActionResult> VerifyChangeUserPhoneNumberOrAddUserPhoneNumberCode([FromBody] ChangeUserEmailOrPhoneNumberVerificationCodeDto request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Foydalanuvchi aniqlanmadi");

                var result = await _userService.VerifyChangeUserPhoneNumberCodeService(request, userId);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Server xatosi", Error = ex.Message });
            }
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Parolni yangilash?",
            Description = "Yangi parol kiriting va bosing"
            )]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Foydalanuvchi aniqlanmadi");

                var result = await _userService.ChangePasswordService(request, userId);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
