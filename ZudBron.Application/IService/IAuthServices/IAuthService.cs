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
        Task<Result<string>> SignUpByPhoneService(UserRegisterByPhoneDto model);
        Task<Result<string>> VerifyRegisterCodeByPhoneService(RegisterVerificationCodeByPhoneDto registerVerificationCodeByPhone);
        Task<Result<TokenResponse>> SignInByPhoneService(SignInByPhoneDto signInByPhone);
        Task<Result<string>> ForgotPasswordByPhoneService(ForgotPasswordByPhoneDto model);
        Task<Result<string>> VerifyForgotPasswordCodeByPhoneService(ForgotPasswordVerificationCodeByPhoneDto forgotPasswordVerificationCodeByPhone);
        Task<Result<string>> ResetPasswordByPhoneService(ResetPasswordRequestByPhoneDto resetPasswordRequestByPhone);
        Task<Result<TokenResponse>> RefreshTokenService(RefreshTokenDto refreshTokenDto);
    }
}
