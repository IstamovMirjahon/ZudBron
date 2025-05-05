using Article.Domain.Abstractions;
using ZudBron.Domain.DTOs.UserDTOs;

namespace ZudBron.Application.IService.IUserServices
{
    public interface IUserService
    {
        Task<Result<string>> ChangeUserFullNameService(ChangeUserFullNameDto changeUserFullNameDto, string userId);
        Task<Result<string>> ChangeUserEmailService(ChangeUserEmailDto changeUserEmailDto, string Id);
        Task<Result<string>> VerifyChangeUserEmailCodeService(ChangeUserEmailVerificationCode changeUserEmailVerificationCode, string Id);
    }
}
