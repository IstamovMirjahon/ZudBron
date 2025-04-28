using ZudBron.Domain.Enums.UserEnum;
using ZudBron.Domain.Models.UserModel;

namespace ZudBron.Application.IService.ITokenServices
{
    public interface ITokenService
    {
        string GenerateAccessToken(Guid userId, UserRole role);
        Task<string> GenerateRefreshTokenAsync(Guid userId);
        Task<RefreshToken?> GetToken(string refreshToken);
    }
}
