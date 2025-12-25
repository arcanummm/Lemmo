using Lemmo.WebApi.Persistance.Auth.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Lemmo.WebApi.Persistance.Auth.Implementations
{
    public class JwtTokenService(IConfiguration config) : ITokenService
    {
        public string GenerateAccessToken(string userId, string phoneNumber)
        {
            var jwt = config.GetSection("Jwt");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.MobilePhone, phoneNumber)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt["AccessExpiresMinutes"]!)),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public (string Token, DateTime ExpiresAt) GenerateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);

            string token = Convert.ToBase64String(bytes);
            DateTime expiresAt = DateTime.UtcNow.AddDays(int.Parse(config["Jwt:RefreshExpiresDays"]!));

            return (token, expiresAt);
        }
    }
}
