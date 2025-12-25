namespace Lemmo.WebApi.Persistance.Auth.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(string userId, string phoneNumber);

        (string Token, DateTime ExpiresAt) GenerateRefreshToken();
    }
}
