using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ZudBron.Application.IService.ITokenServices;
using ZudBron.Domain.Enums.UserEnum;
using ZudBron.Domain.Models.UserModel;

namespace ZudBron.Infrastructure.Services.TokenServices
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;
        public TokenService(IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
        }
        public string GenerateAccessToken(Guid userId, UserRole role)
        {
            var secretKey = _configuration["JwtSettings:Secret"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var expiryMinutes = int.TryParse(_configuration["JwtSettings:ExpiryMinutes"], out var result) ? result : 60;
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentException("JWT Secret key is missing in configuration.");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
        new Claim(ClaimTypes.Role, role.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateRefreshTokenAsync(Guid userId)
        {
            string refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)); // 64 bayt uzunlikdagi tasodifiy token

            // Avval mavjud refresh tokenni tekshirib ko‘ramiz
            var existingToken = await _applicationDbContext.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.UserId == userId);

            if (existingToken != null)
            {
                // Mavjud tokenni yangilash
                existingToken.Token = refreshToken;
                existingToken.ExpiryDate = DateTime.UtcNow.AddDays(7);
            }
            else
            {
                // Agar yo‘q bo‘lsa, yangi RefreshToken obyektini qo‘shish
                var refreshTokenEntity = new RefreshToken
                {
                    UserId = userId,
                    Token = refreshToken,
                    ExpiryDate = DateTime.UtcNow.AddDays(7)
                };

                await _applicationDbContext.RefreshTokens.AddAsync(refreshTokenEntity);
            }

            return refreshToken;
        }

        public async Task<RefreshToken?> GetToken(string refreshToken)
        {
            return await _applicationDbContext.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);
        }

        public async Task<RefreshToken?> GetTokenByUserId(Guid UserId)
        {
            return await _applicationDbContext.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.UserId == UserId);
        }

        public void DeleteRefreshToken(RefreshToken refreshToken)
        {
            _applicationDbContext.Remove(refreshToken);
        }
    }
}
