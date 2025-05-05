using Microsoft.EntityFrameworkCore;
using ZudBron.Domain.Models.UserModel;

namespace ZudBron.Infrastructure.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<ChangeUserEmailOrPhoneNumber?> GetChangeUserEmailOrPhoneNumberById(Guid userId)
        {
            return await _applicationDbContext.ChangeUserEmailOrPhoneNumbers
                .FirstOrDefaultAsync(user => user.UserId == userId);
        }

        public Task UpdateChangeUserEmailOrPhoneNumber(ChangeUserEmailOrPhoneNumber oldEmailOrPhoneNumber, ChangeUserEmailOrPhoneNumber newEmailOrPhoneNumber)
        {
            oldEmailOrPhoneNumber.Email = newEmailOrPhoneNumber.Email;
            oldEmailOrPhoneNumber.VerificationCode = newEmailOrPhoneNumber.VerificationCode;
            oldEmailOrPhoneNumber.ExpirationTime = newEmailOrPhoneNumber.ExpirationTime;

            return Task.CompletedTask;
        }

        public async Task AddChangeUserEmailOrPhoneNumber(ChangeUserEmailOrPhoneNumber EmailOrPhoneNumber)
        {
            await _applicationDbContext.ChangeUserEmailOrPhoneNumbers.AddAsync(EmailOrPhoneNumber);
        }

        public void ChangeUserDelete(ChangeUserEmailOrPhoneNumber changeUserEmailOrPhoneNumber)
        {
            _applicationDbContext.ChangeUserEmailOrPhoneNumbers.Remove(changeUserEmailOrPhoneNumber);
        }
    }
}
