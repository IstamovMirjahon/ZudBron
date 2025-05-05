using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Security.Cryptography;
using ZudBron.Domain.Models.UserModel;
using ZudBron.Domain.StaticModels.SmtpModel;

namespace ZudBron.Infrastructure.Repositories.AuthRepositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AuthRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _applicationDbContext.Users
                .FirstOrDefaultAsync(user => user.Email == email && !user.IsDeleted);
        }

        public int GenerateCode()
        {
            return RandomNumberGenerator.GetInt32(100000, 1000000);
        }

        public async Task<TempUser?> GetTempUserByEmailAsync(string email)
        {
            return await _applicationDbContext.TempUsers
                .FirstOrDefaultAsync(user => user.Email == email);
        }

        public Task SaveUpdateVerificationCode(TempUser oldTempUser, TempUser newTempUser)
        {
            oldTempUser.FullName = newTempUser.FullName;
            oldTempUser.Email = newTempUser.Email;
            oldTempUser.HashedPassword = newTempUser.HashedPassword;
            oldTempUser.VerificationCode = newTempUser.VerificationCode;
            oldTempUser.ExpirationTime = newTempUser.ExpirationTime;

            return Task.CompletedTask;
        }

        public async Task SaveAddVerificationCode(TempUser tempUser)
        {
            await _applicationDbContext.TempUsers.AddAsync(tempUser);
        }

        public async Task AddUserAsync(User user)
        {
            await _applicationDbContext.Users.AddAsync(user);
        }

        public void TempUserDelete(TempUser tempUser)
        {
            _applicationDbContext.TempUsers.Remove(tempUser);
        }

        public async Task<ForgotPassword?> GetForgotPasswordByEmailAsync(string email)
        {
            return await _applicationDbContext.ForgotPasswords
                .FirstOrDefaultAsync(user => user.Email == email);
        }

        public Task UpdateForgotPassword(ForgotPassword oldForgotPassword, ForgotPassword newPassword)
        {
            oldForgotPassword.Email = newPassword.Email;
            oldForgotPassword.VerificationCode = newPassword.VerificationCode;
            oldForgotPassword.ExpirationTime = newPassword.ExpirationTime;
            oldForgotPassword.IsUsed = newPassword.IsUsed;

            return Task.CompletedTask;
        }

        public async Task AddForgotPasswordAsync(ForgotPassword forgotPassword)
        {
            await _applicationDbContext.ForgotPasswords.AddAsync(forgotPassword);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _applicationDbContext.Users
                .FirstOrDefaultAsync(user => user.Id == id && !user.IsDeleted);
        }

        public void ForgotPasswordDelete(ForgotPassword forgotPassword)
        {
            _applicationDbContext.ForgotPasswords.Remove(forgotPassword);
        }

        public async Task<User?> GetUserByPhoneAsync(string phoneNumber)
        {
            return await _applicationDbContext.Users
                .FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber && !user.IsDeleted);
        }

        public async Task<TempUser?> GetTempUserByPhoneAsync(string phone)
        {
            return await _applicationDbContext.TempUsers
                .FirstOrDefaultAsync(user => user.PhoneNumber == phone);
        }

        public Task SaveUpdateVerificationCodeByPhone(TempUser oldTempUser, TempUser newTempUser)
        {
            oldTempUser.FullName = newTempUser.FullName;
            oldTempUser.PhoneNumber = newTempUser.PhoneNumber;
            oldTempUser.HashedPassword = newTempUser.HashedPassword;
            oldTempUser.VerificationCode = newTempUser.VerificationCode;
            oldTempUser.ExpirationTime = newTempUser.ExpirationTime;

            return Task.CompletedTask;
        }

        public async Task<ForgotPassword?> GetForgotPasswordByPhoneAsync(string phone)
        {
            return await _applicationDbContext.ForgotPasswords
                .FirstOrDefaultAsync(user => user.PhoneNumber == phone);
        }

        public Task UpdateForgotPasswordByPhone(ForgotPassword oldForgotPassword, ForgotPassword newPassword)
        {
            oldForgotPassword.PhoneNumber = newPassword.PhoneNumber;
            oldForgotPassword.VerificationCode = newPassword.VerificationCode;
            oldForgotPassword.ExpirationTime = newPassword.ExpirationTime;
            oldForgotPassword.IsUsed = newPassword.IsUsed;

            return Task.CompletedTask;
        }
    }
}
