using ZudBron.Domain.Models.UserModel;

namespace ZudBron.Infrastructure.Repositories.AuthRepositories
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        int GenerateCode();
        Task<TempUser?> GetTempUserByEmailAsync(string email);
        Task SaveUpdateVerificationCode(TempUser oldTempUser, TempUser newTempUser);
        Task SaveAddVerificationCode(TempUser tempUser);
        Task AddUserAsync(User user);
        void TempUserDelete(TempUser tempUser);
        Task<ForgotPassword?> GetForgotPasswordByEmailAsync(string email);
        Task UpdateForgotPassword(ForgotPassword oldForgotPassword, ForgotPassword newPassword);
        Task AddForgotPasswordAsync(ForgotPassword forgotPassword);
        Task<User?> GetByIdAsync(Guid id);
        void ForgotPasswordDelete(ForgotPassword forgotPassword);
        Task<User?> GetUserByPhoneAsync(string phoneNumber);
        Task<TempUser?> GetTempUserByPhoneAsync(string phone);
        Task SaveUpdateVerificationCodeByPhone(TempUser oldTempUser, TempUser newTempUser);
        Task<ForgotPassword?> GetForgotPasswordByPhoneAsync(string phone);
        Task UpdateForgotPasswordByPhone(ForgotPassword oldForgotPassword, ForgotPassword newPassword);
    }
}
