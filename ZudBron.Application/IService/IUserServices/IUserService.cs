using Article.Domain.Abstractions;
using ZudBron.Domain.DTOs.UserDTOs;

namespace ZudBron.Application.IService.IUserServices
{
    public interface IUserService
    {
        Task<Result<string>> ChangeUserFullNameService(ChangeUserFullNameDto changeUserFullNameDto, string userId);
        Task<Result<string>> ChangeUserEmailService(ChangeUserEmailDto changeUserEmailDto, string Id);
        Task<Result<string>> VerifyChangeUserEmailCodeService(ChangeUserEmailOrPhoneNumberVerificationCode changeUserEmailVerificationCode, string Id);
        Task<Result<string>> ChangeUserPhoneNumberService(ChangeUserPhoneNumberDto changeUserPhoneNumberDto, string Id);
        Task<Result<string>> VerifyChangeUserPhoneNumberCodeService(ChangeUserEmailOrPhoneNumberVerificationCode changeUserEmailOrPhoneNumberVerification, string Id);
    }
}
