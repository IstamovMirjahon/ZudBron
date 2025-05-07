using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ZudBron.Application.IService.IAuthServices;
using ZudBron.Domain.DTOs.TokenDTOs;
using ZudBron.Domain.DTOs.UserDTOs;

namespace ZudBron.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [SwaggerOperation(
            Summary = "User registratsiya qilish",
            Description = "User registratsiya qilish uchun ma'lumotlarni to'ldiring va bosing"
            )]
        public async Task<IActionResult> SignUpByEmail([FromBody] UserRegisterByEmailDto request)
        {
            try
            {
                var result = await _authService.SignUpService(request);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
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
        public async Task<IActionResult> SignInByEmail([FromBody] SignInByEmailDto request)
        {
            try
            {
                var result = await _authService.SignInService(request);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
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
        public async Task<IActionResult> VerifyRegisterCodeByEmail([FromBody] RegisterVerificationCodeDto request)
        {
            try
            {
                var result = await _authService.VerifyRegisterCodeService(request);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
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
        public async Task<IActionResult> ForgotPasswordByEmail([FromBody] ForgotPasswordDto request)
        {
            try
            {
                var result = await _authService.ForgotPasswordService(request);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "ForgotPassword uchun kodni tasdiqlash",
            Description = "Emailingizga yuborilgan kodni kiriting va bosing"
            )]
        public async Task<IActionResult> VerifyForgotPasswordCodeByEmail([FromBody] ForgotPasswordVerificationCodeDto request)
        {
            try
            {
                var result = await _authService.VerifyForgotPasswordCodeService(request);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Parolni yangilash?",
            Description = "Yangi parolingizni kiriting va parolingizni almashtiring"
            )]
        public async Task<IActionResult> ResetPasswordByEmail([FromBody] ResetPasswordRequestDto request)
        {
            try
            {
                var result = await _authService.ResetPasswordService(request);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "User registratsiya qilish",
            Description = "User registratsiya qilish uchun ma'lumotlarni to'ldiring va bosing"
            )]
        public async Task<IActionResult> SignUpByPhone([FromBody] UserRegisterByPhoneDto request)
        {
            try
            {
                var result = await _authService.SignUpByPhoneService(request);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Server xatosi", Error = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Userning tizimga kirishi",
            Description = "Userning tizimga kirishi uchun PhoneNumber va parolingizni kiriting va bosing"
            )]
        public async Task<IActionResult> SignInByPhone([FromBody] SignInByPhoneDto request)
        {
            try
            {
                var result = await _authService.SignInByPhoneService(request);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
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
        public async Task<IActionResult> VerifyRegisterCodeByPhone([FromBody] RegisterVerificationCodeByPhoneDto request)
        {
            try
            {
                var result = await _authService.VerifyRegisterCodeByPhoneService(request);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Server xatosi", Error = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Parolni unutdingizmi?",
            Description = "Ro'yhatdan o'tgan PhoneNumber orqali parolingizni almashtiring"
            )]
        public async Task<IActionResult> ForgotPasswordByPhone([FromBody] ForgotPasswordByPhoneDto request)
        {
            try
            {
                var result = await _authService.ForgotPasswordByPhoneService(request);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "ForgotPassword uchun kodni tasdiqlash",
            Description = "PhoneNumberingizga yuborilgan kodni kiriting va bosing"
            )]
        public async Task<IActionResult> VerifyForgotPasswordCodeByPhone([FromBody] ForgotPasswordVerificationCodeByPhoneDto request)
        {
            try
            {
                var result = await _authService.VerifyForgotPasswordCodeByPhoneService(request);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Parolni yangilash?",
            Description = "Yangi parolingizni kiriting va parolingizni almashtiring"
            )]
        public async Task<IActionResult> ResetPasswordByPhone([FromBody] ResetPasswordRequestByPhoneDto request)
        {
            try
            {
                var result = await _authService.ResetPasswordByPhoneService(request);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
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
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto request)
        {
            try
            {
                var result = await _authService.RefreshTokenService(request);

                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Server xatosi", Error = ex.Message });
            }
        }
    }
}
