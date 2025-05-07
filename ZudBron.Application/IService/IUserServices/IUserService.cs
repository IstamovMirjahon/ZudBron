using Article.Domain.Abstractions;
using ZudBron.Domain.DTOs.UserDTOs;

namespace ZudBron.Application.IService.IUserServices
{
    public interface IUserService
    {
        Task<Result<string>> ChangeUserFullNameService(ChangeUserFullNameDto changeUserFullNameDto, string userId);
        Task<Result<string>> ChangeUserEmailService(ChangeUserEmailDto changeUserEmailDto, string Id);
        Task<Result<string>> VerifyChangeUserEmailCodeService(ChangeUserEmailOrPhoneNumberVerificationCodeDto changeUserEmailVerificationCode, string Id);
        Task<Result<string>> ChangeUserPhoneNumberService(ChangeUserPhoneNumberDto changeUserPhoneNumberDto, string Id);
        Task<Result<string>> VerifyChangeUserPhoneNumberCodeService(ChangeUserEmailOrPhoneNumberVerificationCodeDto changeUserEmailOrPhoneNumberVerification, string Id);
        Task<Result<string>> ChangePasswordService(ChangePasswordDto changePasswordDto, string Id);
        Task<Result<string>> LogOutService(string id);
    }
}
