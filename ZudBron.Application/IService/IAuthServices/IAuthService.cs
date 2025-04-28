using Article.Domain.Abstractions;
using ZudBron.Domain.DTOs.TokenDTOs;
using ZudBron.Domain.DTOs.UserDTOs;

namespace ZudBron.Application.IService.IAuthServices
{
    public interface IAuthService
    {
        Task<Result<string>> SignUpService(UserRegisterByEmailDto model);
        Task<Result<string>> VerifyRegisterCodeService(RegisterVerificationCodeDto registerVerificationCode);
        Task<Result<TokenResponse>> SignInService(SignInByEmailDto signInByEmail);
        Task<Result<string>> ForgotPasswordService(ForgotPasswordDto model);
        Task<Result<string>> VerifyForgotPasswordCodeService(ForgotPasswordVerificationCodeDto forgotPasswordVerificationCode);
        Task<Result<string>> ResetPasswordService(ResetPasswordRequestDto resetPasswordRequest);
        Task<Result<TokenResponse>> RefreshTokenService(RefreshTokenDto refreshTokenDto);
    }
}
