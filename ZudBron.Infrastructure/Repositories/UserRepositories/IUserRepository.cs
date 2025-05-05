using ZudBron.Domain.Models.UserModel;

namespace ZudBron.Infrastructure.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<ChangeUserEmailOrPhoneNumber?> GetChangeUserEmailOrPhoneNumberById(Guid userId);
        Task UpdateChangeUserEmailOrPhoneNumber(ChangeUserEmailOrPhoneNumber oldEmailOrPhoneNumber, ChangeUserEmailOrPhoneNumber newEmailOrPhoneNumber);
        Task AddChangeUserEmailOrPhoneNumber(ChangeUserEmailOrPhoneNumber EmailOrPhoneNumber);
        void ChangeUserDelete(ChangeUserEmailOrPhoneNumber changeUserEmailOrPhoneNumber);
    }
}
