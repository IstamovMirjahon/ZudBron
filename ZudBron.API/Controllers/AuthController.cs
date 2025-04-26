using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ZudBron.Domain.DTOs.TokenDTOs;
using ZudBron.Domain.DTOs.UserDTOs;

namespace ZudBron.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [SwaggerOperation(
            Summary = "User registratsiya qilish",
            Description = "User registratsiya qilish uchun ma'lumotlarni to'ldiring va bosing"
            )]
        public async Task<IActionResult> SignUpByEmail([FromBody] UserRegisterByEmailDto model)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Server xatosi", Error = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "User registratsiya qilish",
            Description = "User registratsiya qilish uchun ma'lumotlarni to'ldiring va bosing"
            )]
        public async Task<IActionResult> SignUpByPhone([FromBody] UserRegisterByPhoneDto model)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Server xatosi", Error = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Userning tizimga kirishi",
            Description = "Userning tizimga kirishi uchun email va parolingizni kiriting va bosing"
            )]
        public async Task<IActionResult> SignInByEmail([FromBody] SignInByEmailDTO signInDTO)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Server xatosi", Error = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Userning tizimga kirishi",
            Description = "Userning tizimga kirishi uchun email va parolingizni kiriting va bosing"
            )]
        public async Task<IActionResult> SignInByPhone([FromBody] SignInByPhoneDTO signInDTO)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Server xatosi", Error = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Register uchun kodni tasdiqlash",
            Description = "Emailingiz, yuborilgan kodni kiriting va bosing"
            )]
        public async Task<IActionResult> VerifyRegisterCode([FromBody] RegisterVerificationCodeDto verificationCodeDTO)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Server xatosi", Error = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Parolni unutdingizmi?",
            Description = "Ro'yhatdan o'tgan emailingiz orqali parolingizni almashtiring"
            )]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDTO)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "ForgotPassword uchun kodni tasdiqlash",
            Description = "Emailingiz, yuborilgan kodni kiriting va bosing"
            )]
        public async Task<IActionResult> VerifyForgotPasswordCode([FromBody] ForgotPasswordVerificationCodeDto dto)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Parolni yangilash?",
            Description = "Token, yangi parolingizni kiriting va parolingizni almashtiring"
            )]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordDTO)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Token yangilash",
            Description = "Refresh token kiriting va bosing"
            )]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO refreshToken)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Server xatosi", Error = ex.Message });
            }
        }
    }
}
